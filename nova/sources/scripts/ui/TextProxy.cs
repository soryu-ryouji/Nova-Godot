using Godot;

namespace Nova;

public partial class TextProxy : Control
{
    private RichTextLabel richText;

    private bool inited;
    private bool needRefreshLineBreak;
    private bool needRefreshFade;

    public void Init()
    {
        if (inited) return;
        richText = GetNode<RichTextLabel>("Label");
        inited = true;
    }

    public string Text
    {
        get => Text;
        set
        {
            if (Text == value) return;
            richText.Text = value;
            needRefreshLineBreak = true;
        }
    }
}