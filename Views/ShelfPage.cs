using PersonalLibrary.Interfaces;
using PersonalLibrary.Models;
using SplashKitSDK;

namespace PersonalLibrary.Views;

/// <summary>
/// Page to view library materials
/// </summary>
public class ShelfPage : IPage
{
	/// <summary>
	/// A virtual shelf containing material items
	/// </summary>
	private readonly Shelf _shelf;

	public string Title => "Shelf";

	public ShelfPage(Shelf shelf)
	{
		_shelf = shelf;
	}

	public void Render()
	{
		// Render graphical shelves
		ShelfRack(32, 255);
		ShelfRack(32, 565);

		// Display materials on upper rack
		List<Material> rack1 = _shelf.GetRack(0);
		for (int i = 0; i < rack1.Count; i++)
		{
			MaterialCard(96 + 180 * i, 130, rack1[i]);
		}

		// Display materials on lower rack
		List<Material> rack2 = _shelf.GetRack(1);
		for (int i = 0; i < rack2.Count; i++)
		{
			MaterialCard(96 + 180 * i, 440, rack2[i]);
		}
	}

	/// <summary>
	/// Renders a graphical rack on the shelf
	/// </summary>
	/// <param name="x">X position</param>
	/// <param name="y">Y position</param>
	private static void ShelfRack(int x, int y)
	{
		Point2D[] points = [
			new() { X = x, Y = y + 60 },
			new() { X = x + 96, Y = y },
			new() { X = x + 1216, Y = y + 60 },
			new() { X = x + 1120, Y = y },
		];
		SplashKit.FillQuad(Color.Brown, new() { Points = points });
	}

	/// <summary>
	/// Renders a card showing a material item, with width 180 and height 270
	/// </summary>
	/// <param name="x">X position</param>
	/// <param name="y">Y position</param>
	/// <param name="data">Material data</param>
	private static void MaterialCard(int x, int y, Material data)
	{
		// Bounding box: w = 180, w = 270
		Rectangle coverImgBox = new() { Height = 170, Width = 120, X = x + 30, Y = y };
		SplashKit.FillRectangle(Color.Tomato, coverImgBox);

		SplashKit.SetInterfaceFontSize(20);
		SplashKit.Label(data.Authors[0], new() { X = x, Y = y + 190, Height = 30, Width = 165 });

		SplashKit.SetInterfaceFontSize(18);
		SplashKit.Paragraph(
			data.Title.Truncate(30),
			new() { X = x + 5, Y = y + 220, Height = 30, Width = 165 }
		);

		if (
			SplashKit.MouseClicked(MouseButton.LeftButton) &&
			SplashKit.PointInRectangle(SplashKit.MousePosition(), coverImgBox)
		)
		{
			UserInterface.GetInstance().GoInto(new MaterialPage(data));
		}
	}
}
