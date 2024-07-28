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

		SplashKit.StartInset("type", new Rectangle() { X = 20, Y = 110, Width = 900, Height = 45 });
		_form["type"] = SplashKit.Slider("Type", (int)_form["type"], 0, 4);
		SplashKit.EndInset("type");

		TextField(155, "Author(s)", "authors");
		TextField(200, "Title", "title");
		TextField(245, "Publisher", "publisher");
		TextField(290, "ID", "id");

		SplashKit.StartInset("year", new Rectangle() { X = 20, Y = 335, Width = 300, Height = 45 });
		_form["year"] = SplashKit.NumberBox("Year", (int)_form["year"], 1);
		SplashKit.EndInset("year");

		SplashKit.StartInset("month", new Rectangle() { X = 320, Y = 335, Width = 300, Height = 45 });
		_form["month"] = SplashKit.NumberBox("month", (int)_form["month"], 1);
		SplashKit.EndInset("month");

		SplashKit.StartInset("day", new Rectangle() { X = 620, Y = 335, Width = 300, Height = 45 });
		_form["day"] = SplashKit.NumberBox("day", (int)_form["day"], 1);
		SplashKit.EndInset("day");
	}

	private void TextField(uint y, string label, string key)
	{
		SplashKit.StartInset(key, new Rectangle() { X = 20, Y = y, Width = 900, Height = 45 });
		_form[key] = SplashKit.TextBox(label, (string)_form[key]);
		SplashKit.EndInset(key);
	}
}
