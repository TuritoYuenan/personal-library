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
		string appearance = _settings.Appearance switch
		{
			InterfaceStyle.FlatDarkStyle => "Dark & Flat",
			InterfaceStyle.ShadedDarkStyle => "Dark & Shaded",
			InterfaceStyle.FlatLightStyle => "Light & Flat",
			InterfaceStyle.ShadedLightStyle => "Light & Shaded",
			InterfaceStyle.Bubble => "Bubbly",
			InterfaceStyle.BubbleMulticolored => "Bubbly & Colorful",
			_ => "Unknown"
		};

		SplashKit.SetInterfaceFontSize(20);
		SplashKit.StartInset("s1", new Rectangle() { X = 20, Y = 110, Width = 620, Height = 580 });

		_settings.Appearance = (InterfaceStyle)SplashKit.Slider(appearance, (float)_settings.Appearance, 0, 5);
		_settings.SavePath = SplashKit.TextBox("Save Location", _settings.SavePath);

		// Update SplashKit interface style
		SplashKit.SetInterfaceStyle(_settings.Appearance);

		SplashKit.EndInset("s1");

		SplashKit.StartInset("s2", new Rectangle() { X = 660, Y = 110, Width = 590, Height = 580 });

		SplashKit.EndInset("s2");
	}
}
