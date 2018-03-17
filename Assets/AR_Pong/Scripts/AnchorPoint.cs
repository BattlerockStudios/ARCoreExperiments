using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Pose pose = new Pose
        {
            position = Vector3.zero,
            rotation = Quaternion.identity
        };
        //m_anchorPoint = GoogleARCore.Session.CreateAnchor(pose);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
