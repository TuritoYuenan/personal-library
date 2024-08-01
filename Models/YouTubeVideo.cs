using SplashKitSDK;

namespace PersonalLibrary.Models;

/// <summary>
/// Represents an online video (e.g. on YouTube/Vimeo/DailyMotion)
/// </summary>
public class YouTubeVideo : Material, IOnline
{
	/// <summary>
	/// YouTube channel that uploaded the video
	/// </summary>
	public string Channel { get; set; }

	public string VideoId { get; set; }

	/// <summary>
	/// URL to where the video can be accessed
	/// </summary>
	public Uri Link => new("https://youtu.be/" + VideoId);

	public override string Id => VideoId;

	public YouTubeVideo() : base()
	{
		Channel = "";
		VideoId = "";
	}

	public YouTubeVideo(Json json) : base(json)
	{
		Channel = json.ReadString("channel");
		VideoId = json.ReadString("id");
	}

	public override Json ToJson()
	{
		Json json = base.ToJson();
		json.AddString("type", "video");
		json.AddString("channel", Channel);
		json.AddString("id", VideoId);
		json.AddString("url", Link.AbsoluteUri);
		return json;
	}

	public override async Task<Bitmap> GetImage()
	{
		if (_image == null)
		{
			string url = $"https://img.youtube.com/vi/{VideoId}/hqdefault.jpg";
			Console.WriteLine($"Downloading thumbnail for video: {Title}");
			_image = await Task.Run(() => SplashKit.DownloadBitmap(Id, url, 443));
		}
		return _image;
	}
}
