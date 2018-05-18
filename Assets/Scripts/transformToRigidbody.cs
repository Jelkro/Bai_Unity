using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformToRigidbody : MonoBehaviour {
    public GameObject RBobject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RBobject.GetComponent<Rigidbody>().position = transform.position;
        RBobject.GetComponent<Rigidbody>().rotation = transform.rotation;
    }
}
