﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleJoiner : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<HingeJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
