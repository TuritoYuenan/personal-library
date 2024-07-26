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
		SplashKit.SetInterfaceLabelWidth(200);
		while (!ui.Window.CloseRequested)
		{
			SplashKit.ProcessEvents();
			ui.Window.Clear(Color.White);

			// Temporary: open material
			if (SplashKit.KeyTyped(KeyCode.Num1Key)) { ui.GoInto(new MaterialPage(shelf[0])); }
			if (SplashKit.KeyTyped(KeyCode.Num2Key)) { ui.GoInto(new MaterialPage(shelf[1])); }
			if (SplashKit.KeyTyped(KeyCode.Num3Key)) { ui.GoInto(new MaterialPage(shelf[2])); }

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
			ui.Window.Refresh();
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

		shelf.Clear();
		foreach (Json item in contents)
		{
			Material material = CreateMaterial.FromJson(item);
			shelf.Add(material);
		}

		Json.FreeAll();
	}
}
