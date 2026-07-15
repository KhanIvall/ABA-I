using System.Text;
using Godot;

namespace ABAI;

public partial class Hud : CanvasLayer
{
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
		_quests = GetNode<Label>("Margin/VBox/Quests");
		_mode = GetNode<Label>("Margin/VBox/Mode");

		_questsMgr.QuestsUpdated += Refresh;
		_camera.ModeChanged += _ => Refresh();
		_locale.LanguageChanged += _ => Refresh();
		Refresh();
		Layer = 10;
	}

	private void Refresh()
	{
		var sb = new StringBuilder();
		sb.AppendLine(_locale.TrKey("ui.hud.quests") + ":");
		foreach (var quest in _questsMgr.ActiveQuests())
			sb.AppendLine("• " + _locale.TrKey(quest.Def.TitleKey));

		foreach (var quest in _questsMgr.All())
		{
			if (quest.Status == QuestStatus.Available)
				sb.AppendLine("○ " + _locale.TrKey(quest.Def.TitleKey));
		}

		_quests.Text = sb.ToString().TrimEnd();
		_mode.Text = _camera.Mode == CameraMode.Side
			? _locale.TrKey("ui.hud.mode_side")
			: _locale.TrKey("ui.hud.mode_topdown");
	}
}
