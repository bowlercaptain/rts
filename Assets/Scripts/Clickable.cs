using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
	public bool selected;
	public Commander selector;
	public virtual void TakeWaypoint(Waypoint newPoint)
	{
		throw new System.NotImplementedException();
	}
}
