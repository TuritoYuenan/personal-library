using PersonalLibrary.Interfaces;
using SplashKitSDK;

namespace PersonalLibrary.Views;

/// <summary>
/// Represents the program's user interface
/// </summary>
public sealed class UserInterface
{
	/// <summary>
	/// Store the singular UserInterface object
	/// </summary>
	private static UserInterface? _instance;

	/// <summary>
	/// Padlock for thread-safe singleton solution
	/// </summary>
	private static readonly object _lock = new();

	/// <summary>
	/// Represents page navigation
	/// </summary>
	/// <remarks>
	/// This stack-based approach is similar to how mobile apps work
	/// https://developer.android.com/guide/navigation/principles
	/// </remarks>
	private readonly Stack<IPage> _pages;

	/// <summary>
	/// Program window, provided by SplashKit
	/// </summary>
	public Window Window { get; }

	/// <summary>
	/// Stores button states
	/// </summary>
	public Dictionary<string, bool> Buttons { get; }

	/// <summary>
	/// Gets the page the user is currently in
	/// </summary>
	public IPage CurrentPage => _pages.Peek();

	/// <summary>
	/// Confirms if user is at the start page
	/// </summary>
	public bool IsStartPage => _pages.Count == 1;

	/// <summary>
	/// Generate program user interface
	/// </summary>
	private UserInterface()
	{
		_pages = [];
		Window = new("Personal Library", 1280, 720) { Icon = new("icon", "icon.png") };
		Buttons = [];
	}

	public static UserInterface GetInstance()
	{
		lock (_lock)
		{
			_instance ??= new();
			return _instance;
		}
	}

	/// <summary>
	/// Goes into a new page. This can be triggered by clicking on a button or a card
	/// </summary>
	/// <param name="page">Page to go to</param>
	public void GoInto(IPage page) => _pages.Push(page);

	/// <summary>
	/// Goes back to the previous page. Similar to the Android back button
	/// </summary>
	/// <returns></returns>
	public IPage GoBack() => _pages.Pop();

	/// <summary>
	/// Render program user interface
	/// </summary>
	public void Render()
	{
		CurrentPage.Render();

		string title = CurrentPage.Title;
		SplashKit.SetInterfaceFontSize(50);
		SplashKit.Label(title, new() { X = 220, Y = 34, Height = 70, Width = 500 });

		Buttons["add"] = CreateButton.Icon(30, 30, "add");
		Buttons["settings"] = CreateButton.Icon(130, 30, "settings");

		if (Buttons["settings"]) { GoInto(new SettingsPage()); }

		SplashKit.DrawInterface();
	}
}
