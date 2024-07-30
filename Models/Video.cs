using SplashKitSDK;

namespace PersonalLibrary.Models;

/// <summary>
/// Represents an online video (e.g. on YouTube/Vimeo/DailyMotion)
/// </summary>
public class YouTubeVideo : Material, IOnline
{
	/// <summary>
	/// Platform/Website where the video is uploaded
	/// </summary>
	public string Platform { get; set; }

	/// <summary>
	/// URL to where the video can be accessed
	/// </summary>
	public Uri Link { get; set; }

	public override string Id => Link.AbsoluteUri;

	public YouTubeVideo() : base()
	{
		Platform = "";
		Link = new("");
	}

	public YouTubeVideo(Json json) : base(json)
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

	public override Bitmap GetImage()
	{
		if (_image != null) { return _image; }
		_image = SplashKit.DownloadBitmap(Id, Link.Host.Replace("www.", "") switch
		{
			"youtube.com" => $"https://img.youtube.com/vi/{Link.Query.Replace("?watch=", "")}/default.jpg",
			"youtu.be" => $"https://img.youtube.com/vi/{Link.Segments[1]}/default.jpg",
			_ => throw new NotSupportedException("Unsupported URL")
		}, 443);
		return _image;
	}
}
