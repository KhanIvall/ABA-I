using System;
using System.Collections.Generic;
using Godot;

namespace ABAI;

/// <summary>CSV-backed localization (es default, en optional). No UI hard-coded strings.</summary>
public partial class Locale : Node
{
	public const string Spanish = "es";
	public const string English = "en";

	public event Action<string>? LanguageChanged;

	private readonly Dictionary<string, Dictionary<string, string>> _table = new();
	private string _language = Spanish;

	public string Language
	{
		get => _language;
		set => SetLanguage(value, notify: true);
	}

	/// <summary>Apply language from a save without notifying UI mid-scene-change.</summary>
	public void SetLanguageSilent(string language) => SetLanguage(language, notify: false);

	private void SetLanguage(string value, bool notify)
	{
		if (value != Spanish && value != English)
			return;
		if (_language == value)
			return;

		_language = value;
		TranslationServer.SetLocale(value);
		if (notify)
			LanguageChanged?.Invoke(value);
	}

	public override void _Ready()
	{
		LoadCsv("res://locales/strings.csv");
		_language = Spanish;
		TranslationServer.SetLocale(Spanish);
	}

	public string TrKey(string key)
	{
		if (_table.TryGetValue(key, out var locales))
		{
			if (locales.TryGetValue(_language, out var text) && !string.IsNullOrEmpty(text))
				return text;
			if (locales.TryGetValue(Spanish, out var fallback) && !string.IsNullOrEmpty(fallback))
				return fallback;
		}
		return key;
	}

	public void ToggleLanguage() =>
		Language = _language == Spanish ? English : Spanish;

	private void LoadCsv(string path)
	{
		if (!FileAccess.FileExists(path))
		{
			GD.PushError($"Locale CSV missing: {path}");
			return;
		}

		using var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		if (file == null)
			return;

		var header = ParseCsvLine(file.GetLine());
		if (header.Count < 2)
			return;

		while (!file.EofReached())
		{
			var line = file.GetLine();
			if (string.IsNullOrWhiteSpace(line))
				continue;

			var cols = ParseCsvLine(line);
			if (cols.Count == 0)
				continue;

			var key = cols[0];
			var locales = new Dictionary<string, string>();
			for (var i = 1; i < header.Count && i < cols.Count; i++)
				locales[header[i]] = cols[i];

			_table[key] = locales;
		}
	}

	private static List<string> ParseCsvLine(string line)
	{
		var result = new List<string>();
		var current = new System.Text.StringBuilder();
		var inQuotes = false;

		for (var i = 0; i < line.Length; i++)
		{
			var c = line[i];
			if (c == '"')
			{
				if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
				{
					current.Append('"');
					i++;
				}
				else
				{
					inQuotes = !inQuotes;
				}
			}
			else if (c == ',' && !inQuotes)
			{
				result.Add(current.ToString());
				current.Clear();
			}
			else
			{
				current.Append(c);
			}
		}

		result.Add(current.ToString());
		return result;
	}
}
