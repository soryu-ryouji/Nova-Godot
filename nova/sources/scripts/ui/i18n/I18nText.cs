using Godot;

namespace Nova;

public partial class I18nText : Node
{
	[Export]
	public string inflateTextKey;

	private Label textLabel;
	private RichTextLabel richTextLabel;

	private TextProxy textProxy;

	public override void _Ready()
	{
		textLabel = GetNodeOrNull<Label>("Label");
		richTextLabel = GetNodeOrNull<RichTextLabel>("RichTextLabel");
		textProxy = GetNodeOrNull<TextProxy>("TextProxy");

		if (textLabel == null && richTextLabel == null && textProxy == null)
		{
			GD.PushWarning("Missing Label or RichTextLabel or TextProxy.");
		}

		I18n.LocaleChanged += UpdateText;
	}

	public override void _ExitTree()
	{
		I18n.LocaleChanged -= UpdateText;
	}

	private void UpdateText()
	{
		string text = I18n.__(inflateTextKey);
		if (textProxy != null)
		{
			textProxy.Text = text;
		}
		else if (richTextLabel != null)
		{
			richTextLabel.Text = text;
		}
		else
		{
			textLabel.Text = text;
		}
	}
}
