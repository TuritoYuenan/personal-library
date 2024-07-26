using ANumbers = (int Volume, int Issue, int Start, int End);
using PersonalLibrary.Interfaces;
using SplashKitSDK;

namespace PersonalLibrary.Models;

/// <summary>
/// Represents an article published in a science journal or magazine
/// </summary>
public class Article : Material, IOnline
{
	/// <summary>
	/// Digital Object Identifier
	/// </summary>
	public string Doi { get; set; }

	/// <summary>
	/// Name of the publisher (Journal/Magazine)
	/// </summary>
	public string Publisher { get; set; }

	/// <summary>
	/// Volume, issue, start-end page
	/// </summary>
	public ANumbers Numbers { get; set; }

	/// <summary>
	/// Online permalink generated from the DOI
	/// </summary>
	public Uri Link => new("https://doi.org/" + Doi);

	public override string Id => Doi;

	public Article
	(
		List<string> author, string title, DateOnly date,
		string journal, ANumbers numbers, string doi
	)
		: base(author, title, date)
	{
		Doi = doi;
		Publisher = journal;
		Numbers = numbers;
	}

	public Article(Json json) : base(json)
	{
		Doi = json.ReadString("doi");
		Publisher = json.ReadString("journal");
		Numbers = (
			json.ReadInteger("volume"),
			json.ReadInteger("issue"),
			json.ReadInteger("page1"),
			json.ReadInteger("page2")
		);
	}

	public override Json ToJson()
	{
		Json json = base.ToJson();
		json.AddString("type", "article");
		json.AddString("doi", Doi);
		json.AddString("journal", Publisher);
		json.AddNumber("volume", Numbers.Volume);
		json.AddNumber("issue", Numbers.Issue);
		json.AddNumber("page1", Numbers.Start);
		json.AddNumber("page2", Numbers.End);

		return json;
	}
}
