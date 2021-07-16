using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MouseReciever : MonoBehaviour
{
	public Action<Vector3> pointHit;
	private void OnMouseDown()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit))
		{
			pointHit(hit.point);
		}
	}
}
