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

	private string InterfaceStyleLabel()
	{
		return _settings.Appearance switch
		{
			InterfaceStyle.FlatDarkStyle => "Dark & Flat",
			InterfaceStyle.ShadedDarkStyle => "Dark & Shaded",
			InterfaceStyle.FlatLightStyle => "Light & Flat",
			InterfaceStyle.ShadedLightStyle => "Light & Shaded",
			InterfaceStyle.Bubble => "Bubbly",
			InterfaceStyle.BubbleMulticolored => "Bubbly & Colorful",
			_ => "Unknown"
		};
	}

	public void Render()
	{
		SplashKit.SetInterfaceLabelWidth(200);
		SplashKit.SetInterfaceFontSize(20);
		SplashKit.StartInset("s1", new Rectangle() { X = 20, Y = 110, Width = 680, Height = 580 });

		_settings.Appearance = (InterfaceStyle)SplashKit.Slider(
			InterfaceStyleLabel(), (uint)_settings.Appearance, 0, 5
		);
		_settings.SavePath = SplashKit.TextBox("Save Location", _settings.SavePath);

		// Update SplashKit interface style
		SplashKit.SetInterfaceStyle(_settings.Appearance);

		SplashKit.EndInset("s1");

		SplashKit.StartInset("about", new Rectangle() { X = 720, Y = 110, Width = 530, Height = 580 });

		SplashKit.SetInterfaceFontSize(40);
		SplashKit.Label("Personal Library Program");
		SplashKit.SetInterfaceFontSize(20);
		SplashKit.Label("Author: Nguyen Ta Minh Triet (Freshman@Swinburne)");
		SplashKit.Label("Contact: <104993913@student.swin.edu.au>");
		SplashKit.Label("Website: http://turitoyuenan.proton.me");
		SplashKit.Label("GitHub Repository: TuritoYuenan/personal-library");

		SplashKit.EndInset("about");
	}
}
