using PersonalLibrary.Controllers;
using SplashKitSDK;

namespace PersonalLibrary.Views;

/// <summary>
/// Represents the program's user interface
/// </summary>
public sealed class UserInterface
{
	/// <summary>
	/// Stores button states
	/// </summary>
	private Dictionary<string, bool> _buttons;

	/// <summary>
	/// Program window, provided by SplashKit
	/// </summary>
	public Window Window { get; }

	/// <summary>
	/// Generate program user interface
	/// </summary>
	public UserInterface()
	{
		Window = new("Personal Library", 1280, 720) { Icon = new("icon", "icon.png") };
		_buttons = [];
	}

	/// <summary>
	/// Render program user interface
	/// </summary>
	public void Render()
	{
		Window.Clear(Color.WhiteSmoke);
		Navigator.CurrentPage.Render();

		SplashKit.StartInset("title", SplashKit.RectangleFrom(200, 0, 1070, 100));
		SplashKit.SetInterfaceFontSize(50);
		SplashKit.Label(Navigator.CurrentPage.Title, SplashKit.RectangleFrom(10, 10, 800, 70));
		SplashKit.EndInset("title");

		_buttons["add"] = IconButton(0, 0, "add");
		_buttons["settings"] = IconButton(100, 0, "settings");

		if (_buttons["add"]) { Navigator.GoInto(new AddPage()); }
		if (_buttons["settings"]) { Navigator.GoInto(new SettingsPage()); }

		SplashKit.DrawInterface();
		Window.Refresh(20);
	}

	public static void ErrorDialog(Exception e)
	{
		SplashKit.DisplayDialog("Error", e.Message, SplashKit.GetSystemFont(), 10);
	}

	/// <summary>
	/// Renders an icon button
	/// </summary>
	/// <param name="icon">Icon name (images/{icon}.png)</param>
	/// <returns>Whether the button is pressed</returns>
	private static bool IconButton(int x, int y, string icon)
	{
		SplashKit.StartInset(icon, SplashKit.RectangleFrom(x, y, 100, 100));
		bool isPressed = SplashKit.BitmapButton(
			SplashKit.LoadBitmap(icon, icon + ".png"),
			SplashKit.RectangleFrom(15, 15, 60, 60)
		);
		SplashKit.EndInset(icon);
		return isPressed;
	}
}
