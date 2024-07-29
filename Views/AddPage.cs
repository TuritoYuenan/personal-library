using PersonalLibrary.Interfaces;
using PersonalLibrary.Models;
using SplashKitSDK;

namespace PersonalLibrary.Views;

public class AddPage : IPage
{
	private Dictionary<string, bool> _buttons;
	private int _type;
	private (string A, string T, string P, string I) _common;
	private (int Y, int M, int D) _date;
	private (int V, int I, int S, int E) _article;
	private string _bEdition;

	public string Title => "Add New " + TypeLabel();

	public AddPage()
	{
		_buttons = UserInterface.GetInstance().Buttons;
		_type = 0;
		_common = ("", "", "", "");
		_date = (2000, 1, 1);
		_bEdition = "";
		_article = (-1, -1, -1, -1);
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
		_common.A = TextField(320, 190, "Author(s)", _common.A);
		_common.T = TextField(320, 235, "Title", _common.T);
		_date.Y = NumberField(320, 280, "Year", _date.Y);
		_date.M = NumberField(520, 280, "Month", _date.M);
		_date.D = NumberField(720, 280, "Day", _date.D);

		if (_type == 0)
		{
			_common.P = TextField(320, 325, "Publisher", _common.P);
			_bEdition = TextField(320, 370, "Edition", _bEdition);
			_common.I = TextField(320, 415, "ISBN", _common.I);
		}

		if (_type == 1)
		{
			_common.P = TextField(320, 325, "Publisher", _common.P);
			_article.V = NumberField(420, 415, "Volume", _article.V);
			_article.I = NumberField(620, 415, "Issue", _article.I);
			_article.S = NumberField(420, 460, "Page", _article.S);
			_article.E = NumberField(620, 460, "Page", _article.E);
			_common.I = TextField(320, 370, "DOI", _common.I);
		}

		if (_type == 2 || _type == 3)
		{
			_common.P = TextField(320, 325, "Website", _common.P);
			_common.I = TextField(320, 370, "URL", _common.I);
		}

		_buttons["create"] = SplashKit.Button("Add to Shelf", SplashKit.RectangleFrom(520, 550, 200, 100));
		if (_buttons["create"]) BuildMaterial();
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
			3 => "Video",
			_ => "Material"
		};
	}

	private Material BuildMaterial()
	{
		var authors = _common.A.Split([',', '/']);
		return _type switch
		{
			0 => new Book()
				.AddIsbn(_common.I)
				.AddEdition(_bEdition)
				.AddPublication(_common.P)
				.AddDate(_date.Y, _date.M, _date.D)
				.AddTitle(_common.T)
				.AddAuthors(authors),
			1 => new Article()
				.AddDoi(_common.I)
				.AddNumbers(_article.V, _article.I, _article.S, _article.E)
				.AddPublication(_common.P)
				.AddDate(_date.Y, _date.M, _date.D)
				.AddTitle(_common.T)
				.AddAuthors(authors),
			2 => new Webpage()
				.AddLink(_common.I)
				.AddPublication(_common.P)
				.AddDate(_date.Y, _date.M, _date.D)
				.AddTitle(_common.T)
				.AddAuthors(authors),
			3 => new YouTubeVideo()
				.AddLink(_common.I)
				.AddPublication(_common.P)
				.AddDate(_date.Y, _date.M, _date.D)
				.AddTitle(_common.T)
				.AddAuthors(authors),
			_ => throw new NotSupportedException("Unknown type")
		};
	}
}
