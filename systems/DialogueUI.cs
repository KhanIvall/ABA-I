using System;
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

	public event Action? DialogueFinished;

	public override void _Ready()
	{
		_locale = GetNode<Locale>("/root/Locale");
		_panel = GetNode<PanelContainer>("Panel");
		_label = GetNode<Label>("Panel/Margin/VBox/Text");
		_hint = GetNode<Label>("Panel/Margin/VBox/Hint");
		_panel.Visible = false;
		_locale.LanguageChanged += OnLanguageChanged;
		Layer = 20;
		ProcessMode = ProcessModeEnum.Always;
	}

	public override void _ExitTree()
	{
		if (_locale != null)
			_locale.LanguageChanged -= OnLanguageChanged;
	}

	private void OnLanguageChanged(string _) => RefreshLabels();

	public void ShowKeys(params string[] keys)
	{
		_lines.Clear();
		foreach (var key in keys)
			_lines.Enqueue(key);
		_open = true;
		_panel.Visible = true;
		ShowNext();
	}

	/// <summary>
	/// Use _Input (not unhandled) so continue works even if the player also listens for E.
	/// </summary>
	public override void _Input(InputEvent @event)
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
		DialogueFinished?.Invoke();
	}

	private void RefreshLabels()
	{
		if (!IsInsideTree() || !IsInstanceValid(this) || _hint == null)
			return;
		_hint.Text = _locale.TrKey("ui.dialogue.continue");
	}
}
