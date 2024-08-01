using PersonalLibrary.Models;
using SplashKitSDK;
using System.Diagnostics;

namespace PersonalLibrary.Views;

/// <summary>
/// Page to view details of a material item
/// </summary>
public class MaterialPage : IPage
{
	/// <summary>
	/// The material item to view
	/// </summary>
	private readonly Material _material;

	/// <summary>
	/// Stores button states
	/// </summary>
	private readonly Dictionary<string, bool> _buttons;

	public string Title => "View: " + _material.GetType().Name;

	public MaterialPage(Material material)
	{
		_material = material;
		_buttons = [];
	}

	public void Render()
	{
		SplashKit.StartInset("metadata", SplashKit.RectangleFrom(520, 125, 610, 510));

		// Date
		SplashKit.SetInterfaceFontSize(28);
		SplashKit.Label(_material.Date.ToString());

		// Title
		SplashKit.SetInterfaceFontSize(48);
		SplashKit.Paragraph(_material.Title.Truncate(70));

		// Authors
		SplashKit.SetInterfaceFontSize(20);
		SplashKit.Label("by " + string.Join(" / ", _material.Authors));

		// Publication
		SplashKit.Label("Published " + _material switch
		{
			Book book => $"by {book.Publisher}",
			Article article => $"in {article.Publisher}",
			Webpage webpage => $"on {webpage.Website}",
			YouTubeVideo video => $"by {video.Channel} on YouTube",
			_ => throw new NotImplementedException(),
		});

		// Misc
		SplashKit.Label(_material switch
		{
			Book book => $"Edition: {book.Edition}",
			Article article => $"Volume: {article.Numbers.Volume} | Issue: {article.Numbers.Issue} | Pages: {article.Numbers.Start} - {article.Numbers.End}",
			Webpage webpage => $"URL: {webpage.Link}",
			YouTubeVideo video => $"URL: {video.Link}",
			_ => throw new NotImplementedException(),
		});

		// ID
		SplashKit.Label(_material switch
		{
			Book book => $"ISBN: {book.Isbn}",
			Article article => $"DOI: {article.Doi}",
			Webpage => "",
			YouTubeVideo video => $"ID: {video.VideoId}",
			_ => throw new NotImplementedException(),
		});

		SplashKit.EndInset("metadata");

		// Display "View Online" button is material is available online
		if (_material is IOnline onlineMaterial)
		{
			_buttons["viewOnline"] = ViewOnlineButton(530, 565);
			if (_buttons["viewOnline"]) { ViewOnline(onlineMaterial!.Link); }
		}

		// Cover image
		Bitmap bmp = _material.GetImage().GetAwaiter().GetResult();
		SplashKit.FillRectangle(Color.LightGray, 148, 130, 360, 510);
		SplashKit.DrawBitmap(bmp, 148, 130);
	}

	/// <summary>
	/// A button with the label "View Online"
	/// </summary>
	/// <param name="x">X position</param>
	/// <param name="y">Y position</param>
	/// <returns>Whether the button is pressed</returns>
	private static bool ViewOnlineButton(int x, int y)
	{
		SplashKit.SetInterfaceFontSize(20);
		return SplashKit.Button("View Online", SplashKit.RectangleFrom(x, y, 186, 60));
	}

	/// <summary>
	/// Open a website using the default browser
	/// </summary>
	/// <param name="link">Address to where the material can be found online</param>
	private static Process? ViewOnline(Uri link)
	{
		ProcessStartInfo startInfo = new(link.AbsoluteUri) { UseShellExecute = true };
		return Process.Start(startInfo);
	}
}
