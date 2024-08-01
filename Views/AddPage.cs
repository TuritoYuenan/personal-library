using PersonalLibrary.Controllers;
using PersonalLibrary.Models;
using SplashKitSDK;

namespace PersonalLibrary.Views;

public class AddPage : IPage
{
	private Dictionary<string, bool> _buttons;
	private Dictionary<string, string> _txt;
	private Dictionary<string, int> _num;
	private int _type;

	public string Title => "Add new " + TypeLabel();

	public AddPage()
	{
		_buttons = [];
		_txt = [];
		_txt["authors"] = "";
		_txt["title"] = "";
		_txt["pub"] = "";
		_txt["edition"] = "";
		_txt["id"] = "";
		_num = [];
		_num["year"] = 1900;
		_num["month"] = 1;
		_num["day"] = 1;
		_num["volume"] = -1;
		_num["issue"] = -1;
		_num["start"] = -1;
		_num["end"] = -1;
		_type = 0;
	}

	public void Render()
	{
		SplashKit.SetInterfaceFontSize(20);
		SplashKit.StartCustomLayout();

		SplashKit.SetInterfaceLabelWidth(200);
		SplashKit.StartInset("Type", SplashKit.RectangleFrom(320, 145, 600, 45));
		_type = (int)SplashKit.Slider("Type: " + TypeLabel(), _type, 0, 3);
		SplashKit.EndInset("Type");

		SplashKit.SetInterfaceLabelWidth(100);
		_txt["authors"] = TextField(320, 190, "Author(s)", _txt["authors"]);
		_txt["title"] = TextField(320, 235, "Title", _txt["title"]);
		_num["year"] = NumberField(320, 280, "Year", _num["year"]);
		_num["month"] = NumberField(520, 280, "Month", _num["month"]);
		_num["day"] = NumberField(720, 280, "Day", _num["day"]);

		if (_type == 0)
		{
			_txt["pub"] = TextField(320, 325, "Publisher", _txt["pub"]);
			_txt["edition"] = TextField(320, 370, "Edition", _txt["edition"]);
			_txt["id"] = TextField(320, 415, "ISBN", _txt["id"]);
		}
		if (_type == 1)
		{
			_txt["pub"] = TextField(320, 325, "Publisher", _txt["pub"]);
			_num["volume"] = NumberField(420, 415, "Volume", _num["volume"]);
			_num["issue"] = NumberField(620, 415, "Issue", _num["issue"]);
			_num["start"] = NumberField(420, 460, "Page", _num["start"]);
			_num["end"] = NumberField(620, 460, "Page", _num["end"]);
			_txt["id"] = TextField(320, 370, "DOI", _txt["id"]);
		}
		if (_type == 2)
		{
			_txt["pub"] = TextField(320, 325, "Website", _txt["pub"]);
			_txt["id"] = TextField(320, 370, "URL", _txt["id"]);
		}
		if (_type == 3)
		{
			_txt["pub"] = TextField(320, 325, "Channel", _txt["pub"]);
			_txt["id"] = TextField(320, 370, "ID", _txt["id"]);
		}

		_buttons["create"] = SplashKit.Button("Add to Shelf", SplashKit.RectangleFrom(520, 550, 200, 100));
		if (_buttons["create"])
		{
			try
			{
				Material material = BuildMaterial();
				ToDoList.AddTask("add_item", material);
			}
			catch (Exception e)
			{
				SplashKit.DisplayDialog("Wrong format", e.Message, SplashKit.GetSystemFont(), 20);
			}
		}
	}

	private static string TextField(int x, int y, string label, string value)
	{
		SplashKit.StartInset(label, SplashKit.RectangleFrom(x, y, 600, 45));
		value = SplashKit.TextBox(label, value);
		SplashKit.EndInset(label);
		return value;
	}

	private static int NumberField(int x, int y, string label, int value)
	{
		SplashKit.StartInset(label, SplashKit.RectangleFrom(x, y, 200, 45));
		value = (int)SplashKit.NumberBox(label, value, 1);
		SplashKit.EndInset(label);
		return value;
	}

	private string TypeLabel()
	{
		return _type switch
		{
			0 => "Book",
			1 => "Article",
			2 => "Webpage",
			3 => "YouTube Video",
			_ => "Material"
		};
	}

	private Material BuildMaterial()
	{
		Material material = _type switch
		{
			0 => new Book()
				.AddIsbn(_txt["id"])
				.AddEdition(_txt["edition"]),
			1 => new Article()
				.AddDoi(_txt["id"])
				.AddNumbers(_num["volume"], _num["issue"], _num["start"], _num["end"]),
			2 => new Webpage()
				.AddLink(_txt["id"]),
			3 => new YouTubeVideo()
				.AddVideoId(_txt["id"]),
			_ => throw new NotSupportedException("Unsupported material type")
		};
		return material
			.AddAuthors(_txt["authors"].Split([',', '/']).Select(author => author.Trim()).ToArray())
			.AddTitle(_txt["title"])
			.AddDate(_num["year"], _num["month"], _num["day"])
			.AddPublication(_txt["pub"]);
	}
}
