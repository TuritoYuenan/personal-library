namespace PersonalLibrary.Controllers;

/// <summary>
/// Represents a list of what the program needs to do
/// </summary>
/// <remarks>The "list" is actually a message queue</remarks>
public static class Agenda
{
	private static readonly Queue<(string, object)> _tasks = [];

	public static void AddTask(string task, object attachment)
	{
		_tasks.Enqueue((task, attachment));
	}

	public static (string, object) GetTask()
	{
		if (_tasks.Count == 0) return ("no_task", new());
		return _tasks.Dequeue();
	}
}
