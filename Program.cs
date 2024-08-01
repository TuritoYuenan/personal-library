using PersonalLibrary.Models;
using PersonalLibrary.Views;
using PersonalLibrary.Controllers;

namespace PersonalLibrary;

/// <summary>
/// Main program class
/// </summary>
internal class Program
{
	public static void Main()
	{
		Shelf shelf = new();
		UserInterface ui = new();
		Librarian librarian = new(shelf, ui);

		try { librarian.Execute(); }
		catch (Exception e)
		{
			Console.Error.WriteLine(e.Message);
			UserInterface.ErrorDialog(e);
		}
	}
}
