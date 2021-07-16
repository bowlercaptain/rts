using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : Clickable
{
    Vector3 target;
    Rigidbody myRigidbody;
    Queue<Waypoint> orderQueue = new Queue<Waypoint>();

    public ResourceBundle carried;

    public float health;
    public float resourcesHeld;
    public Color color;

	private void Awake()
	{
        myRigidbody = GetComponent<Rigidbody>();
	}


    // Update is called once per frame
    void Update()
    {
        if (orderQueue.Count > 0)
        {
			Waypoint waypoint = orderQueue.Peek();
			if (waypoint == null) { orderQueue.Dequeue(); return; }
			Vector3 target = waypoint.transform.position;
			Vector3 diff = target - transform.position;
			myRigidbody.AddForce(diff.normalized);
            if(diff.magnitude < 3f)
			{
                var wp = orderQueue.Dequeue();
                wp?.GetComponent<Interactable>()?.TakeInteraction(this, 1);
                Destroy(wp.gameObject);
			}
        }
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        collision.collider.GetComponent<Interactable>()?.TakeInteraction(this, myRigidbody.velocity.magnitude);
	}

	public override void TakeWaypoint(Waypoint newPoint)
	{
        orderQueue.Enqueue(newPoint);
	}
}
