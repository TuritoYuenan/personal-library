using SplashKitSDK;

namespace PersonalLibrary.Models;

public static class CreateMaterial
{
	/// <summary>
	/// Create a material from json
	/// </summary>
	/// <param name="json">SplashKit JSON object</param>
	/// <returns>A material item of specific type</returns>
	/// <exception cref="InvalidDataException">JSON "type" key is invalid</exception>
	public static Material FromJson(Json json)
	{
		string type = json.ReadString("type");
		return type switch
		{
			"book" => new Book(json),
			"article" => new Article(json),
			"webpage" => new Webpage(json),
			"video" => new YouTubeVideo(json),
			_ => throw new InvalidDataException("Unknown type: " + type)
		};
	}

	public static Material TestMaterial()
	{
		return new Book()
		{
			Authors = ["Erich Gamma", "Richard Helm", "Ralph Johnson", "John Vlissides"],
			Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
			Date = new(1994, 10, 21),
			Publisher = "Addison-Wesley",
			Edition = "Original",
			Isbn = "0-201-63361-2"
		};
	}
}
