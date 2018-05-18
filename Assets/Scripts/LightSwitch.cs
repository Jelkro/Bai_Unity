using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {
    
    public float maxIntensity;
    public float intensity;
    public GameObject FlyingSpeaker;
    public Vector3 lightPosition;
    public float distance;
    public bool on = false;
    public float lightSpeed = 0.2f;

    // Use this for initialization
    void Start() {
        lightPosition = GetComponent<Transform>().position;
        //maxIntensity = 10;
    }

    // Update is called once per frame
    void Update() {
        Vector3 speakerPosition = FlyingSpeaker.GetComponent<Transform>().position;
        Vector3 diffPosition = speakerPosition - lightPosition;
        distance = diffPosition.magnitude;

        if (distance < 2.8)
        {
            if (!on)
            {
                GetComponent<Light>().intensity = maxIntensity;
                on = true;
            }


        }
        if (on)
        {
            GetComponent<Light>().intensity -= lightSpeed/maxIntensity;
        }

        if(GetComponent<Light>().intensity <= 0) {
        on = false;
        }

	}
}
