﻿using PersonalLibrary.Interfaces;
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
		_buttons = UserInterface.GetInstance().Buttons;
	}

	public void Render()
	{
		// Cover image
		SplashKit.FillRectangle(Color.LightGray, 148, 130, 360, 510);
		SplashKit.DrawBitmap(_material.GetImage(), 148, 130);

		// Date
		SplashKit.SetInterfaceFontSize(28);
		SplashKit.Label(_material.Date.ToString(), new() { X = 523, Y = 120, Width = 610, Height = 38 });

		// Title
		SplashKit.SetInterfaceFontSize(48);
		SplashKit.Paragraph(_material.Title, new() { X = 528, Y = 160, Width = 610, Height = 112 });

		// Authors
		SplashKit.SetInterfaceFontSize(20);
		SplashKit.Label(string.Join(" / ", _material.Authors), new() { X = 528, Y = 312, Width = 610, Height = 28 });

		// Display "View Online" button is material is available online
		if (_material is IOnline onlineMaterial)
		{
			_buttons["viewOnline"] = ViewOnlineButton(528, 575);
			if (_buttons["viewOnline"]) { ViewOnline(onlineMaterial!.Link); }
		}
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
		return SplashKit.Button("View Online", new Rectangle() { X = x, Y = y, Width = 186, Height = 60 });
	}

	/// <summary>
	/// Open a website using the default browser
	/// </summary>
	/// <param name="link">Address to where the material can be found online</param>
	private static Process? ViewOnline(Uri link)
	{
		ProcessStartInfo startInfo = new() { FileName = link.AbsoluteUri, UseShellExecute = true };
		return Process.Start(startInfo);
	}
}
