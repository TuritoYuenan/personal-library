using SplashKitSDK;

namespace PersonalLibrary.Models;

/// <summary>
/// Represents a book
/// </summary>
public class Book : Material
{
	public string Isbn { get; set; }
	public string Edition { get; set; }
	public string Publisher { get; set; }

	public override string Id => Isbn;

	public Book() : base()
	{
		Isbn = "";
		Edition = "";
		Publisher = "";
	}

	public Book(Json json) : base(json)
	{
		Publisher = json.ReadString("publisher");
		Edition = json.ReadString("edition");
		Isbn = json.ReadString("isbn");
	}

	public override Json ToJson()
	{
		Json json = base.ToJson();
		json.AddString("type", "book");
		json.AddString("publisher", Publisher);
		json.AddString("edition", Edition);
		json.AddString("isbn", Isbn);

		return json;
	}

	public override async Task<Bitmap> GetImage()
	{
		if (_image == null)
		{
			string url = $"https://covers.openlibrary.org/b/isbn/{Isbn}-L.jpg";
			Console.WriteLine($"Downloading cover for {Title}");
			_image = await Task.Run(() => SplashKit.DownloadBitmap(Id, url, 443));
		}
		return _image;
	}
}
