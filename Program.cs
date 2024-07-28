using SplashKitSDK;
using PersonalLibrary.Models;
using PersonalLibrary.Views;

namespace PersonalLibrary;

/// <summary>
/// Main program class
/// </summary>
internal class Program
{
	public static void Main()
	{
		Shelf shelf = new();
		Settings settings = Settings.GetInstance();
		UserInterface ui = UserInterface.GetInstance();

		// Set the first page as shelf page
		ui.GoInto(new ShelfPage(shelf));

		SplashKit.SetInterfaceFont(SplashKit.GetSystemFont());
		while (!ui.Window.CloseRequested)
		{
			SplashKit.ProcessEvents();
			ui.Window.Clear(Color.White);

			if (SplashKit.KeyTyped(KeyCode.SKey))
			{
				SaveData(shelf, settings);
			}

			if (SplashKit.KeyTyped(KeyCode.LKey))
			{
				try { LoadData(shelf, settings); }
				catch (Exception e) { Console.Error.WriteLine("Load error: {0}", e.Message); }
			}

			if (SplashKit.KeyTyped(KeyCode.EscapeKey))
			{
				if (ui.IsStartPage) { ui.Window.Close(); }
				else { ui.GoBack(); }
			}

			ui.Render();
			ui.Window.Refresh(20);
		}
	}

	/// <summary>
	/// Save program data to a JSON file
	/// </summary>
	/// <param name="shelf">Shelf containing materials</param>
	/// <param name="settings">Program settings</param>
	private static void SaveData(Shelf shelf, Settings settings)
	{
		// Create JSON object
		Json json = new();

		json.AddArray("contents", shelf.ToJsonList());
		json.AddObject("settings", settings.ToJson());

		string jsonString = Json.ToJsonString(json);
		File.WriteAllTextAsync(settings.SavePath, jsonString);

		Json.FreeAll();
	}

	/// <summary>
	/// Load program data from a JSON file
	/// </summary>
	/// <param name="shelf">Shelf to load materials into</param>
	/// <param name="settings">Program settings</param>
	private static async void LoadData(Shelf shelf, Settings settings)
	{
		// Load JSON from saved file
		string jsonString = await File.ReadAllTextAsync(settings.SavePath);
		Json json = Json.FromJsonString(jsonString);

		// Load settings
		settings.FromJson(json.ReadObject("settings"));

		// Load contents from save file
		List<Json> contents = [];
		json.ReadArray("contents", ref contents);
		shelf.FromJsonList(contents);

		Json.FreeAll();
	}
}
