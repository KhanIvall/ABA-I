using Godot;

namespace ABAI;

/// <summary>Shared bootstrap for playable maps: place player, HUD, dialogue, pause.</summary>
public partial class MapBootstrap : Node
{
	[Export] public CameraMode InitialMode { get; set; } = CameraMode.TopDown;
	[Export] public Vector2 DefaultSpawn { get; set; } = new(160, 200);
	[Export] public PackedScene? PlayerScene { get; set; }
	[Export] public PackedScene? HudScene { get; set; }
	[Export] public PackedScene? DialogueScene { get; set; }
	[Export] public PackedScene? PauseScene { get; set; }

	public override void _Ready()
	{
		var camera = GetNode<CameraModeService>("/root/CameraModeService");
		var save = GetNode<SaveSystem>("/root/SaveSystem");
		camera.Mode = InitialMode;

		EnsureUi();

		var spawn = DefaultSpawn;
		if (save.HasPendingLoad)
			save.ConsumePendingLoad(out spawn);

		var playerRoot = GetTree().CurrentScene;
		var existing = playerRoot.GetNodeOrNull<PlayerController>("Player");
		if (existing == null)
		{
			PlayerController player;
			if (PlayerScene != null)
				player = PlayerScene.Instantiate<PlayerController>();
			else
				player = BuildDefaultPlayer();

			player.Name = "Player";
			player.AddToGroup("player");
			playerRoot.AddChild(player);
			player.GlobalPosition = spawn;
		}
		else
		{
			existing.AddToGroup("player");
			existing.GlobalPosition = spawn;
		}
	}

	private void EnsureUi()
	{
		var root = GetTree().Root;
		if (root.FindChild("Hud", true, false) == null)
		{
			var hud = HudScene != null ? HudScene.Instantiate() : BuildHud();
			hud.Name = "Hud";
			GetTree().CurrentScene.AddChild(hud);
		}

		if (root.FindChild("DialogueUI", true, false) == null)
		{
			var dialogue = DialogueScene != null ? DialogueScene.Instantiate() : BuildDialogue();
			dialogue.Name = "DialogueUI";
			GetTree().CurrentScene.AddChild(dialogue);
		}

		if (root.FindChild("PauseMenu", true, false) == null)
		{
			var pause = PauseScene != null ? PauseScene.Instantiate() : BuildPause();
			pause.Name = "PauseMenu";
			GetTree().CurrentScene.AddChild(pause);
		}
	}

	private static PlayerController BuildDefaultPlayer()
	{
		var player = new PlayerController();
		var collision = new CollisionShape2D { Name = "CollisionShape2D" };
		collision.Shape = new RectangleShape2D { Size = new Vector2(16, 24) };
		player.AddChild(collision);

		var body = new ColorRect
		{
			Name = "Visual",
			Size = new Vector2(16, 24),
			Position = new Vector2(-8, -12),
			Color = new Color(0.24f, 0.55f, 0.99f),
		};
		player.AddChild(body);

		var camera = new Camera2D { Name = "Camera2D", Enabled = true };
		player.AddChild(camera);

		var interact = new Area2D
		{
			Name = "InteractArea",
			CollisionLayer = 0,
			CollisionMask = 4,
		};
		var interactShape = new CollisionShape2D();
		interactShape.Shape = new CircleShape2D { Radius = 28 };
		interact.AddChild(interactShape);
		player.AddChild(interact);

		player.CollisionLayer = 2;
		player.CollisionMask = 1;
		return player;
	}

	private static Hud BuildHud()
	{
		var hud = new Hud();
		var margin = new MarginContainer { Name = "Margin" };
		margin.SetAnchorsPreset(Control.LayoutPreset.FullRect);
		margin.AddThemeConstantOverride("margin_left", 16);
		margin.AddThemeConstantOverride("margin_top", 16);
		margin.AddThemeConstantOverride("margin_right", 16);
		margin.AddThemeConstantOverride("margin_bottom", 16);
		var vbox = new VBoxContainer { Name = "VBox" };
		vbox.AddChild(new Label { Name = "Controls" });
		vbox.AddChild(new Label { Name = "Quests" });
		vbox.AddChild(new Label { Name = "Mode" });
		margin.AddChild(vbox);
		hud.AddChild(margin);
		return hud;
	}

	private static DialogueUI BuildDialogue()
	{
		var ui = new DialogueUI();
		var panel = new PanelContainer
		{
			Name = "Panel",
			Visible = false,
		};
		panel.SetAnchorsPreset(Control.LayoutPreset.BottomWide);
		panel.OffsetTop = -140;
		panel.OffsetBottom = -20;
		panel.OffsetLeft = 40;
		panel.OffsetRight = -40;

		var margin = new MarginContainer { Name = "Margin" };
		margin.AddThemeConstantOverride("margin_left", 16);
		margin.AddThemeConstantOverride("margin_top", 12);
		margin.AddThemeConstantOverride("margin_right", 16);
		margin.AddThemeConstantOverride("margin_bottom", 12);
		var vbox = new VBoxContainer { Name = "VBox" };
		vbox.AddChild(new Label { Name = "Text", AutowrapMode = TextServer.AutowrapMode.WordSmart });
		vbox.AddChild(new Label { Name = "Hint" });
		margin.AddChild(vbox);
		panel.AddChild(margin);
		ui.AddChild(panel);
		return ui;
	}

	private static PauseMenu BuildPause()
	{
		var pause = new PauseMenu();
		var root = new Control { Name = "Root", Visible = false, ProcessMode = ProcessModeEnum.Always };
		root.SetAnchorsPreset(Control.LayoutPreset.FullRect);

		var dim = new ColorRect
		{
			Color = new Color(0, 0, 0, 0.55f),
		};
		dim.SetAnchorsPreset(Control.LayoutPreset.FullRect);
		root.AddChild(dim);

		var panel = new PanelContainer { Name = "Panel" };
		panel.SetAnchorsPreset(Control.LayoutPreset.Center);
		panel.OffsetLeft = -140;
		panel.OffsetTop = -120;
		panel.OffsetRight = 140;
		panel.OffsetBottom = 120;

		var vbox = new VBoxContainer { Name = "VBox" };
		vbox.AddChild(new Label { Name = "Title", HorizontalAlignment = HorizontalAlignment.Center });
		vbox.AddChild(new Button { Name = "Resume" });
		vbox.AddChild(new Button { Name = "Save" });
		vbox.AddChild(new Button { Name = "Language" });
		vbox.AddChild(new Button { Name = "MainMenu" });
		panel.AddChild(vbox);
		root.AddChild(panel);
		pause.AddChild(root);
		return pause;
	}
}
