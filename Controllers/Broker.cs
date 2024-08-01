namespace PersonalLibrary.Controllers;

public static class Broker
{
	private static readonly Queue<KeyValuePair<string, object>> _queue = [];

	public static void Publish(string command, object data)
	{
		_queue.Enqueue(new(command, data));
	}

	public static (string, object) Poll()
	{
		if (_queue.Count == 0) return ("", new());
		(string command, object data) = _queue.Dequeue();
		return (command, data);
	}
}
