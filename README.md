# School Assignment: Personal Library Program

> - **Assignment**: 6.5HD & 6.6HD - HD Level Custom Program Task
> - **Unit**: Object Oriented Programming (COS20007)
> - **Student**: Nguyen Ta Minh Triet
> - **Date**: 08 July 2024 - 02 August 2024
> - **School**: Swinburne University of Technology (HCMC Campus, Vietnam)

This is a program that you can use to manage your materials i.e. books, articles (journal/magazine), webpages, and YouTube™ videos.

## Features

- Add items (books, articles, webpages, YouTube™ videos) to the store in the shelf
- Save and load items and settings via JSON file
- GUI supported by SplashKit
- Customise GUI appearance and location to save JSON file via program settings
- View details of saved items, with cover image (book) and thumbnail (YouTube™ video) automatically received from public APIs

## How to use

### Download and run the program

There are two ways to download this program into your computer:

1. Dev mode: Clone this repository, then enter the repository's directory via shell and run `dotnet restore` and `dotnet watch`
2. User mode: Download the executable from the Releases section and run it in your Windows computer (untested)

### Using the program

1. Upon staring, you will be met with the Shelf page
2. Add a new material item by pressing on the top left button, use the slider to choose which type of material to add, enter the informations, and press the "Add to Shelf" button
	- For the text field, type in the infos. Because of SplashKit's current limitation, cutting/copying/pasting of text is not yet supported
	- For the number field, click on the number boxes and drag mouse left or right to change the numbers
3. Change program settings by pressing on the second to top left button. You can change UI theme and save location from here
4. Save your items and settings by pressing Ctrl + S
5. To load your items from a saved file, type the location in Settings (relative/absolute) and press Ctrl + L
6. Go back to the previous page by pressing Esc
7. Quit the program by pressing Ctrl + Q (unsaved items will be lost)

## Example materials to test Add functionality

> Note: Because of SplashKit's current limitation, you cannot cut/copy/paste text

- Book:
	- Authors: Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides
	- Title: Design Patterns: Elements of Reusable Object-Oriented Software
	- Date: 1994-10-21
	- Publisher: Addison-Wesley
	- Edition: Original
	- Isbn: 0-201-63361-2
- Book: https://amazon.com/Cosmos-Possible-Worlds-Ann-Druyan/dp/1426219083
	- Authors: Ann Druyan
	- Title: Cosmos: Possible Worlds
	- Date: 2020-02-01
	- Publisher: National Geographic
	- Edition: Original
	- ISBN: 1426219083
- Article: https://doi.org/10.1214/13-STS452
	- Authors: John M. Chambers
	- Title: Object-Oriented Programming, Functional Programming and R
	- Date: 2014-05-01
	- Publisher: Statist. Sci
	- Volume: 29
	- Issue: 2
	- Pages: 167 - 180
	- DOI: 10.1214/13-STS452
- Webpage: https://wikipedia.org/wiki/Uranus
	- Authors: Wikipedia contributors
	- Title: Uranus
	- Date: 2024-07-31
	- Website: Wikipedia, The Free Encyclopedia
	- URL: https://wikipedia.org/wiki/Uranus
- YouTube™ Video: https://youtu.be/mScpHTIi-kM
	- Authors: Derek Muller
	- Title: What Game Theory Reveals About Life, The Universe, and Everything
	- Date: 2023-12-24
	- Channel: Veritasium
	- ID: mScpHTIi-kM
