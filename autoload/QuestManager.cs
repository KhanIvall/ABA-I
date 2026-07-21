using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Godot;

namespace ABAI;

/// <summary>Graph-based quest manager. Content-driven; story lives in JSON + locale keys.</summary>
public partial class QuestManager : Node
{
	public event Action<string, int>? QuestStatusChanged;
	public event Action? QuestsUpdated;

	private readonly Dictionary<string, QuestRuntime> _quests = new();
	private WorldFlags _flags = null!;

	public override void _Ready()
	{
		_flags = GetNode<WorldFlags>("/root/WorldFlags");
		_flags.FlagChanged += OnFlagChanged;
		LoadCatalog("res://content/quests/quests.json");
		RefreshAvailability();
	}

	public IEnumerable<QuestRuntime> All() => _quests.Values;

	public QuestRuntime? Get(string id) =>
		_quests.TryGetValue(id, out var q) ? q : null;

	public IEnumerable<QuestRuntime> ActiveQuests() =>
		_quests.Values.Where(q => q.Status == QuestStatus.Active);

	public void StartQuest(string id)
	{
		if (!_quests.TryGetValue(id, out var quest))
			return;
		if (quest.Status is not (QuestStatus.Available or QuestStatus.Locked))
			return;
		if (!_flags.MeetsAll(quest.Def.RequireFlags))
			return;

		quest.Status = QuestStatus.Active;
		quest.Progress.Clear();
		EmitStatus(quest);
		TryCompleteIfDone(quest);
	}

	public void ReportTalk(string npcId) =>
		ReportProgress(QuestObjectiveType.Talk, npcId, 1);

	public void ReportReach(string markerId) =>
		ReportProgress(QuestObjectiveType.Reach, markerId, 1);

	public void ResetAll()
	{
		foreach (var quest in _quests.Values)
		{
			quest.Status = QuestStatus.Locked;
			quest.Progress.Clear();
		}
		RefreshAvailability();
		QuestsUpdated?.Invoke();
	}

	public Dictionary<string, Godot.Collections.Dictionary> Snapshot()
	{
		var result = new Dictionary<string, Godot.Collections.Dictionary>();
		foreach (var pair in _quests)
		{
			var progress = new Godot.Collections.Dictionary();
			foreach (var p in pair.Value.Progress)
				progress[p.Key] = p.Value;

			result[pair.Key] = new Godot.Collections.Dictionary
			{
				["status"] = (int)pair.Value.Status,
				["progress"] = progress,
			};
		}
		return result;
	}

	public void LoadFrom(Godot.Collections.Dictionary data)
	{
		foreach (var quest in _quests.Values)
		{
			quest.Status = QuestStatus.Locked;
			quest.Progress.Clear();
		}

		foreach (var key in data.Keys)
		{
			var id = key.AsString();
			if (!_quests.TryGetValue(id, out var quest))
				continue;

			var entry = data[key].AsGodotDictionary();
			quest.Status = (QuestStatus)(int)entry["status"];
			quest.Progress.Clear();
			if (entry.ContainsKey("progress"))
			{
				var progress = entry["progress"].AsGodotDictionary();
				foreach (var pKey in progress.Keys)
					quest.Progress[pKey.AsString()] = (int)progress[pKey];
			}
		}

		RefreshAvailability();
		QuestsUpdated?.Invoke();
	}

	private void LoadCatalog(string path)
	{
		if (!FileAccess.FileExists(path))
		{
			GD.PushError($"Quest catalog missing: {path}");
			return;
		}

		using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		var json = file.GetAsText();
		var catalog = JsonSerializer.Deserialize<QuestCatalog>(json, new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
		});

		if (catalog == null)
			return;

		_quests.Clear();
		foreach (var def in catalog.Quests)
		{
			_quests[def.Id] = new QuestRuntime
			{
				Def = def,
				Status = QuestStatus.Locked,
			};
		}
	}

	private void OnFlagChanged(string key, int value) => RefreshAvailability();

	private void RefreshAvailability()
	{
		foreach (var quest in _quests.Values)
		{
			if (quest.Status is QuestStatus.Completed or QuestStatus.Failed or QuestStatus.Active)
				continue;

			var was = quest.Status;
			if (_flags.MeetsAll(quest.Def.RequireFlags))
			{
				quest.Status = QuestStatus.Available;
				if (was != QuestStatus.Available)
					EmitStatus(quest);

				if (quest.Def.AutoStartWhenAvailable)
					StartQuest(quest.Def.Id);
			}
			else
			{
				quest.Status = QuestStatus.Locked;
			}
		}

		QuestsUpdated?.Invoke();
	}

	private void ReportProgress(QuestObjectiveType type, string target, int amount)
	{
		foreach (var quest in ActiveQuests())
		{
			foreach (var objective in quest.Def.Objectives)
			{
				if (!Matches(objective, type, target))
					continue;

				var key = ProgressKey(objective);
				quest.Progress.TryGetValue(key, out var current);
				quest.Progress[key] = Math.Min(objective.Count, current + amount);
			}

			TryCompleteIfDone(quest);
		}

		QuestsUpdated?.Invoke();
	}

	private void TryCompleteIfDone(QuestRuntime quest)
	{
		if (quest.Status != QuestStatus.Active)
			return;

		foreach (var objective in quest.Def.Objectives)
		{
			if (ParseType(objective.Type) == QuestObjectiveType.Flag)
			{
				if (!_flags.Has(objective.Target, objective.Count))
					return;
				continue;
			}

			var key = ProgressKey(objective);
			quest.Progress.TryGetValue(key, out var current);
			if (current < objective.Count)
				return;
		}

		quest.Status = QuestStatus.Completed;
		_flags.Apply(quest.Def.RewardFlags);
		EmitStatus(quest);

		foreach (var unlockId in quest.Def.Unlocks)
		{
			if (_quests.TryGetValue(unlockId, out var unlocked) &&
				unlocked.Status == QuestStatus.Available &&
				unlocked.Def.AutoStartWhenAvailable)
			{
				StartQuest(unlockId);
			}
		}

		RefreshAvailability();
	}

	private static bool Matches(QuestObjectiveDef objective, QuestObjectiveType type, string target) =>
		ParseType(objective.Type) == type &&
		string.Equals(objective.Target, target, StringComparison.OrdinalIgnoreCase);

	private static string ProgressKey(QuestObjectiveDef objective) =>
		$"{objective.Type}:{objective.Target}";

	private static QuestObjectiveType ParseType(string type) =>
		type.ToLowerInvariant() switch
		{
			"talk" => QuestObjectiveType.Talk,
			"reach" => QuestObjectiveType.Reach,
			"flag" => QuestObjectiveType.Flag,
			_ => QuestObjectiveType.Talk,
		};

	private void EmitStatus(QuestRuntime quest) =>
		QuestStatusChanged?.Invoke(quest.Def.Id, (int)quest.Status);
}
