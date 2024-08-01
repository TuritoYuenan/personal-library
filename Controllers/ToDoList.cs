namespace PersonalLibrary.Controllers;

/// <summary>
/// Represents a list of what the program needs to do
/// </summary>
/// <remarks>The "list" is actually a message queue
public static class ToDoList
{
	private static readonly Queue<KeyValuePair<string, object>> _tasks = [];

	public static void AddTask(string title, object attachment)
	{
		_tasks.Enqueue(new(title, attachment));
	}

	public static (string, object) GetTask()
	{
		if (_tasks.Count == 0) return ("no_task", new());
		(string command, object data) = _tasks.Dequeue();
		return (command, data);
	}
}
