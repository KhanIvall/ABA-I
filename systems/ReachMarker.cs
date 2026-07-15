using Godot;

namespace ABAI;

/// <summary>Reach objective trigger for the quest graph.</summary>
public partial class ReachMarker : Area2D
{
	[Export] public string MarkerId { get; set; } = "marker_overlook";

	private QuestManager _quests = null!;
	private bool _triggered;

	public override void _Ready()
	{
		_quests = GetNode<QuestManager>("/root/QuestManager");
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (_triggered || body is not PlayerController)
			return;
		_triggered = true;
		_quests.ReportReach(MarkerId);
	}
}
