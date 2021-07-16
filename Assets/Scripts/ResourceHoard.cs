using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHoard : MonoBehaviour
{

	public ResourceBundle myResources;
	public bool TrySpend(ResourceBundle resources)
	{
		return true;
		//check against owned resources
		//if can spend, spend, then return true;
	}
}
