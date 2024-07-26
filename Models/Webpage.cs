using PersonalLibrary.Interfaces;
using SplashKitSDK;

namespace PersonalLibrary.Models;

/// <summary>
/// Represent a webpage (blog, online news, someone's profile, etc)
/// </summary>
public class Webpage : Material, IOnline
{
	/// <summary>
	/// Name of the website
	/// </summary>
	public string Website { get; set; }

	/// <summary>
	/// URL to where the webpage can be accessed
	/// </summary>
	public Uri Link { get; set; }

	public override string Id => Link.AbsoluteUri;

	public Webpage
	(
		List<string> authors, string title, DateOnly date,
		string websiteName, string url
	)
		: base(authors, title, date)
	{
		Website = websiteName;
		Link = new(url);
	}

	public Webpage(Json json) : base(json)
	{
		Website = json.ReadString("website");
		Link = new(json.ReadString("url"));
	}

	public override Json ToJson()
	{
		Json json = base.ToJson();
		json.AddString("type", "webpage");
		json.AddString("website", Website);
		json.AddString("url", Link.AbsoluteUri);

		return json;
	}

	public override Bitmap GetPicture()
	{
		return new("webpage", "settings.png");
	}
}
