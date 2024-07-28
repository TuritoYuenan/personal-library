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
		Settings settings = Settings.GetInstance();
		UserInterface ui = UserInterface.GetInstance();
		Librarian librarian = new(shelf, settings, ui);

		try { librarian.Execute(); }
		catch (Exception e) { Console.Error.WriteLine(e.Message); }
	}
}
