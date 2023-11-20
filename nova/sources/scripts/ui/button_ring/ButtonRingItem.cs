using Godot;

namespace Nova;

public struct ButtonRingItem(string actionI18nName, CompressedTexture2D activeSprite)
{
    public CompressedTexture2D ActiveSprite = activeSprite;
    public string ActionI18nName = actionI18nName;
}
