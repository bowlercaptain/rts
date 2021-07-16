using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSource : MonoBehaviour, Interactable
{
    public ResourceType type;
    public float starting = 100;
    public float current;

    public float dead = 0;

	public void TakeInteraction(Dude doer, float muchness)
	{
        if (current > dead){
            doer.carried.AddResources(type,Mathf.Min(muchness, current));
            current -= muchness;
        }
        if(current < dead)
		{
            Destroy(gameObject);
		}
	}

    public void Start(){
        current = starting;
    }
}
