using System.Collections.Generic;
using Godot;

namespace ABAI;

/// <summary>Local JSON save to user:// (maps to AppData on Windows).</summary>
public partial class SaveSystem : Node
{
	private const string SlotPath = "user://save_slot_0.json";

	private WorldFlags _flags = null!;
	private QuestManager _quests = null!;
	private Locale _locale = null!;
	private CameraModeService _camera = null!;

	public string LastScenePath { get; private set; } = "res://maps/Village.tscn";
	public Vector2 LastPlayerPosition { get; private set; } = Vector2.Zero;
	public bool HasPendingLoad { get; private set; }

	public override void _Ready()
	{
		_flags = GetNode<WorldFlags>("/root/WorldFlags");
		_quests = GetNode<QuestManager>("/root/QuestManager");
		_locale = GetNode<Locale>("/root/Locale");
		_camera = GetNode<CameraModeService>("/root/CameraModeService");
	}

	public bool HasSave() => FileAccess.FileExists(SlotPath);

	public void SaveGame(string scenePath, Vector2 playerPosition)
	{
		LastScenePath = scenePath;
		LastPlayerPosition = playerPosition;

		var flagsDict = new Godot.Collections.Dictionary();
		foreach (var pair in _flags.Snapshot())
			flagsDict[pair.Key] = pair.Value;

		var questsDict = new Godot.Collections.Dictionary();
		foreach (var pair in _quests.Snapshot())
			questsDict[pair.Key] = pair.Value;

		var data = new Godot.Collections.Dictionary
		{
			["version"] = 1,
			["scene"] = scenePath,
			["player_x"] = playerPosition.X,
			["player_y"] = playerPosition.Y,
			["language"] = _locale.Language,
			["camera_mode"] = (int)_camera.Mode,
			["flags"] = flagsDict,
			["quests"] = questsDict,
		};

		using var file = FileAccess.Open(SlotPath, FileAccess.ModeFlags.Write);
		file.StoreString(Json.Stringify(data));
		GD.Print($"Saved game to {SlotPath}");
	}

	public bool TryLoadGame()
	{
		if (!HasSave())
			return false;

		using var file = FileAccess.Open(SlotPath, FileAccess.ModeFlags.Read);
		var parsed = Json.ParseString(file.GetAsText());
		if (parsed.VariantType != Variant.Type.Dictionary)
			return false;

		var data = parsed.AsGodotDictionary();
		LastScenePath = (string)data["scene"];
		LastPlayerPosition = new Vector2((float)data["player_x"], (float)data["player_y"]);

		if (data.ContainsKey("language"))
			_locale.Language = (string)data["language"];

		if (data.ContainsKey("camera_mode"))
			_camera.Mode = (CameraMode)(int)data["camera_mode"];

		var flags = new Dictionary<string, int>();
		if (data.ContainsKey("flags"))
		{
			var flagsDict = data["flags"].AsGodotDictionary();
			foreach (var key in flagsDict.Keys)
				flags[key.AsString()] = (int)flagsDict[key];
		}
		_flags.LoadFrom(flags);

		if (data.ContainsKey("quests"))
			_quests.LoadFrom(data["quests"].AsGodotDictionary());

		HasPendingLoad = true;
		return true;
	}

	public void ConsumePendingLoad(out Vector2 position)
	{
		position = LastPlayerPosition;
		HasPendingLoad = false;
	}

	public void NewGame()
	{
		_flags.Reset();
		_quests.ResetAll();
		_camera.Mode = CameraMode.TopDown;
		LastScenePath = "res://maps/Village.tscn";
		LastPlayerPosition = new Vector2(160, 200);
		HasPendingLoad = true;
	}
}
