using Godot;

namespace ABAI;

public partial class PrototypeTopDownMap : Node2D
{
	public override void _Ready()
	{
		GetNode<CameraModeService>("/root/CameraModeService").Mode = CameraMode.TopDown;

		AddChild(new ColorRect
		{
			Size = new Vector2(640, 400),
			Color = new Color(0.14f, 0.2f, 0.16f),
			ZIndex = -10,
		});

		CreateObstacle(new Rect2(0, 0, 640, 16));
		CreateObstacle(new Rect2(0, 384, 640, 16));
		CreateObstacle(new Rect2(0, 0, 16, 400));
		CreateObstacle(new Rect2(624, 0, 16, 400));
		CreateObstacle(new Rect2(200, 140, 80, 80));
		CreateObstacle(new Rect2(400, 220, 100, 40));

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
			InitialMode = CameraMode.TopDown,
			DefaultSpawn = new Vector2(80, 80),
		};
		AddChild(bootstrap);
	}

	private void CreateObstacle(Rect2 rect)
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
			Color = new Color(0.3f, 0.35f, 0.3f),
		});
		AddChild(body);
	}
}
