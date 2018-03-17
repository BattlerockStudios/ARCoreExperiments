using UnityEngine;
using System.Collections;

public class SetRandomColor : MonoBehaviour 
{	
	// Initializes things
	void Start () 
	{
		gameObject.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
	}
}
