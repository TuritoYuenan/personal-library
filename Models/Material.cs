using SplashKitSDK;

namespace PersonalLibrary.Models;

public abstract class Material
{
	protected Bitmap? _image;

	/// <summary>
	/// List of authors
	/// </summary>
	public List<string> Authors { get; set; }

	/// <summary>
	/// Title of the material item
	/// </summary>
	public string Title { get; set; }

	/// <summary>
	/// Publication/Upload date
	/// </summary>
	public DateOnly Date { get; set; }

	/// <summary>
	/// Gets a string used to identify a library material
	/// </summary>
	/// <returns>ID string. This can be ISBN, DOI, or URL</returns>
	public abstract string Id { get; }

	/// <summary>
	/// Default constructor
	/// </summary>
	protected Material()
	{
		Authors = [];
		Title = "";
		Date = DateOnly.MinValue;
	}

	/// <summary>
	/// Constructs a material item from a JSON object
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
	/// Converts a material item to a JSON object
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

	public abstract Task<Bitmap> GetImage();
}
