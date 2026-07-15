using Godot;

namespace ABAI;

public partial class PrototypeSideMap : Node2D
{
	public override void _Ready()
	{
		GetNode<CameraModeService>("/root/CameraModeService").Mode = CameraMode.Side;

		AddChild(new ColorRect
		{
			Size = new Vector2(800, 400),
			Color = new Color(0.12f, 0.14f, 0.2f),
			ZIndex = -10,
		});

		CreatePlatform(new Rect2(0, 320, 800, 40));
		CreatePlatform(new Rect2(120, 250, 120, 16));
		CreatePlatform(new Rect2(320, 200, 120, 16));
		CreatePlatform(new Rect2(520, 150, 120, 16));

		var back = new Button
		{
			Text = "Menu",
			Position = new Vector2(12, 12),
		};
		back.Pressed += () => GetTree().ChangeSceneToFile("res://ui/MainMenu.tscn");
		var canvas = new CanvasLayer();
		canvas.AddChild(back);
		AddChild(canvas);

		var bootstrap = new MapBootstrap
		{
			Name = "Bootstrap",
			InitialMode = CameraMode.Side,
			DefaultSpawn = new Vector2(80, 280),
		};
		AddChild(bootstrap);
	}

	private void CreatePlatform(Rect2 rect)
	{
		var body = new StaticBody2D { CollisionLayer = 1 };
		var shape = new CollisionShape2D
		{
			Shape = new RectangleShape2D { Size = rect.Size },
			Position = rect.Size / 2f,
		};
		body.Position = rect.Position;
		body.AddChild(shape);
		body.AddChild(new ColorRect
		{
			Size = rect.Size,
			Color = new Color(0.45f, 0.4f, 0.35f),
		});
		AddChild(body);
	}
}
