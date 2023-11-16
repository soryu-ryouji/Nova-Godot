using Godot;

namespace Nova;

public partial class AudioController : Node
{
	public string CurrentAudioName { get; private set; }
	private string lastAudioName;

	public bool Playing { get; private set; }
	private bool lastIsPlaying;

	[Export] public string AudioFolder;

	private UnifiedAudioStreamPlayer audioStreamPlayer;

	private float _scriptVolume;
	public float ScriptVolume
	{
		get => _scriptVolume;
		set
		{
			_scriptVolume = value;
			Init();
		}
	}

	private bool inited;

	private void Init()
	{
		if (inited) return;

        audioStreamPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

		inited = true;
	}

    public override void _Ready()
    {
        Init();
    }

	private void ForceUpdate()
	{
		AudioStream stream = null;
		AudioStream headStream = null;

		if (!string.IsNullOrEmpty(CurrentAudioName))
		{
			var audioPath = System.IO.Path.Combine("resources", AudioFolder, CurrentAudioName);
			stream = ResourceLoader.Load<AudioStream>(audioPath);
			if (stream != null)
			{
				// To reduce the possibility of discontinuity between head and loop,
				// we use the full audio as head
				headStream = ResourceLoader.Load<AudioStream>(audioPath);
			}
			else
			{
				stream = ResourceLoader.Load<AudioStream>(audioPath);
			}
		}

		if (CurrentAudioName != lastAudioName)
		{
			if (stream != null)
			{
				audioStreamPlayer.SetStream(stream, headStream);
				audioStreamPlayer.Play();
			}
			else
			{
				audioStreamPlayer.Stop();
				audioStreamPlayer.SetStream(null, null);
			}
		}
		else
		{
			audioStreamPlayer.Play();
		}

		lastAudioName = CurrentAudioName;
		lastIsPlaying = Playing;
	}

	public void Play(string audioName)
	{
		CurrentAudioName = audioName;
		Playing = true;
		ForceUpdate();
	}

	public void Stop()
	{
		CurrentAudioName = null;
		Playing = false;
		ForceUpdate();
	}

	public void Pause()
	{
		Playing = false;
		ForceUpdate();
	}

	public void UnPause()
	{
		Playing = true;
		ForceUpdate();
	}

	public void Preload(string audioName)
	{
		// To reduce the possibility of discontinuity between head and loop,
		// we use the full audio as head
		// var audioPath = System.IO.Path.Combine(audioFolder, audioName);
		// AssetLoader.Preload(AssetCacheType.Audio, audioPath);
		// AssetLoader.Preload(AssetCacheType.Audio, audioPath + "_loop");
	}

	public void Unpreload(string audioName)
	{
		// To reduce the possibility of discontinuity between head and loop,
		// we use the full audio as head
		// var audioPath = System.IO.Path.Combine(audioFolder, audioName);
		// AssetLoader.Unpreload(AssetCacheType.Audio, audioPath);
		// AssetLoader.Unpreload(AssetCacheType.Audio, audioPath + "_loop");
	}
}
