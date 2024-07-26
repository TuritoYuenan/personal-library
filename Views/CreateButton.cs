using SplashKitSDK;

namespace PersonalLibrary.Views;

public static class CreateButton
{
	/// <summary>
	/// Render an icon button, mostly used in top bar
	/// </summary>
	/// <param name="x">X position</param>
	/// <param name="y">Y position</param>
	/// <param name="icon">Icon name (images/{icon}.png)</param>
	/// <returns>Whether the button is pressed</returns>
	public static bool Icon(int x, int y, string icon)
	{
		return SplashKit.BitmapButton(
			new Bitmap(icon, icon + ".png"),
			new Rectangle { X = x, Y = y, Width = 70, Height = 70 }
		);
	}

	/// <summary>
	/// A button with the label "View Online"
	/// </summary>
	/// <param name="x">X position</param>
	/// <param name="y">Y position</param>
	/// <returns>Whether the button is pressed</returns>
	public static bool ViewOnline(int x, int y)
	{
		SplashKit.SetInterfaceFontSize(20);
		return SplashKit.Button("View Online", new Rectangle() { X = x, Y = y, Width = 186, Height = 60 });
	}
}
