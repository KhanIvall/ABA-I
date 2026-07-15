using Godot;

namespace ABAI;

/// <summary>Switches CameraMode when the player enters (hybrid by zones).</summary>
public partial class CameraModeZone : Area2D
{
	[Export] public CameraMode Mode { get; set; } = CameraMode.TopDown;

	private CameraModeService _camera = null!;

	public override void _Ready()
	{
		_camera = GetNode<CameraModeService>("/root/CameraModeService");
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is PlayerController)
			_camera.SetMode(Mode);
	}
}
