using Godot;

namespace ABAI;

public partial class PauseMenu : CanvasLayer
{
	private Control _root = null!;
	private Label _title = null!;
	private Button _resume = null!;
	private Button _save = null!;
	private Button _language = null!;
	private Button _menu = null!;
	private Locale _locale = null!;
	private SaveSystem _saveSystem = null!;
	private bool _open;

	public override void _Ready()
	{
		_locale = GetNode<Locale>("/root/Locale");
		_saveSystem = GetNode<SaveSystem>("/root/SaveSystem");
		_root = GetNode<Control>("Root");
		_title = GetNode<Label>("Root/Panel/VBox/Title");
		_resume = GetNode<Button>("Root/Panel/VBox/Resume");
		_save = GetNode<Button>("Root/Panel/VBox/Save");
		_language = GetNode<Button>("Root/Panel/VBox/Language");
		_menu = GetNode<Button>("Root/Panel/VBox/MainMenu");

		_resume.Pressed += Close;
		_save.Pressed += OnSave;
		_language.Pressed += () =>
		{
			_locale.ToggleLanguage();
			RefreshTexts();
		};
		_menu.Pressed += () =>
		{
			GetTree().Paused = false;
			GetTree().ChangeSceneToFile("res://ui/MainMenu.tscn");
		};

		_root.Visible = false;
		ProcessMode = ProcessModeEnum.Always;
		_root.ProcessMode = ProcessModeEnum.Always;
		Layer = 30;
		RefreshTexts();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("pause"))
		{
			if (_open)
				Close();
			else
				Open();
			GetViewport().SetInputAsHandled();
		}
	}

	private void Open()
	{
		_open = true;
		_root.Visible = true;
		GetTree().Paused = true;
		RefreshTexts();
	}

	private void Close()
	{
		_open = false;
		_root.Visible = false;
		GetTree().Paused = false;
	}

	private void OnSave()
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		var scene = GetTree().CurrentScene?.SceneFilePath ?? "res://maps/Village.tscn";
		var pos = player?.GlobalPosition ?? Vector2.Zero;
		_saveSystem.SaveGame(scene, pos);
	}

	private void RefreshTexts()
	{
		_title.Text = _locale.TrKey("ui.pause.title");
		_resume.Text = _locale.TrKey("ui.pause.resume");
		_save.Text = _locale.TrKey("ui.pause.save");
		_language.Text = _locale.TrKey("ui.pause.language");
		_menu.Text = _locale.TrKey("ui.pause.menu");
	}
}
