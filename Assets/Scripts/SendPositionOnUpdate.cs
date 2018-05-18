using UnityEngine;
using System.Collections;

public class SendPositionOnUpdate : MonoBehaviour {

	public OSC osc;
    public Vector3 acceleration;
    public Vector3 lastVelocity;
    public Quaternion orientationQuat;
    Vector3 rotationRate;

    Vector3 startPosition;
    Vector3 relativePosition;

	// Use this for initialization
	void Start () {
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        orientationQuat = transform.rotation;
        CalculateAcceleration();
        relativePosition = transform.position - startPosition;

        OscMessage message = new OscMessage();

        message.address = "/UpdateXYZ";
        message.values.Add(transform.position.x);
        message.values.Add(transform.position.y);
        message.values.Add(transform.position.z);
        osc.Send(message);

        message = new OscMessage();
        message.address = "/RotationXYZ";
        message.values.Add(transform.eulerAngles.x);
        message.values.Add(transform.eulerAngles.y);
        message.values.Add(transform.eulerAngles.z);
        osc.Send(message);

        message = new OscMessage();
        message.address = "/QuaternionXYZW";
        message.values.Add(transform.rotation.x);
        message.values.Add(transform.rotation.y);
        message.values.Add(transform.rotation.z);
        message.values.Add(transform.rotation.w);
        osc.Send(message);
     

        message = new OscMessage();
        message.address = "/PositionXZ";
        message.values.Add(relativePosition.x);
        message.values.Add(relativePosition.z);
        osc.Send(message);

        message = new OscMessage();
        message.address = "/Velocity";
        message.values.Add(GetComponent<Rigidbody>().velocity.magnitude);
        osc.Send(message);
        Debug.Log("velocity: " + GetComponent<Rigidbody>().velocity.magnitude);

        message = new OscMessage();
        message.address = "/LinAccelerationMagnitude";
        message.values.Add(acceleration.magnitude);
        osc.Send(message);

        message = new OscMessage();
        message.address = "/LinAccelerationXYZ";
        message.values.Add(acceleration.x);
        message.values.Add(acceleration.y);
        message.values.Add(acceleration.z);
        osc.Send(message);

        message = new OscMessage();
        message.address = "/RotRateXYZ";
        message.values.Add(GetComponent<Rigidbody>().angularVelocity.x);
        message.values.Add(GetComponent<Rigidbody>().angularVelocity.y);
        message.values.Add(GetComponent<Rigidbody>().angularVelocity.z);
        osc.Send(message);
        Debug.Log("rotRate: " + GetComponent<Rigidbody>().angularVelocity);
    }

    void CalculateAcceleration()
    {
        acceleration = (GetComponent<Rigidbody>().velocity - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = GetComponent<Rigidbody>().velocity;
    }

    void CalculateRotationRate()
    {

    }

}
