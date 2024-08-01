﻿using SplashKitSDK;

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

	/// <summary>
	/// URL to where the video can be accessed
	/// </summary>
	public Uri Link { get; set; }

	public override string Id => Link.AbsoluteUri;

	public YouTubeVideo() : base()
	{
		Channel = "";
		Link = new("");
	}

	public YouTubeVideo(Json json) : base(json)
	{
		Channel = json.ReadString("channel");
		Link = new(json.ReadString("url"));
	}

	public override Json ToJson()
	{
		Json json = base.ToJson();
		json.AddString("type", "video");
		json.AddString("channel", Channel);
		json.AddString("url", Link.AbsoluteUri);
		return json;
	}

	public override async Task<Bitmap> GetImage()
	{
		if (_image == null)
		{
			string url = Link.Host.Replace("www.", "") switch
			{
				"youtube.com" => $"https://img.youtube.com/vi/{Link.Query.Replace("?watch=", "")}/default.jpg",
				"youtu.be" => $"https://img.youtube.com/vi/{Link.Segments[1]}/default.jpg",
				_ => throw new NotSupportedException("Unsupported URL")
			};
			Console.WriteLine($"Downloading thumbnail for {Title}");
			_image = await Task.Run(() => SplashKit.DownloadBitmap(Id, url, 443));
		}
		return _image;
	}
}