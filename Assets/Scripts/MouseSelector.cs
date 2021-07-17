using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSelector : Commander
{
    List<Clickable> selecteds = new List<Clickable>();
    Camera cam;
    public LayerMask selectableMask;
    public Waypoint waypointPrefab;
    public GameObject selectBoxUI;
    private RectTransform selectBoxRectTrans;

    private void Awake()
    {
        if (cam == null) { cam = GetComponent<Camera>(); }
        Debug.Assert(selectBoxUI != null, "hey I need a ui panel to show u selections with. set my selectboxui field plz thx");
        if(selectBoxRectTrans == null) { selectBoxRectTrans = selectBoxUI.GetComponent<RectTransform>(); }
    }

    Vector3 firstSelectCorner;
    private HashSet<Collider> checkedColliders = new HashSet<Collider>();
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowSelectRect(Input.mousePosition);
            Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit firstShothit);
            firstSelectCorner = firstShothit.point;
			foreach (var unit in selecteds)
			{
                unit.selected = false;
			}
            selecteds.Clear();
        }

		if (Input.GetMouseButton(0))
        {
            UpdateSelectRect(Input.mousePosition);
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
				Vector3 center = (hit.point + firstSelectCorner) / 2f;
                Vector3 diagonalDiff = hit.point - firstSelectCorner;
                Vector3 absedDiff = new Vector3(Mathf.Abs(diagonalDiff.x), Mathf.Abs(diagonalDiff.y), Mathf.Abs(diagonalDiff.z));
                Vector3 extents = (absedDiff)/2f + Vector3.up * 200;

                var collideds = Physics.OverlapBox(center, extents*2f, Quaternion.identity, selectableMask);;

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

    private Vector2 firstMousePos;
    void ShowSelectRect(Vector2 mousePos)
	{
        selectBoxUI.SetActive(true);
        firstMousePos = mousePos;
	}

    void UpdateSelectRect(Vector2 mousePos)
	{
        var diff = mousePos - firstMousePos;
        selectBoxRectTrans.anchoredPosition = firstMousePos + diff / 2f;
        selectBoxRectTrans.sizeDelta = new Vector2(Mathf.Abs(diff.x), Mathf.Abs(diff.y));
	}

	void HideSelectRect()
	{
        selectBoxUI.SetActive(false);
	}
}
