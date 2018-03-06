using UnityEngine;
using System.Collections;

public class SetColor : MonoBehaviour 
{
	public Color col;	
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.GetComponent<Renderer>().material.color = col;
	}
}
