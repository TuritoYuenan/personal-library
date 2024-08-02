using PersonalLibrary.Models;
using PersonalLibrary.Views;
using SplashKitSDK;

namespace PersonalLibrary.Controllers;

/// <summary>
/// Represents the library's main controller
/// </summary>
public class Librarian
{
	private readonly Shelf _shelf;
	private readonly Settings _settings;
	private readonly UserInterface _ui;

	public Librarian(Shelf shelf, UserInterface ui)
	{
		_shelf = shelf;
		_settings = Settings.GetInstance();
		_ui = ui;
	}

	public void Execute()
	{
		SplashKit.SetInterfaceFont(SplashKit.GetSystemFont());
		SplashKit.SetInterfaceBorderColor(Color.Transparent);

		_ui.Navigator.GoInto(new ShelfPage(_shelf));

		while (!_ui.Window.CloseRequested)
		{
			SplashKit.SetInterfaceStyle(_settings.Appearance);
			SplashKit.ProcessEvents();

			// Handle keybinds
			if (SplashKit.KeyTyped(KeyCode.EscapeKey)) _ui.Navigator.GoBack();
			if (SplashKit.KeyDown(KeyCode.LeftCtrlKey))
			{
				if (SplashKit.KeyTyped(KeyCode.QKey)) _ui.Window.Close();
				if (SplashKit.KeyTyped(KeyCode.SKey)) SaveData();
				if (SplashKit.KeyTyped(KeyCode.LKey)) LoadData();
			}

			// Perform tasks from to-do list
			(string task, object attachment) = Agenda.GetTask();
			if (task == "add_item" && attachment is Material material)
			{
				_shelf.Add(material);
				while (!_ui.Navigator.IsStartPage) _ui.Navigator.GoBack();
			}
			if (task == "go_into" && attachment is IPage page)
			{
				_ui.Navigator.GoInto(page);
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
		string savePath = _settings.SavePath ?? "data.json";
		File.WriteAllTextAsync(savePath, Json.ToJsonString(json));

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
