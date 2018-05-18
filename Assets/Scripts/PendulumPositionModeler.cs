using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumPositionModeler : MonoBehaviour {
    public GameObject pendulum;
    public GameObject anchorpoint;
    Vector3 r;
    Vector3 F;
    Vector3 momentum;
    Vector3 g = new Vector3(0, -9.8f, 0);
    public float ropeLength;
    public float stringConstant = 5000000;
    public float dt = .025f;
    public bool syncToObject;
    public float resetInterval;
    float curTime;
    float prevTime;
    float mass;


	// Use this for initialization
	void Start () {
        this.transform.position = pendulum.transform.position;
        r = pendulum.transform.position - anchorpoint.transform.position;
        ropeLength = r.magnitude;
        mass = this.GetComponent<Rigidbody>().mass;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        curTime = Time.time;
        mass = this.GetComponent<Rigidbody>().mass;
        Vector3 difference = this.transform.position - pendulum.transform.position;

        if (syncToObject && difference.magnitude > .5)
        {
            this.transform.position = pendulum.transform.position;
            momentum = pendulum.GetComponent<Rigidbody>().velocity * mass;
            resetInterval = curTime - prevTime;
            prevTime = curTime;
        }
        
        r = this.transform.position - anchorpoint.transform.position;
        F = mass * g + stringConstant * (ropeLength - r.magnitude) * r.normalized;
        momentum += F * dt;
        this.transform.position += momentum * dt / mass;

        Debug.Log("ropeLength length:" + ropeLength);
        Debug.Log(ropeLength - r.magnitude);
	}
}
