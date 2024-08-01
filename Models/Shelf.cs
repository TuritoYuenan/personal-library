using SplashKitSDK;

namespace PersonalLibrary.Models;

/// <summary>
/// Represents a virtual shelf holding materials
/// </summary>
public class Shelf
{
	private readonly List<Material> _items;

	public Shelf()
	{
		_items = [];
	}

	public void Add(Material material) => _items.Add(material);
	public bool Remove(Material material) => _items.Remove(material);
	public void Clear() => _items.Clear();

	/// <summary>
	/// Get a rack from the shelf to display items
	/// </summary>
	/// <param name="n">Order of a rack (nth rack)</param>
	/// <returns>Rack containing 6 items max, 0 items min</returns>
	public List<Material> GetRack(int n)
	{
		int start = n * 6;
		if (start >= _items.Count) { return []; }

		int numberOfItems = Math.Min(6, _items.Count - start);
		return _items.GetRange(start, numberOfItems);
	}

	/// <summary>
	/// Export items from the shelf to JSON
	/// </summary>
	/// <returns>List of SplashKit JSON objects</returns>
	public List<Json> ToJsonList()
	{
		List<Json> saves = [];
		foreach (Material item in _items) { saves.Add(item.ToJson()); }
		return saves;
	}

	/// <summary>
	/// Import items from JSON to the shelf
	/// </summary>
	/// <param name="jsonList"></param>
	public void FromJsonList(List<Json> jsonList)
	{
		foreach (Json item in jsonList) { Add(CreateMaterial.FromJson(item)); }
	}
}
