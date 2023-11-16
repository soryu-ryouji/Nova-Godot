using Godot;

namespace Nova;

public partial class UISound : Control
{
	[Export] public string audioName;

    [Export] public AudioManager audioManager;

	public override void _Ready()
	{
		MouseEntered += MouseEnter;
		MouseExited += MouseExit;
	}

	private void MouseEnter()
	{
		audioManager.Play(AudioType.UISound, audioName);
	}

	private void MouseExit()
	{

	}
}
