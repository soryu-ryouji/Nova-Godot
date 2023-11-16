using Godot;

namespace Nova;

public partial class UnifiedAudioStreamPlayer : Node
{
    public readonly AudioStreamPlayer audioStreamPlayer;
    public readonly AudioLooper audioLooper;

    public static implicit operator UnifiedAudioStreamPlayer(AudioStreamPlayer audioStreamPlayer) => new(audioStreamPlayer);

    public static implicit operator UnifiedAudioStreamPlayer(AudioLooper audioLooper) => new(audioLooper);

    public UnifiedAudioStreamPlayer(AudioStreamPlayer audioStreamPlayer)
    {
        this.audioStreamPlayer = audioStreamPlayer;
    }

    public UnifiedAudioStreamPlayer(AudioLooper audioLooper)
    {
        this.audioLooper = audioLooper;
    }

    public AudioStream Stream => audioLooper != null ? audioLooper.Stream : audioStreamPlayer.Stream;

    public void SetStream(AudioStream stream, AudioStream headClip)
    {
        if (audioLooper != null) audioLooper.SetStream(stream, headClip);
        else audioStreamPlayer.Stream = stream;
    }

    public float VolumeDb
    {
        get => audioLooper?.VolumeDb ?? audioStreamPlayer.VolumeDb;
        set
        {
            if (audioLooper != null) audioLooper.VolumeDb = value;
            else audioStreamPlayer.VolumeDb = value;
        }
    }

    public float PitchScale
    {
        get => audioLooper?.PitchScale ?? audioStreamPlayer.PitchScale;
        set
        {
            if (audioLooper != null) audioLooper.PitchScale = value;
            else audioStreamPlayer.PitchScale = value;
        }
    }

    public void Play()
    {
        if (audioLooper != null) audioLooper.Play();
        else audioStreamPlayer.Play();
    }

    public void Stop()
    {
        if (audioLooper != null) audioLooper.Stop();
        else audioStreamPlayer.Stop();
    }

    public void Pause()
    {
        if (audioLooper != null) audioLooper.Pause();
        else audioStreamPlayer.StreamPaused = true;
    }

    public void UnPause()
    {
        if (audioLooper != null) audioLooper.UnPause();
        else audioStreamPlayer.StreamPaused = false;
    }
}