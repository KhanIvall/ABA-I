using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ABAI;

public enum QuestStatus
{
	Locked,
	Available,
	Active,
	Completed,
	Failed,
}

public enum QuestObjectiveType
{
	Talk,
	Reach,
	Flag,
}

public sealed class QuestObjectiveDef
{
	[JsonPropertyName("type")]
	public string Type { get; set; } = "talk";

	[JsonPropertyName("target")]
	public string Target { get; set; } = "";

	[JsonPropertyName("count")]
	public int Count { get; set; } = 1;
}

public sealed class QuestDef
{
	[JsonPropertyName("id")]
	public string Id { get; set; } = "";

	[JsonPropertyName("title_key")]
	public string TitleKey { get; set; } = "";

	[JsonPropertyName("desc_key")]
	public string DescKey { get; set; } = "";

	[JsonPropertyName("require_flags")]
	public Dictionary<string, int> RequireFlags { get; set; } = new();

	[JsonPropertyName("objectives")]
	public List<QuestObjectiveDef> Objectives { get; set; } = new();

	[JsonPropertyName("reward_flags")]
	public Dictionary<string, int> RewardFlags { get; set; } = new();

	[JsonPropertyName("unlocks")]
	public List<string> Unlocks { get; set; } = new();

	[JsonPropertyName("auto_start_when_available")]
	public bool AutoStartWhenAvailable { get; set; }
}

public sealed class QuestCatalog
{
	[JsonPropertyName("quests")]
	public List<QuestDef> Quests { get; set; } = new();
}

public sealed class QuestRuntime
{
	public QuestDef Def { get; init; } = null!;
	public QuestStatus Status { get; set; } = QuestStatus.Locked;
	public Dictionary<string, int> Progress { get; set; } = new();
}
