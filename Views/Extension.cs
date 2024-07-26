namespace PersonalLibrary.Views;

public static class Extension
{
	public static string Truncate(this string value, int max)
	{
		if (value.Length <= max) { return value; }
		else { return string.Concat(value.AsSpan(0, max), "..."); }
	}
}
