using System.Text;
using Godot;

namespace ABAI;

public partial class Hud : CanvasLayer
{
	private Label _controls = null!;
	private Label _quests = null!;
	private Label _mode = null!;
	private Locale _locale = null!;
	private QuestManager _questsMgr = null!;
	private CameraModeService _camera = null!;

	public override void _Ready()
	{
		_locale = GetNode<Locale>("/root/Locale");
		_questsMgr = GetNode<QuestManager>("/root/QuestManager");
		_camera = GetNode<CameraModeService>("/root/CameraModeService");
		_controls = GetNode<Label>("Margin/VBox/Controls");
		_quests = GetNode<Label>("Margin/VBox/Quests");
		_mode = GetNode<Label>("Margin/VBox/Mode");

		_questsMgr.QuestsUpdated += Refresh;
		_camera.ModeChanged += OnModeChanged;
		_locale.LanguageChanged += OnLanguageChanged;
		Refresh();
		Layer = 10;
	}

	public override void _ExitTree()
	{
		if (_questsMgr != null)
			_questsMgr.QuestsUpdated -= Refresh;
		if (_camera != null)
			_camera.ModeChanged -= OnModeChanged;
		if (_locale != null)
			_locale.LanguageChanged -= OnLanguageChanged;
	}

	private void OnModeChanged(int _) => Refresh();
	private void OnLanguageChanged(string _) => Refresh();

	private void Refresh()
	{
		if (!IsInsideTree() || !IsInstanceValid(this))
			return;

		_controls.Text = _locale.TrKey("ui.hud.controls");

		var sb = new StringBuilder();
		sb.AppendLine(_locale.TrKey("ui.hud.quests") + ":");
		var any = false;
		foreach (var quest in _questsMgr.ActiveQuests())
		{
			any = true;
			sb.AppendLine("• " + _locale.TrKey(quest.Def.TitleKey));
			sb.AppendLine("  " + _locale.TrKey(quest.Def.DescKey));
		}

		foreach (var quest in _questsMgr.All())
		{
			if (quest.Status != QuestStatus.Available)
				continue;
			any = true;
			sb.AppendLine("○ " + _locale.TrKey(quest.Def.TitleKey));
			sb.AppendLine("  " + _locale.TrKey(quest.Def.DescKey));
		}

		if (!any)
			sb.AppendLine(_locale.TrKey("ui.hud.quests_none"));

		_quests.Text = sb.ToString().TrimEnd();
		_mode.Text = _camera.Mode == CameraMode.Side
			? _locale.TrKey("ui.hud.mode_side")
			: _locale.TrKey("ui.hud.mode_topdown");
	}
}
