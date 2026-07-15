namespace ABAI;

/// <summary>
/// Gameplay camera / movement mode. Quests never depend on this.
/// Vertical-slice decision: hybrid by zones (Village is top-down by default;
/// the south platform pocket switches to Side via CameraModeZone).
/// </summary>
public enum CameraMode
{
	Side,
	TopDown,
}
