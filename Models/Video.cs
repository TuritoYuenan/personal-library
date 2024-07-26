using PersonalLibrary.Interfaces;
using SplashKitSDK;

namespace PersonalLibrary.Models;

public class Video : Material, IOnline
{
	public string Platform { get; set; }
	public Uri Link { get; set; }

	public override string Id => Link.AbsoluteUri;

	public Video
	(
		List<string> authors, string title, DateOnly date,
		string platform, string url
	)
		: base(authors, title, date)
	{
		Platform = platform;
		Link = new(url);
	}

	public Video(Json json) : base(json)
	{
		Platform = json.ReadString("platform");
		Link = new(json.ReadString("url"));
	}

	public override Json ToJson()
	{
		Json json = base.ToJson();
		json.AddString("type", "video");
		json.AddString("platform", Platform);
		json.AddString("url", Link.AbsoluteUri);
		return json;
	}
}
