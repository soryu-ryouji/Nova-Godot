using Godot;

namespace Nova;

public enum AudioType
{
	BGM,
	BGS,
	UISound
}

public partial class AudioManager : Node
{
	private AudioController bgmController;
	private AudioController bgsController;
	private AudioController uiSoundController;

	private void Init()
	{
		bgmController = GetNode<AudioController>("BGM");
		bgsController = GetNode<AudioController>("BGS");
		uiSoundController = GetNode<AudioController>("UISound");

        if (bgmController != null || bgsController != null || uiSoundController != null)
        {
            GD.Print("AudioManager: init success");
        }
        else
        {
            GD.PrintErr("AudioManager init failed");
        }
	}

    public override void _Ready()
    {
        Init();
    }

	public void Play(AudioType playerType, string audioName)
	{
		switch (playerType)
		{
			case AudioType.BGM: bgmController.Play(audioName); break;
			case AudioType.BGS: bgsController.Play(audioName); break;
			case AudioType.UISound: uiSoundController.Play(audioName); break;
		}
	}
}
