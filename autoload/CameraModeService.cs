using Godot;

namespace ABAI;

/// <summary>Global camera/movement mode. Zones and maps set this; PlayerController reads it.</summary>
public partial class CameraModeService : Node
{
	[Signal]
	public delegate void ModeChangedEventHandler(int mode);

	private CameraMode _mode = CameraMode.TopDown;

	public CameraMode Mode
	{
		get => _mode;
		set
		{
			if (_mode == value)
				return;
			_mode = value;
			EmitSignal(SignalName.ModeChanged, (int)value);
		}
	}

	public void SetMode(CameraMode mode) => Mode = mode;
}
