using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotIntensity : MonoBehaviour {
    public float intensity = 2;
    public Vector2 rotationAmnt;
    public Vector3 orientation;
    public float tiltAmount;

    //Light color
    Renderer rend;
    Color finalColor;
    GameObject LightBulb;
    Material mat;
    Color baseColor;

    // Use this for initialization
    void Start () {
        LightBulb = transform.Find("LightBulb").gameObject;
        rend = LightBulb.GetComponent<Renderer>();
        mat = rend.material;
        baseColor = new Color();
        ColorUtility.TryParseHtmlString("#FFF5BF", out baseColor);
    }
	
	// Update is called once per frame
	void Update () {

        //Get Orientation data
        orientation = GetComponent<Transform>().localEulerAngles;
        rotationAmnt.x = orientation.x;
        rotationAmnt.y = orientation.z;

        //Set data to -180 to 180 range
        if(rotationAmnt.x >= 180)
        {
            rotationAmnt.x = (rotationAmnt.x - 360);
        }
        if (rotationAmnt.y >= 180)
        {
            rotationAmnt.y = (rotationAmnt.y - 360);
        }

        tiltAmount = Mathf.Abs(rotationAmnt.magnitude);
        print("rotationAmnt: " + rotationAmnt);
        print("Size: " + tiltAmount);

        //Adjust light intensity based on amount of tilt from speaker
        GetComponentInChildren<Light>().intensity = intensity * Mathf.Pow(tiltAmount, 2) / 1000;
       
        //Adjust emmission of bulb     
        finalColor = baseColor * Mathf.LinearToGammaSpace(intensity * Mathf.Pow(tiltAmount,1) / 300);
        mat.SetColor("_EmissionColor", finalColor);

    }
}
