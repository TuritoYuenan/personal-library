namespace PersonalLibrary.Controllers;

using PersonalLibrary.Views;

public class Navigator
{
	/// <summary>
	/// Represents page navigation
	/// </summary>
	/// <remarks>
	/// This stack-based approach is similar to how mobile apps work
	/// https://developer.android.com/guide/navigation/principles
	/// </remarks>
	private readonly Stack<IPage> _pages;

	/// <summary>
	/// Gets the page the user is currently in
	/// </summary>
	public IPage CurrentPage => _pages.Peek();

	/// <summary>
	/// Confirms if user is at the start page
	/// </summary>
	public bool IsStartPage => _pages.Count == 1;

	public Navigator()
	{
		_pages = [];
	}

	/// <summary>
	/// Goes into a new page. This can be triggered by clicking on a button or a card
	/// </summary>
	/// <param name="page">Page to go to</param>
	public void GoInto(IPage page) => _pages.Push(page);

	/// <summary>
	/// Goes back to the previous page. Similar to the Android back button
	/// </summary>
	/// <returns></returns>
	public IPage? GoBack()
	{
		if (IsStartPage) return null;
		return _pages.Pop();
	}
}
