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

	public Book
	(
		List<string> author, string title, DateOnly date,
		string isbn, string publisher, string edition
	)
		: base(author, title, date)
	{
		Isbn = isbn;
		Edition = edition;
		Publisher = publisher;
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

	public override Bitmap GetPicture()
	{
		if (_picture == null)
		{
			Console.WriteLine($"Downloading cover for {Title}");
			_picture = SplashKit.DownloadBitmap(Id, $"https://covers.openlibrary.org/b/isbn/{Isbn}-M.jpg", 443);
		}
		return _picture;
	}
}
