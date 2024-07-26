using PersonalLibrary.Interfaces;
using SplashKitSDK;

namespace PersonalLibrary.Models;

public class Webpage : Material, IOnline
{
	public Uri Link { get; set; }
	public string WebsiteName { get; set; }

	public override string Id => Link.AbsoluteUri;

	public Webpage
	(
		List<string> authors, string title, DateOnly date,
		string websiteName, string url
	)
		: base(authors, title, date)
	{
		WebsiteName = websiteName;
		Link = new(url);
	}

	public Webpage(Json json) : base(json)
	{
		WebsiteName = json.ReadString("website");
		Link = new(json.ReadString("url"));
	}

	public override Json ToJson()
	{
		Json json = base.ToJson();
		json.AddString("type", "webpage");
		json.AddString("website", WebsiteName);
		json.AddString("url", Link.AbsoluteUri);

		return json;
	}
}
