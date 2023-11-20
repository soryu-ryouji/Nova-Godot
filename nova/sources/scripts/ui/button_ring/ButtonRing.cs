using Godot;
using System;
using System.Collections.Generic;

namespace Nova;

public partial class ButtonRing : Control
{
    private List<ButtonRingItem> sectors = [];
    private Vector2 preCalculatedAnchorPos;
    private Control ring;
    private TextureRect buttonRingBackground;
    private Label actionNameText;

    public float innerRatio = 0.3f;
    private float _sectorRadius = 200.0f;
    public float angleOffset = -67.5f;
    private int selectedSectorIndex = -1;

    public override void _Ready()
    {
        ring = GetNode<Control>("Ring");
        buttonRingBackground = GetNode<TextureRect>("Ring/TextureRect");
        actionNameText = GetNode<Label>("Ring/Label");

        Init();
    }

    public override void _Process(double delta)
    {
        var pointerRelativeAngle = CalculatePointerRelative(out var distance);
        if (float.IsNaN(pointerRelativeAngle)) return;

        selectedSectorIndex = GetSectorIndexAtAngle(pointerRelativeAngle);
        SwitchSector(selectedSectorIndex + 1);
    }

    private void Init()
    {
        // godot的angle是以正x轴为0度，顺时针为正，逆时针为负
        // 为了方便后续根据sector index来调整button ring的显示，特意调整了sectors元素的添加顺序
        sectors.Add(new(" ", GD.Load<CompressedTexture2D>("res://nova/ui/sprites/button_ring/button_ring_0.png")));
        sectors.Add(new("7", GD.Load<CompressedTexture2D>("res://nova/ui/sprites/button_ring/button_ring_7.png")));
        sectors.Add(new("6", GD.Load<CompressedTexture2D>("res://nova/ui/sprites/button_ring/button_ring_6.png")));
        sectors.Add(new("5", GD.Load<CompressedTexture2D>("res://nova/ui/sprites/button_ring/button_ring_5.png")));
        sectors.Add(new("4", GD.Load<CompressedTexture2D>("res://nova/ui/sprites/button_ring/button_ring_4.png")));
        sectors.Add(new("3", GD.Load<CompressedTexture2D>("res://nova/ui/sprites/button_ring/button_ring_3.png")));
        sectors.Add(new("2", GD.Load<CompressedTexture2D>("res://nova/ui/sprites/button_ring/button_ring_2.png")));
        sectors.Add(new("1", GD.Load<CompressedTexture2D>("res://nova/ui/sprites/button_ring/button_ring_1.png")));
        sectors.Add(new("8", GD.Load<CompressedTexture2D>("res://nova/ui/sprites/button_ring/button_ring_8.png")));

        SwitchSector(0);
    }

    private void SwitchSector(int index)
    {
        if (index < 0 || index >= sectors.Count) return;

        buttonRingBackground.Texture = sectors[index].ActiveSprite;
        actionNameText.Text = sectors[index].ActionI18nName;
    }

    public void SetRingPosition(Vector2 position)
    {
        ring.Position = position - ring.PivotOffset;
    }

    /// <summary>
    /// return relative angle in deg, range from [0, 360), distance will be the distance from pointer to anchor point
    /// </summary>
    /// <returns>return relative angle in deg, range from [0, 360)</returns>
    public float CalculatePointerRelative(out float distance)
    {
        Vector2 pointerPos = RealInput.PointerPosition;
        // var anchorPos = preCalculatedAnchorPos;
        var anchorPos = ring.Position + ring.PivotOffset;
        var diff = pointerPos - anchorPos;
        distance = (float)Math.Sqrt(diff.X * diff.X + diff.Y * diff.Y);
        var angle = Mathf.Atan2(diff.Y, diff.X);
        if (angle < 0f)
        {
            // angle ranges in [0, 2 PI)
            angle = Mathf.Pi * 2f + angle;
        }

        // cvt to reg in [0, 360)
        angle = Mathf.RadToDeg(angle);

        return angle;
    }

    /// <summary>
    /// Relative angle between [0, 360)
    /// </summary>
    /// <param name="angle">relative angle</param>
    /// <returns>the index of sector being hovered. if no sector is hovered, return -1</returns>
    private int GetSectorIndexAtAngle(float angle)
    {
        var sectorRange = 360f / 8;
        var index = (int)(angle / sectorRange);
        if (index < 0)
        {
            index += sectors.Count;
        }

        return index;
    }


    public bool ButtonShowing { get; private set; }
	public bool HoldOpen { get; private set; }

    public void ShowRing()
    {
        if (ButtonShowing) return;

        AdjustAnchorPosition();
        ButtonShowing = true;
        Visible = true;
    }

    public void HideRing()
	{
		if (!ButtonShowing) return;

		HoldOpen = false;
		ButtonShowing = false;
		Visible = false;
	}

    private void AdjustAnchorPosition()
	{
		var currentPointerPos = RealInput.PointerPosition;
		SetRingPosition(currentPointerPos);
	}
}
