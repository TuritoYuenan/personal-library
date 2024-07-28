using PersonalLibrary.Models;
using PersonalLibrary.Views;
using SplashKitSDK;

namespace PersonalLibrary.Controllers;

public class Librarian
{
	private readonly Shelf _shelf;
	private readonly Settings _settings;
	private readonly UserInterface _ui;

	public Librarian(Shelf shelf, Settings settings, UserInterface ui)
	{
		_shelf = shelf;
		_settings = settings;
		_ui = ui;
	}

	public void Execute()
	{
		SplashKit.SetInterfaceFont(SplashKit.GetSystemFont());
		SplashKit.SetInterfaceStyle(_settings.Appearance);

		_ui.GoInto(new ShelfPage(_shelf));

		while (!_ui.Window.CloseRequested)
		{
			SplashKit.ProcessEvents();

			if (SplashKit.KeyTyped(KeyCode.SKey)) SaveData();
			if (SplashKit.KeyTyped(KeyCode.LKey)) LoadData();
			if (SplashKit.KeyTyped(KeyCode.EscapeKey))
			{
				if (_ui.IsStartPage) { _ui.Window.Close(); }
				else { _ui.GoBack(); }
			}

			_ui.Render();
		}
	}

	/// <summary>
	/// Save program data to a JSON file
	/// </summary>
	private void SaveData()
	{
		// Create JSON object
		Json json = new();

		// Save items and settings
		json.AddArray("contents", _shelf.ToJsonList());
		json.AddObject("settings", _settings.ToJson());

		// Save JSON to save file
		File.WriteAllTextAsync(_settings.SavePath, Json.ToJsonString(json));

		// Free JSON resource
		Json.FreeAll();
	}

	/// <summary>
	/// Load program data from a JSON file
	/// </summary>
	private async void LoadData()
	{
		// Load JSON from save file
		Json json = Json.FromJsonString(await File.ReadAllTextAsync(_settings.SavePath));

		// Load array from JSON
		List<Json> items = [];
		json.ReadArray("contents", ref items);

		// Load items and settings
		_shelf.FromJsonList(items);
		_settings.FromJson(json.ReadObject("settings"));

		// Free JSON resource
		Json.FreeAll();
	}
}
