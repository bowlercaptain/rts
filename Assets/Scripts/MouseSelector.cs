using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelector : Commander
{
    List<Clickable> selecteds = new List<Clickable>();
    Camera cam;
    public LayerMask selectableMask;
    public Waypoint waypointPrefab;
    public Transform cubeTransform;

    private void Awake()
    {
        if (cam == null) { cam = GetComponent<Camera>(); }

    }



    private Vector3 firstSelectCorner;
    private HashSet<Collider> checkedColliders = new HashSet<Collider>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				firstSelectCorner = hit.point;
			}

			foreach (var unit in selecteds)
			{
                unit.selected = false;
			}
            selecteds.Clear();
        }

		if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                var collideds = Physics.OverlapBox((hit.point + firstSelectCorner) / 2f, (hit.point - firstSelectCorner) + Vector3.up*20, Quaternion.identity, selectableMask);
                cubeTransform.position = (hit.point + firstSelectCorner) / 2f;
                    cubeTransform.localScale = (hit.point - firstSelectCorner) + Vector3.up * 20;
                foreach (Collider col in collideds)
				{
					if (!checkedColliders.Contains(col))
					{
                        Debug.Log(col.name);
                        var clickable = col.GetComponent<Clickable>();
                        if (clickable != null)
                        {
                            Debug.Log("selected!");
                            selecteds.Add(clickable);
                            clickable.selected = true;
                            clickable.selector = this;
                        }
                        checkedColliders.Add(col);
					}
				}
            }
        }
		if (Input.GetMouseButtonUp(0))
		{

            cubeTransform.position -= Vector3.up * 100;
            Debug.Log("dropped");
            checkedColliders = new HashSet<Collider>();
		}

        if (Input.GetMouseButtonDown(1))
        {
            if (selecteds.Count > 0)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Waypoint whey = Instantiate(waypointPrefab);
                    whey.transform.position = hit.point;
                    foreach (var selected in selecteds)
                    {
                        selected.TakeWaypoint(whey);
                    }

                }

            }
        }
    }
}
