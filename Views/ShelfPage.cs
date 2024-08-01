using PersonalLibrary.Controllers;
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
		SplashKit.FillQuad(Color.Brown, SplashKit.QuadFrom(
			x, y + 60,
			x + 96, y,
			x + 1216, y + 60,
			x + 1120, y
		));
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
		Rectangle coverImgBox = SplashKit.RectangleFrom(x + 30, y, 120, 170);
		SplashKit.FillRectangle(Color.Tomato, coverImgBox);

		SplashKit.SetInterfaceFontSize(20);
		SplashKit.Label(
			data.Authors[0],
			SplashKit.RectangleFrom(x, y + 190, 165, 30)
		);

		SplashKit.SetInterfaceFontSize(18);
		SplashKit.Paragraph(
			data.Title.Truncate(30),
			SplashKit.RectangleFrom(x + 5, y + 220, 165, 30)
		);

		if (
			SplashKit.MouseClicked(MouseButton.LeftButton) &&
			SplashKit.PointInRectangle(SplashKit.MousePosition(), coverImgBox)
		)
		{
			ToDoList.AddTask("go_into", new MaterialPage(data));
		}
	}
}
