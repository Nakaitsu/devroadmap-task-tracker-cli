using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Task_Tracker;

try
{
    if (args.Length > 0)
    {
        var jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true
        };

        switch (args[0])
        {
            case "add":
                if (args.Length >= 2)
                {
                    var newTask = new Task_Tracker.Task
                    {
                        Description = args[1]
                    };

                    var database = GetDatabase();

                    newTask.Id = ++database.LastInsertedId;
                    database.Tasks.Add(newTask);

                    File.WriteAllText(SystemDatabase.FileName, JsonSerializer.Serialize(database, jsonOptions));

                    Console.WriteLine($"Task added successfully (ID: {newTask.Id})");
                }

                break;
            case "update":
                if (args.Length >= 3)
                {
                    var database = GetDatabase();

                    var taskToUpdate = database.Tasks.FirstOrDefault(x => x.Id == int.Parse(args[1]));

                    if (taskToUpdate is null)
                        return;

                    taskToUpdate.Description = args[2];
                    taskToUpdate.UpdateAt = DateTime.Now;

                    File.WriteAllText(SystemDatabase.FileName, JsonSerializer.Serialize(database, jsonOptions));
                }
                break;
            case "delete":
                if (args.Length >= 2)
                {
                    var database = GetDatabase();

                    var taskToDelete = database.Tasks.FirstOrDefault(x => x.Id == int.Parse(args[1]));

                    if (taskToDelete is null)
                        return;

                    database.Tasks.Remove(taskToDelete);

                    File.WriteAllText(SystemDatabase.FileName, JsonSerializer.Serialize(database, jsonOptions));
                }
                break;
            case "list":
                {
                    var database = GetDatabase();

                    foreach (var task in database.Tasks)
                    {
                        var sb = new StringBuilder();

                        var props = task.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                        foreach (var prop in props)
                            sb.Append($"{prop.Name}:'{prop.GetValue(task, null)}'  ");

                        Console.WriteLine(sb.ToString());
                    }

                    break;
                }
            case "mark-in-progress":
                {
                    var database = GetDatabase();

                    var taskToUpdate = database.Tasks.FirstOrDefault(x => x.Id == int.Parse(args[1]));

                    if (taskToUpdate is null)
                        return;

                    taskToUpdate.Status = Status.InProgress;

                    File.WriteAllText(SystemDatabase.FileName, JsonSerializer.Serialize(database, jsonOptions));

                    break;
                }
            case "mark-done":
                {
                    var database = GetDatabase();

                    var taskToUpdate = database.Tasks.FirstOrDefault(x => x.Id == int.Parse(args[1]));

                    if (taskToUpdate is null)
                        return;

                    taskToUpdate.Status = Status.Done;

                    File.WriteAllText(SystemDatabase.FileName, JsonSerializer.Serialize(database, jsonOptions));

                    break;
                }
            default:
                Console.WriteLine("Invalid command");
                break;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

// TODO centralized database write function

static SystemDatabase GetDatabase()
{
    try
    {
        if (!File.Exists(SystemDatabase.FileName))
            File.WriteAllText(SystemDatabase.FileName, string.Empty);

        var content = File.ReadAllText(SystemDatabase.FileName);
        // TODO melhorar essa chamada, carregar todas tasks em memória não é interessante
        var database = string.IsNullOrEmpty(content)
            ? new SystemDatabase()
            : JsonSerializer.Deserialize<SystemDatabase>(content);

        return database;
    }
    catch (Exception)
    {
        throw;
    }
}