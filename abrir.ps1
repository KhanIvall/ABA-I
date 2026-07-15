# Abre el proyecto con Godot 4.x Mono (.NET). Requiere .NET SDK 8+.
$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $MyInvocation.MyCommand.Path
$localGodot = Join-Path $root "tools\godot\Godot_v4.7.1-stable_mono_win64\Godot_v4.7.1-stable_mono_win64.exe"
if (Test-Path $localGodot) {
	Start-Process -FilePath $localGodot -ArgumentList @("--path", $root)
} else {
	Write-Host "Godot Mono no encontrado en tools/. Instala Godot 4.7 Mono y abre la carpeta del proyecto."
	Write-Host "winget install GodotEngine.GodotEngine.Mono"
}
