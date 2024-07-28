using PersonalLibrary.Interfaces;
using SplashKitSDK;

namespace PersonalLibrary.Views;

public class AddPage : IPage
{
	private readonly Dictionary<string, object> _form;

	public string Title => "Add new material";

	public AddPage()
	{
		_form = [];
		_form["authors"] = "";
		_form["title"] = "";
		_form["publisher"] = "";
		_form["type"] = 0;
		_form["year"] = 2000;
		_form["month"] = 12;
		_form["day"] = 1;
		_form["id"] = "";
	}

	public void Render()
	{
		SplashKit.SetInterfaceLabelWidth(100);
		SplashKit.SetInterfaceFontSize(20);
		SplashKit.StartCustomLayout();

		SplashKit.StartInset("type", SplashKit.RectangleFrom(20, 110, 900, 45));
		_form["type"] = (int)SplashKit.Slider("Type", (int)_form["type"], 0, 4);
		SplashKit.EndInset("type");

		TextField(20, 155, "Author(s)", "authors");
		TextField(20, 200, "Title", "title");
		TextField(20, 245, "Publisher", "publisher");
		TextField(20, 290, "ID", "id");
		NumberField(20, 335, "Year", "year");
		NumberField(320, 335, "Month", "month");
		NumberField(620, 335, "Day", "day");
	}

	private void TextField(int x, int y, string label, string key)
	{
		SplashKit.StartInset(key, SplashKit.RectangleFrom(x, y, 900, 45));
		_form[key] = SplashKit.TextBox(label, (string)_form[key]);
		SplashKit.EndInset(key);
	}

	private void NumberField(int x, int y, string label, string key)
	{
		SplashKit.StartInset(key, SplashKit.RectangleFrom(x, y, 300, 45));
		_form[key] = (int)SplashKit.NumberBox(label, (int)_form[key], 1);
		SplashKit.EndInset(key);
	}
}
