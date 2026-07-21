using Godot;

namespace ABAI;

public partial class MainMenu : Control
{
	private Locale _locale = null!;
	private SaveSystem _save = null!;
	private Button _newGame = null!;
	private Button _loadGame = null!;
	private Button _language = null!;
	private Button _quit = null!;
	private Button _protoSide = null!;
	private Button _protoTop = null!;
	private Label _title = null!;
	private Label _protoLabel = null!;

	public override void _Ready()
	{
		_locale = GetNode<Locale>("/root/Locale");
		_save = GetNode<SaveSystem>("/root/SaveSystem");

		_title = GetNode<Label>("Center/VBox/Title");
		_newGame = GetNode<Button>("Center/VBox/NewGame");
		_loadGame = GetNode<Button>("Center/VBox/LoadGame");
		_language = GetNode<Button>("Center/VBox/Language");
		_quit = GetNode<Button>("Center/VBox/Quit");
		_protoLabel = GetNode<Label>("Center/VBox/ProtoLabel");
		_protoSide = GetNode<Button>("Center/VBox/ProtoSide");
		_protoTop = GetNode<Button>("Center/VBox/ProtoTopDown");

		_newGame.Pressed += OnNewGame;
		_loadGame.Pressed += OnLoadGame;
		_language.Pressed += OnLanguage;
		_quit.Pressed += () => GetTree().Quit();
		_protoSide.Pressed += () => GetTree().ChangeSceneToFile("res://maps/PrototypeSide.tscn");
		_protoTop.Pressed += () => GetTree().ChangeSceneToFile("res://maps/PrototypeTopDown.tscn");

		_loadGame.Disabled = !_save.HasSave();
		_locale.LanguageChanged += OnLanguageChanged;
		RefreshTexts();
	}

	public override void _ExitTree()
	{
		if (_locale != null)
			_locale.LanguageChanged -= OnLanguageChanged;
	}

	private void OnLanguageChanged(string _) => RefreshTexts();

	private void OnNewGame()
	{
		_save.NewGame();
		GetTree().ChangeSceneToFile("res://maps/Village.tscn");
	}

	private void OnLoadGame()
	{
		if (!_save.TryLoadGame())
			return;

		var scene = string.IsNullOrEmpty(_save.LastScenePath)
			? "res://maps/Village.tscn"
			: _save.LastScenePath;
		GetTree().ChangeSceneToFile(scene);
	}

	private void OnLanguage()
	{
		_locale.ToggleLanguage();
	}

	private void RefreshTexts()
	{
		if (!IsInsideTree() || !IsInstanceValid(this))
			return;

		_title.Text = _locale.TrKey("ui.menu.title");
		_newGame.Text = _locale.TrKey("ui.menu.new_game");
		_loadGame.Text = _locale.TrKey("ui.menu.load_game");
		_language.Text = _locale.TrKey("ui.menu.language");
		_quit.Text = _locale.TrKey("ui.menu.quit");
		_protoLabel.Text = _locale.TrKey("ui.menu.prototypes");
		_protoSide.Text = _locale.TrKey("ui.menu.proto_side");
		_protoTop.Text = _locale.TrKey("ui.menu.proto_topdown");
	}
}
