using System.Collections.Generic;
using Godot;

namespace ABAI;

/// <summary>Minimal inventory stub for later content (Phase 3).</summary>
public partial class Inventory : Node
{
	private readonly Dictionary<string, int> _items = new();

	public void Add(string itemId, int amount = 1)
	{
		_items.TryGetValue(itemId, out var current);
		_items[itemId] = current + amount;
	}

	public int Count(string itemId) =>
		_items.TryGetValue(itemId, out var n) ? n : 0;

	public Dictionary<string, int> Snapshot() => new(_items);

	public void LoadFrom(Dictionary<string, int> data)
	{
		_items.Clear();
		foreach (var pair in data)
			_items[pair.Key] = pair.Value;
	}

	public void Clear() => _items.Clear();
}
