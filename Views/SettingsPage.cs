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
		SplashKit.SetInterfaceLabelWidth(200);
		SplashKit.StartInset("s1", SplashKit.RectangleFrom(0, 100, 690, 600));

		Heading("Appearance");
		SplashKit.SetInterfaceFontSize(20);
		_settings.Appearance = (InterfaceStyle)SplashKit.Slider(
			AppearanceLabel(), (uint)_settings.Appearance, 0, 5
		);

		Heading("Saving & Loading");
		SplashKit.SetInterfaceFontSize(20);
		_settings.SavePath = SplashKit.TextBox(
			"Save Location", _settings.SavePath
		);

		SplashKit.EndInset("s1");

		SplashKit.StartInset("about", SplashKit.RectangleFrom(700, 100, 570, 600));

		Heading("About");
		SplashKit.SetInterfaceFontSize(20);
		SplashKit.Label("Personal Library Program");
		SplashKit.Label("Author: Nguyen Ta Minh Triet (Freshman@Swinburne)");
		SplashKit.Label("Contact: 104993913@student.swin.edu.au");
		SplashKit.Label("Website: http://turitoyuenan.proton.me");
		SplashKit.Label("GitHub Repository: TuritoYuenan/personal-library");

		SplashKit.EndInset("about");
	}

	private static void Heading(string heading)
	{
		SplashKit.SetInterfaceFontSize(30);
		SplashKit.Label(heading);
	}

	private string AppearanceLabel()
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
}
