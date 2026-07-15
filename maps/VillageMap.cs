using Godot;

namespace ABAI;

public partial class VillageMap : Node2D
{
	public override void _Ready()
	{
		CreateFloorRect(new Rect2(0, 0, 640, 400), new Color(0.18f, 0.22f, 0.2f));
		CreateWall(new Rect2(0, 0, 640, 16));
		CreateWall(new Rect2(0, 384, 640, 16));
		CreateWall(new Rect2(0, 0, 16, 400));
		CreateWall(new Rect2(624, 0, 16, 400));
		CreateWall(new Rect2(280, 120, 80, 40));
		CreateWall(new Rect2(80, 250, 120, 24));

		CreateNpc(new Vector2(200, 200));
		CreateOverlook(new Vector2(520, 60));
		CreateSideZone(new Rect2(16, 300, 200, 80));
		CreateTopDownZone(new Rect2(16, 16, 600, 280));

		var bootstrap = new MapBootstrap
		{
			Name = "Bootstrap",
			InitialMode = CameraMode.TopDown,
			DefaultSpawn = new Vector2(120, 200),
		};
		AddChild(bootstrap);
	}

	private void CreateFloorRect(Rect2 rect, Color color)
	{
		var floor = new ColorRect
		{
			Position = rect.Position,
			Size = rect.Size,
			Color = color,
			ZIndex = -10,
		};
		AddChild(floor);
	}

	private void CreateWall(Rect2 rect)
	{
		var body = new StaticBody2D { CollisionLayer = 1, CollisionMask = 0 };
		var shape = new CollisionShape2D();
		shape.Shape = new RectangleShape2D { Size = rect.Size };
		shape.Position = rect.Size / 2f;
		body.Position = rect.Position;
		body.AddChild(shape);

		var visual = new ColorRect
		{
			Size = rect.Size,
			Color = new Color(0.35f, 0.32f, 0.28f),
		};
		body.AddChild(visual);
		AddChild(body);
	}

	private void CreateNpc(Vector2 pos)
	{
		var npc = new NpcGuide
		{
			Name = "NpcGuide",
			Position = pos,
			CollisionLayer = 4,
			CollisionMask = 2,
			Monitoring = true,
			Monitorable = true,
		};
		var shape = new CollisionShape2D();
		shape.Shape = new CircleShape2D { Radius = 18 };
		npc.AddChild(shape);
		var visual = new ColorRect
		{
			Size = new Vector2(20, 28),
			Position = new Vector2(-10, -14),
			Color = new Color(0.9f, 0.7f, 0.25f),
		};
		npc.AddChild(visual);
		AddChild(npc);
	}

	private void CreateOverlook(Vector2 pos)
	{
		var marker = new ReachMarker
		{
			Name = "Overlook",
			Position = pos,
			MarkerId = "marker_overlook",
			CollisionLayer = 0,
			CollisionMask = 2,
			Monitoring = true,
		};
		var shape = new CollisionShape2D();
		shape.Shape = new CircleShape2D { Radius = 24 };
		marker.AddChild(shape);
		var visual = new ColorRect
		{
			Size = new Vector2(28, 28),
			Position = new Vector2(-14, -14),
			Color = new Color(0.3f, 0.8f, 0.45f, 0.7f),
		};
		marker.AddChild(visual);
		AddChild(marker);
	}

	private void CreateSideZone(Rect2 rect)
	{
		var zone = new CameraModeZone
		{
			Name = "SideZone",
			Mode = CameraMode.Side,
			Position = rect.Position,
			CollisionLayer = 0,
			CollisionMask = 2,
			Monitoring = true,
		};
		var shape = new CollisionShape2D();
		shape.Shape = new RectangleShape2D { Size = rect.Size };
		shape.Position = rect.Size / 2f;
		zone.AddChild(shape);
		var hint = new ColorRect
		{
			Size = rect.Size,
			Color = new Color(0.2f, 0.35f, 0.7f, 0.2f),
		};
		zone.AddChild(hint);
		AddChild(zone);

		// Local platforms for side mode practice.
		CreateWall(new Rect2(rect.Position.X + 20, rect.Position.Y + 50, 60, 12));
		CreateWall(new Rect2(rect.Position.X + 100, rect.Position.Y + 30, 60, 12));
	}

	private void CreateTopDownZone(Rect2 rect)
	{
		var zone = new CameraModeZone
		{
			Name = "TopDownZone",
			Mode = CameraMode.TopDown,
			Position = rect.Position,
			CollisionLayer = 0,
			CollisionMask = 2,
			Monitoring = true,
		};
		var shape = new CollisionShape2D();
		shape.Shape = new RectangleShape2D { Size = rect.Size };
		shape.Position = rect.Size / 2f;
		zone.AddChild(shape);
		AddChild(zone);
	}
}
