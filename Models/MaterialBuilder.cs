using System.Text.RegularExpressions;

namespace PersonalLibrary.Models;

/// <summary>
/// Provides extension methods to build Material items, with custom data validation
/// </summary>
/// <remarks>Implements the Builder design pattern</remarks>
/// <see href="https://www.webdevtutor.net/blog/c-sharp-builder-pattern-extension-methods"/>
/// <remarks>Implement fluent interface</remarks>
/// <see href="https://en.wikipedia.org/wiki/Fluent_interface"/>
public static partial class MaterialBuilder
{
	public static Material AddAuthors(this Material material, params string[] authors)
	{
		material.Authors.AddRange(authors);
		return material;
	}

	public static Material AddTitle(this Material material, string title)
	{
		material.Title = title;
		return material;
	}

	public static Material AddDate(this Material material, int year, int month = 1, int day = 1)
	{
		if (1 > month || month > 12)
		{
			throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12");
		}
		if (1 > day || day > 31)
		{
			throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 31");
		}
		material.Date = new(year, month, day);
		return material;
	}

	public static Material AddPublication(this Material material, string publication)
	{
		if (material is Book book) book.Publisher = publication;
		if (material is Article article) article.Publisher = publication;
		if (material is Webpage webpage) webpage.Website = publication;
		if (material is YouTubeVideo video) video.Channel = publication;
		return material;
	}

	public static Book AddEdition(this Book book, string edition)
	{
		book.Edition = edition;
		return book;
	}

	public static Book AddIsbn(this Book book, string isbn)
	{
		if (!IsbnPattern().IsMatch(isbn))
		{
			throw new ArgumentException("The given ISBN is not valid", nameof(isbn));
		}
		book.Isbn = isbn;
		return book;
	}

	public static Article AddNumbers(this Article article, int volume = -1, int issue = -1, int start = -1, int end = -1)
	{
		article.Numbers = (volume, issue, start, end);
		return article;
	}

	public static Article AddDoi(this Article article, string doi)
	{
		if (!DoiPattern().IsMatch(doi))
		{
			throw new ArgumentException("The given DOI is not valid", nameof(doi));
		}
		article.Doi = doi;
		return article;
	}

	public static Webpage AddLink(this Webpage webpage, string url)
	{
		if (!UrlPattern().IsMatch(url))
		{
			throw new ArgumentException("The given URL is not valid");
		}
		webpage.Link = new(url);
		return webpage;
	}

	public static YouTubeVideo AddLink(this YouTubeVideo video, string url)
	{
		if (!UrlPattern().IsMatch(url))
		{
			throw new ArgumentException("The given URL is not valid");
		}
		video.Link = new(url);
		return video;
	}

	// https://www.geeksforgeeks.org/regular-expressions-to-validate-isbn-code
	[GeneratedRegex(@"^(?=(?:[^0-9]*[0-9]){10}(?:(?:[^0-9]*[0-9]){3})?$)[\d-]+$")]
	private static partial Regex IsbnPattern();

	// https://www.crossref.org/blog/dois-and-matching-regular-expressions
	[GeneratedRegex(@"^10.\d{4,9}/[-._;()/:A-Z0-9]+$")]
	private static partial Regex DoiPattern();

	// https://urlregex.com
	[GeneratedRegex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$")]
	private static partial Regex UrlPattern();
}
