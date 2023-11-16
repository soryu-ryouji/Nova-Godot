using Godot;

namespace Nova;

public partial class AudioLooper : Node
{
    private AudioStreamPlayer audioStreamPlayer;

    private AudioStreamPlayer headStreamPlayer;

    public AudioStream Stream => audioStreamPlayer.Stream;

    private bool headPaused;

    public void SetStream(AudioStream stream, AudioStream headStream)
    {
        audioStreamPlayer.Stream = stream;
        headStreamPlayer.Stream = headStream;
    }

    public float VolumeDb
    {
        get => audioStreamPlayer.VolumeDb;
        set
        {
            audioStreamPlayer.VolumeDb = value;
            headStreamPlayer.VolumeDb = value;
        }
    }

    public float PitchScale
    {
        get => audioStreamPlayer.PitchScale;
        set
        {
            audioStreamPlayer.PitchScale = value;
            headStreamPlayer.PitchScale = value;
        }
    }

    // TODO: Time Samples
    // public int timeSamples => audioSource.timeSamples;

    public bool IsPlaying => audioStreamPlayer.Playing || headStreamPlayer.Playing;

    public void Play()
    {
        var headStream = headStreamPlayer.Stream;
        if (headStream != null)
        {
            headStreamPlayer.Play();
            headStreamPlayer.Connect("finished", new Callable(this, nameof(OnHeadStreamFinished)));
        }
        else
        {
            audioStreamPlayer.Play();
        }
    }

    public void Stop()
    {
        audioStreamPlayer.Stop();
        headStreamPlayer.Stop();
    }

    public void Pause()
    {
        if (headStreamPlayer.Playing)
        {
            headStreamPlayer.StreamPaused = true;
            audioStreamPlayer.StreamPaused = true;
            headPaused = true;
        }
        else
        {
            audioStreamPlayer.StreamPaused = true;
            headPaused = false;
        }
    }

    public void UnPause()
    {
        if (headPaused)
        {
            headStreamPlayer.StreamPaused = false;
            audioStreamPlayer.StreamPaused = false;
        }
        else
        {
            audioStreamPlayer.StreamPaused = false;
        }
    }

    private void OnHeadStreamFinished()
    {
        audioStreamPlayer.Play();
    }
}