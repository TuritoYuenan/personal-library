using SplashKitSDK;

namespace PersonalLibrary.Models;

/// <summary>
/// Represents program settings. Is a singleton (only one object created)
/// </summary>
public sealed class Settings
{
	/// <summary>
	/// Store the singular Settings object
	/// </summary>
	private static Settings? _instance;

	/// <summary>
	/// Padlock for thread-safe singleton solution
	/// </summary>
	private static readonly object _lock = new();

	public bool DarkMode { get; set; }
	public string SavePath { get; set; }

	private Settings()
	{
		DarkMode = false;
		SavePath = "data.json";
	}

	/// <summary>
	/// Access program settings
	/// </summary>
	/// <returns>Existing or new Settings object</returns>
	public static Settings GetInstance()
	{
		lock (_lock)
		{
			_instance ??= new();
			return _instance;
		}
	}

	/// <summary>
	/// Export settings to JSON
	/// </summary>
	/// <returns>SplashKit JSON object</returns>
	public Json ToJson()
	{
		Json json = new();
		json.AddBool("darkMode", DarkMode);
		json.AddString("savePath", SavePath);
		return json;
	}

	/// <summary>
	/// Load settings from JSON
	/// </summary>
	/// <param name="json">SplashKit JSON object</param>
	public void FromJson(Json json)
	{
		DarkMode = json.ReadBool("darkMode");
		SavePath = json.ReadString("savePath");
	}
}
