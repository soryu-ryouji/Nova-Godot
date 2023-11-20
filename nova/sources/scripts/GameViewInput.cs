using Godot;

namespace Nova;

public partial class GameViewInput : Node
{
	[Export] private ButtonRing buttonRing;
	public override void _Process(double delta)
	{
		HandleShortcut();
	}

	private void HandleShortcut()
	{
		if (buttonRing.ButtonShowing)
		{
		}
	}

    public override void _Input(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.Pressed && eventMouseButton.ButtonIndex == MouseButton.Right) buttonRing.ShowRing();
			if (eventMouseButton.Pressed && eventMouseButton.ButtonIndex == MouseButton.Left) buttonRing.HideRing();
		}
    }
}
