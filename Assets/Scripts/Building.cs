using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Clickable
{
	[System.Serializable]
	public struct buildable
	{
		public string entryName;
		public ResourceBundle cost;
		public GameObject toCreate;
		public string hotkey;
	}

	public buildable[] units;

	private void Update(){
		if (selected)
			foreach (var unit in units)
				if (Input.GetKeyDown(unit.hotkey))
					if (selector.hoard.TrySpend(unit.cost))
					{
						var uuu = Instantiate(unit.toCreate);
						uuu.transform.position = transform.position + transform.up * 5;
					}
	}

	public override void TakeWaypoint(Waypoint newPoint)
	{
		//do nothing
	}
}