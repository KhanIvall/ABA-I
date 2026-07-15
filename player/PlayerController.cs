using Godot;

namespace ABAI;

/// <summary>Player movement adapts to CameraMode (side vs top-down). Quests ignore this.</summary>
public partial class PlayerController : CharacterBody2D
{
	[Export] public float MoveSpeed { get; set; } = 160f;
	[Export] public float JumpVelocity { get; set; } = -320f;
	[Export] public float Gravity { get; set; } = 900f;

	private CameraModeService _camera = null!;
	private Area2D? _interactArea;
	private IInteractable? _currentTarget;

	public override void _Ready()
	{
		_camera = GetNode<CameraModeService>("/root/CameraModeService");
		_interactArea = GetNodeOrNull<Area2D>("InteractArea");
		if (_interactArea != null)
		{
			_interactArea.AreaEntered += OnAreaEntered;
			_interactArea.AreaExited += OnAreaExited;
			_interactArea.BodyEntered += OnBodyEntered;
			_interactArea.BodyExited += OnBodyExited;
		}

		ApplyModePhysics(_camera.Mode);
		_camera.ModeChanged += OnModeChanged;
	}

	public override void _ExitTree()
	{
		if (_camera != null)
			_camera.ModeChanged -= OnModeChanged;
	}

	public override void _PhysicsProcess(double delta)
	{
		var mode = _camera.Mode;
		if (mode == CameraMode.Side)
			ProcessSide(delta);
		else
			ProcessTopDown();

		MoveAndSlide();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed("interact"))
		{
			_currentTarget?.Interact(this);
			GetViewport().SetInputAsHandled();
		}
	}

	private void ProcessSide(double delta)
	{
		var velocity = Velocity;
		if (!IsOnFloor())
			velocity.Y += Gravity * (float)delta;

		var dir = Input.GetAxis("move_left", "move_right");
		velocity.X = dir * MoveSpeed;

		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		Velocity = velocity;
	}

	private void ProcessTopDown()
	{
		var input = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Velocity = input * MoveSpeed;
	}

	private void OnModeChanged(int mode) => ApplyModePhysics((CameraMode)mode);

	private void ApplyModePhysics(CameraMode mode)
	{
		// Side uses gravity + floor; top-down slides freely.
		MotionMode = mode == CameraMode.Side
			? MotionModeEnum.Grounded
			: MotionModeEnum.Floating;
		UpDirection = Vector2.Up;
		if (mode == CameraMode.TopDown)
			Velocity = Vector2.Zero;
	}

	private void OnAreaEntered(Area2D area) => TryBind(area);
	private void OnAreaExited(Area2D area) => TryUnbind(area);
	private void OnBodyEntered(Node2D body) => TryBind(body);
	private void OnBodyExited(Node2D body) => TryUnbind(body);

	private void TryBind(Node node)
	{
		if (node is IInteractable interactable)
			_currentTarget = interactable;
		else if (node.GetParent() is IInteractable parent)
			_currentTarget = parent;
	}

	private void TryUnbind(Node node)
	{
		if (_currentTarget == null)
			return;
		if (ReferenceEquals(_currentTarget, node) ||
			(node.GetParent() is Node p && ReferenceEquals(_currentTarget, p)))
		{
			_currentTarget = null;
		}
	}
}
