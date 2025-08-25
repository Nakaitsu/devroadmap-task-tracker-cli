using System.Data;
using System.IO.Pipes;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Task_Tracker;

try
{
    if (args.Length > 0)
    {
        switch (args[0].Trim())
        {
            case "add":
                if (args.Length >= 2)
                {
                    var database = SystemDatabase.GetDatabase();

                    var newTask = new Task_Tracker.Task(args[1].Trim());
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

                    if (!int.TryParse(args[1].Trim(), out var taskId))
                        throw new ArgumentException("The id value is invalid!");

                    var taskToUpdate = database.Tasks.FirstOrDefault(x => x.Id == taskId)
                        ?? throw new Exception($"No task was found for the provided id: {taskId}");

                    taskToUpdate.Description = args[2].Trim();
                    taskToUpdate.UpdateAt = DateTime.Now;

                    database.Save();
                }
                break;
            case "delete":
                if (args.Length >= 2)
                {
                    var database = SystemDatabase.GetDatabase();

                    if (!int.TryParse(args[1].Trim(), out var taskId))
                        throw new ArgumentException("The id value is invalid!");

                    var taskToDelete = database.Tasks.FirstOrDefault(x => x.Id == taskId)
                        ?? throw new Exception($"No task was found for the provided id: {taskId}");

                    database.Tasks.Remove(taskToDelete);
                    database.Save();
                }
                break;
            case "list":
                {
                    var database = SystemDatabase.GetDatabase();
                    var tasks = database.Tasks;

                    if(args.Length >= 2)
                    {
                        var filteredTasks = args[1].Trim() switch
                        {
                            "done" => database.Tasks.Where(x => x.Status == Status.Done).ToList(),
                            "todo" => database.Tasks.Where(x => x.Status == Status.Todo).ToList(),
                            "in-progress" => database.Tasks.Where(x => x.Status == Status.InProgress).ToList(),
                            _ => database.Tasks
                        };

                        tasks = filteredTasks;
                    }

                    foreach (var task in tasks)
                    {
                        var sb = new StringBuilder();

                        var props = task.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

                        foreach (var prop in props)
                            sb.Append($"{prop.Name}:'{prop.GetValue(task, null)}' | ");

                        Console.WriteLine(sb.ToString());
                    }

                    break;
                }
            case "mark-in-progress":
                {
                    var database = SystemDatabase.GetDatabase();

                    if (!int.TryParse(args[1].Trim(), out var taskId))
                        throw new ArgumentException("The id value is invalid!");

                    var taskToUpdate = database.Tasks.FirstOrDefault(x => x.Id == taskId)
                        ?? throw new Exception($"No task was found for the provided id: {taskId}");

                    taskToUpdate.Status = Status.InProgress;

                    database.Save();

                    break;
                }
            case "mark-done":
                {
                    var database = SystemDatabase.GetDatabase();

                    if (!int.TryParse(args[1].Trim(), out var taskId))
                        throw new ArgumentException("The id value is invalid!");

                    var taskToUpdate = database.Tasks.FirstOrDefault(x => x.Id == taskId)
                        ?? throw new Exception($"No task was found for the provided id: {taskId}");

                    taskToUpdate.Status = Status.Done;

                    database.Save();

                    break;
                }
            default:
                Console.WriteLine("Unkown command");
                break;
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Somenthing went wrong! {ex.Message}");
}