namespace PersonalLibrary.Interfaces;

/// <summary>
/// Represents a page that contains a title and a renderable interface
/// </summary>
public interface IPage
{
	/// <summary>
	/// Page title
	/// </summary>
	public string Title { get; }

	/// <summary>
	/// Procedure to render the page
	/// </summary>
	public void Render();
}
