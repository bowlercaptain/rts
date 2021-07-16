using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "rts/resource type")]
public class ResourceType : ScriptableObject
{
	public string typeName;
	public Texture2D displayImage;
	public Color color;
}
