using System.Collections.Generic;
using Godot;

namespace ABAI;

/// <summary>Simple dialogue UI driven by locale keys (never hard-coded text).</summary>
public partial class DialogueUI : CanvasLayer
{
	private PanelContainer _panel = null!;
	private Label _label = null!;
	private Label _hint = null!;
	private Locale _locale = null!;
	private readonly Queue<string> _lines = new();
	private bool _open;

	public bool IsOpen => _open;

	[Signal]
	public delegate void DialogueFinishedEventHandler();

	public override void _Ready()
	{
		_locale = GetNode<Locale>("/root/Locale");
		_panel = GetNode<PanelContainer>("Panel");
		_label = GetNode<Label>("Panel/Margin/VBox/Text");
		_hint = GetNode<Label>("Panel/Margin/VBox/Hint");
		_panel.Visible = false;
		_locale.LanguageChanged += _ => RefreshLabels();
		Layer = 20;
	}

	public void ShowKeys(params string[] keys)
	{
		_lines.Clear();
		foreach (var key in keys)
			_lines.Enqueue(key);
		_open = true;
		_panel.Visible = true;
		ShowNext();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (!_open)
			return;
		if (@event.IsActionPressed("interact"))
		{
			ShowNext();
			GetViewport().SetInputAsHandled();
		}
	}

	private void ShowNext()
	{
		if (_lines.Count == 0)
		{
			Close();
			return;
		}

		_label.Text = _locale.TrKey(_lines.Dequeue());
		RefreshLabels();
	}

	private void Close()
	{
		_open = false;
		_panel.Visible = false;
		EmitSignal(SignalName.DialogueFinished);
	}

	private void RefreshLabels()
	{
		_hint.Text = _locale.TrKey("ui.dialogue.continue");
	}
}
