using System;
using System.Collections.Generic;
using Godot;

namespace ABAI;

/// <summary>World progress flags (ints). Decoupled from UI and level scenes.</summary>
public partial class WorldFlags : Node
{
	public event Action<string, int>? FlagChanged;

	private readonly Dictionary<string, int> _flags = new();

	public int Get(string key, int defaultValue = 0) =>
		_flags.TryGetValue(key, out var value) ? value : defaultValue;

	public bool Has(string key, int minValue = 1) => Get(key) >= minValue;

	public void Set(string key, int value)
	{
		_flags[key] = value;
		FlagChanged?.Invoke(key, value);
	}

	public void Add(string key, int delta = 1) => Set(key, Get(key) + delta);

	public bool MeetsAll(Dictionary<string, int> requirements)
	{
		foreach (var pair in requirements)
		{
			if (Get(pair.Key) < pair.Value)
				return false;
		}
		return true;
	}

	public void Apply(Dictionary<string, int> rewards)
	{
		foreach (var pair in rewards)
			Set(pair.Key, pair.Value);
	}

	public Dictionary<string, int> Snapshot() => new(_flags);

	public void LoadFrom(Dictionary<string, int> data)
	{
		_flags.Clear();
		foreach (var pair in data)
			_flags[pair.Key] = pair.Value;
	}

	public void Reset()
	{
		_flags.Clear();
	}
}
