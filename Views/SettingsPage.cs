using PersonalLibrary.Interfaces;
using PersonalLibrary.Models;
using SplashKitSDK;

namespace PersonalLibrary.Views;

/// <summary>
/// Page to view program settings
/// </summary>
public class SettingsPage : IPage
{
	private readonly Settings _settings;

	public string Title => "Settings";

	public SettingsPage()
	{
		_settings = Settings.GetInstance();
	}

	public void Render()
	{
		string mode = _settings.DarkMode switch
		{
			true => "Dark mode",
			false => "Light mode",
		};

		SplashKit.SetInterfaceFontSize(20);
		SplashKit.StartInset("s1", new Rectangle() { X = 30, Y = 140, Width = 1210, Height = 720 - 180 });

		SplashKit.StartCustomLayout();
		SplashKit.SplitIntoColumns(2);
		_settings.DarkMode = SplashKit.Checkbox("Appearance", mode, _settings.DarkMode);
		_settings.SavePath = SplashKit.TextBox("Save Location", _settings.SavePath);

		SplashKit.EndInset("s1");
	}
}
