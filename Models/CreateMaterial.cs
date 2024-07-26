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
			"video" => new Video(json),
			_ => throw new InvalidDataException("Unknown type: " + type)
		};
	}

	public static Material TestMaterial()
	{
		return new Webpage(["Tester"], "Example page", DateOnly.MaxValue, "Example", "https://example.com");
	}
}
