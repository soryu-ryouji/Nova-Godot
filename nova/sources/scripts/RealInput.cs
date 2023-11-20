using Godot;

namespace Nova;

public partial class RealInput : Node
{
	private static RealInput Current;
	private Vector2 lastMousePosition;

	public static Vector2 MousePosition
	{
		get
		{
			return Current.lastMousePosition;
		}
	}

	public static Vector2 PointerPosition
	{
		get
		{
			return Current.lastMousePosition;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion) lastMousePosition = eventMouseMotion.Position;
	}

	public override void _Ready()
	{
		Current = this;
	}
}
