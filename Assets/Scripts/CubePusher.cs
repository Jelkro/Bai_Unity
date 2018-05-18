using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePusher : MonoBehaviour {

    public GameObject FlyingSpeaker;
    public float thrust = 100;
    public float thrust_tilt = 70;
    public float tiltForceDistFromCenter = 5;
    Vector3 pushDirection = new Vector3(1.0f, 0.0f, 1.0f);

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        PushCube(KeyCode.LeftArrow, Vector3.left);
        PushCube(KeyCode.RightArrow, Vector3.right);
        PushCube(KeyCode.UpArrow, Vector3.forward);
        PushCube(KeyCode.DownArrow, Vector3.back);
        PushCube(KeyCode.Space, Vector3.down);

        TiltCube(KeyCode.A, Vector3.left);
        TiltCube(KeyCode.D, Vector3.right);
        TiltCube(KeyCode.W, Vector3.forward);
        TiltCube(KeyCode.S, Vector3.back);

        //Rigidbody rb = FlyingSpeaker.GetComponent<Rigidbody>();
        //rb.velocity = velocity * 10;
        //rb.AddForce(pushDirection * thrust);
    }

    void TiltCube(KeyCode kc, Vector3 direction)
    {
        if (Input.GetKey(kc))
        {
            Rigidbody rb = FlyingSpeaker.GetComponent<Rigidbody>();
            rb.AddForceAtPosition(direction * thrust_tilt, FlyingSpeaker.transform.position - Vector3.down * tiltForceDistFromCenter);
        }
    }

    void PushCube(KeyCode kc, Vector3 velocity)
    {
        if (Input.GetKey(kc))
        {
            Rigidbody rb = FlyingSpeaker.GetComponent<Rigidbody>();
            //rb.velocity = velocity * 10;
            rb.AddForce((velocity + Vector3.down) * thrust);
        }
    }
}
