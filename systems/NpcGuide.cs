using Godot;

namespace ABAI;

public partial class NpcGuide : Area2D, IInteractable
{
	[Export] public string NpcId { get; set; } = "npc_guide";

	private DialogueUI? _dialogue;
	private QuestManager _quests = null!;
	private WorldFlags _flags = null!;
	private bool _busy;

	public override void _Ready()
	{
		_quests = GetNode<QuestManager>("/root/QuestManager");
		_flags = GetNode<WorldFlags>("/root/WorldFlags");
		_dialogue = GetTree().Root.FindChild("DialogueUI", true, false) as DialogueUI;
		BodyEntered += _ => { };
	}

	public void Interact(PlayerController player)
	{
		if (_busy)
			return;

		_dialogue ??= GetTree().Root.FindChild("DialogueUI", true, false) as DialogueUI;
		if (_dialogue == null || _dialogue.IsOpen)
			return;

		_busy = true;
		var keys = PickDialogueKeys();
		_dialogue.DialogueFinished += OnDialogueFinished;
		_dialogue.ShowKeys(keys);
	}

	private string[] PickDialogueKeys()
	{
		if (_flags.Has("slice_complete"))
			return new[] { "dialog.guide.done" };
		if (_flags.Has("seen_overlook"))
			return new[] { "dialog.guide.after_overlook" };
		if (_flags.Has("met_guide"))
			return new[] { "dialog.guide.after_meet" };
		return new[] { "dialog.guide.hello" };
	}

	private void OnDialogueFinished()
	{
		if (_dialogue != null)
			_dialogue.DialogueFinished -= OnDialogueFinished;
		_quests.ReportTalk(NpcId);
		_busy = false;
	}
}
