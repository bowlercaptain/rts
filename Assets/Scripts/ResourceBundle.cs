using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class ResourceBundle
{
	[System.Serializable]
	public struct entry
	{
		public entry(ResourceType type, float amount)
		{
			this.type = type;
			this.amount = amount;
		}
		public ResourceType type;
		public float amount;
	}

	public List<entry> entries;
	
	public static ResourceBundle operator +(ResourceBundle a, ResourceBundle b)
	{
		var nr = new ResourceBundle();
		nr.entries.AddRange(a.entries);
		nr.entries.AddRange(b.entries);
		return nr;
	}

	internal void AddResources(ResourceType type, float amount)
	{
		try
		{
			entry sametype = entries.Where((entry ent) => ent.type == type).First();
		}
		catch (System.InvalidOperationException e)
		{
			entries.Add(new entry(type, amount));
		}
	}
}