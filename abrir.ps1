# Abre el proyecto con Godot 4.x Mono (.NET). Requiere .NET SDK 8+.
$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $MyInvocation.MyCommand.Path

# Rebuild C# with the local Godot source generators (needed when NuGet generators are blocked).
$genProj = Join-Path $root "tools\LocalGodotGenerators\LocalGodotGenerators.csproj"
$genDll = Join-Path $root "tools\LocalGodotGenerators\bin\Release\netstandard2.0\Godot.SourceGenerators.dll"
if (Test-Path $genProj) {
	if (-not (Test-Path $genDll)) {
		Write-Host "Compilando generadores C# locales..."
		dotnet build $genProj -c Release --nologo | Out-Host
	}
	Write-Host "Compilando proyecto ABA-I..."
	Push-Location $root
	try { dotnet build --nologo | Out-Host } finally { Pop-Location }
}

$localGodot = Join-Path $root "tools\godot\Godot_v4.7.1-stable_mono_win64\Godot_v4.7.1-stable_mono_win64.exe"
if (Test-Path $localGodot) {
	Start-Process -FilePath $localGodot -ArgumentList @("--path", $root)
} elseif (Get-Command godot -ErrorAction SilentlyContinue) {
	Start-Process -FilePath "godot" -ArgumentList @("--path", $root)
} else {
	Write-Host "Godot Mono no encontrado. Instala Godot 4.7 Mono y abre la carpeta del proyecto."
	Write-Host "winget install GodotEngine.GodotEngine.Mono"
}
