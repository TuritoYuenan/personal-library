using SplashKitSDK;

namespace PersonalLibrary.Models;

public abstract class Material
{
	public List<string> Authors { get; set; }
	public string Title { get; set; }
	public DateOnly Date { get; set; }

	/// <summary>
	/// Gets a string used to identify a library material
	/// </summary>
	/// <returns>ID string. This can be ISBN, DOI, or URL</returns>
	public abstract string Id { get; }

	protected Material(List<string> authors, string title, DateOnly date)
	{
		Authors = authors;
		Title = title;
		Date = date;
	}

	/// <summary>
	/// Constructs a Material object from a JSON object
	/// </summary>
	/// <param name="json">SplashKit JSON object</param>
	protected Material(Json json)
	{
		List<string> temp = [];
		json.ReadArray("authors", ref temp);

		Authors = [.. temp];
		Title = json.ReadString("title");
		Date = DateOnly.Parse(json.ReadString("date"));
	}

	/// <summary>
	/// Converts a Material object to a JSON object
	/// </summary>
	/// <returns>SplashKit JSON object</returns>
	public virtual Json ToJson()
	{
		Json json = new();
		json.AddArray("authors", Authors);
		json.AddString("title", Title);
		json.AddString("date", Date.ToString());
		return json;
	}
}
