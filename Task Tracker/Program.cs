using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Task_Tracker;

try
{
    if (args.Length > 0)
    {
        switch (args[0])
        {
            case "add":
                if (args.Length >= 2)
                {
                    var newTask = new Task_Tracker.Task
                    {
                        Description = args[1]
                    };

                    var database = SystemDatabase.GetDatabase();

                    newTask.Id = ++database.LastInsertedId;
                    database.Tasks.Add(newTask);

                    database.Save();

                    Console.WriteLine($"Task added successfully (ID: {newTask.Id})");
                }

                break;
            case "update":
                if (args.Length >= 3)
                {
                    var database = SystemDatabase.GetDatabase();

                    var taskToUpdate = database.Tasks.FirstOrDefault(x => x.Id == int.Parse(args[1]));

                    if (taskToUpdate is null)
                        return;

                    taskToUpdate.Description = args[2];
                    taskToUpdate.UpdateAt = DateTime.Now;

                    database.Save();
                }
                break;
            case "delete":
                if (args.Length >= 2)
                {
                    var database = SystemDatabase.GetDatabase();

                    var taskToDelete = database.Tasks.FirstOrDefault(x => x.Id == int.Parse(args[1]));

                    if (taskToDelete is null)
                        return;

                    database.Tasks.Remove(taskToDelete);
                    database.Save();
                }
                break;
            case "list":
                {
                    var database = SystemDatabase.GetDatabase();

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
                    var database = SystemDatabase.GetDatabase();

                    var taskToUpdate = database.Tasks.FirstOrDefault(x => x.Id == int.Parse(args[1]));

                    if (taskToUpdate is null)
                        return;

                    taskToUpdate.Status = Status.InProgress;

                    database.Save();

                    break;
                }
            case "mark-done":
                {
                    var database = SystemDatabase.GetDatabase();

                    var taskToUpdate = database.Tasks.FirstOrDefault(x => x.Id == int.Parse(args[1]));

                    if (taskToUpdate is null)
                        return;

                    taskToUpdate.Status = Status.Done;

                    database.Save();

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