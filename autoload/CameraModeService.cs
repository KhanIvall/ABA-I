using Godot;

namespace ABAI;

/// <summary>Global camera/movement mode. Zones and maps set this; PlayerController reads it.</summary>
public partial class CameraModeService : Node
{
	public event System.Action<int>? ModeChanged;

	private CameraMode _mode = CameraMode.TopDown;

	public CameraMode Mode
	{
		get => _mode;
		set
		{
			if (_mode == value)
				return;
			_mode = value;
			ModeChanged?.Invoke((int)value);
		}
	}

	public void SetMode(CameraMode mode) => Mode = mode;
}
