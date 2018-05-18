using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbSwitch : MonoBehaviour
{

    public float maxIntensity;
    public float bulbMaxIntensity = 1;
    float intensity;
    float bulbIntensity;

    public GameObject FlyingSpeaker;
    public Vector3 lightPosition;
    public float distance;
    public bool on = false;
    public float triggerDistance = 2.8f;

    public float lightSpeed = 0.2f;
    Renderer rend;
    Color finalColor;
    public float flickerFrequency = 5;
    public float flickerTime = 4;
    float timer;

    //AUDIO
    AudioSource bowlHit1;
    public AudioClip impact;
    public AudioClip impact2;
    public AudioClip hum;
    public float sampleVolume = 0.8f;
    float maxVolume = .2f;
    public float volume;


    GameObject levelManager;
    int level;

    // Use this for initialization
    void Start()
    {
        lightPosition = GetComponent<Transform>().position;
        //maxIntensity = 10;
        intensity = maxIntensity;
        bulbIntensity = bulbMaxIntensity;
        rend = GetComponent<Renderer>();

        //Audio
        bowlHit1 = GetComponent<AudioSource>();
        levelManager = GameObject.Find("LevelManager");
    }

    // Update is called once per frame
    void Update()
    {
        level = levelManager.GetComponent<SendKeyDown>().level;

        SetLightState(1, 2, 1, .4f, .5f, 2);
        SetLightState(2, 4, 1, .5f, 2f, 2);
        SetLightState(3, 8, 2, .6f, 4, 2);
        SetLightState(4, 16, 2, .9f, 8, 2);
        SetLightState(5, 20, 2, .8f, 20, 2);


        //Calculate distance of pendant speaker to this speaker
        Vector3 speakerPosition = FlyingSpeaker.GetComponent<Transform>().position;
        Vector3 diffPosition = speakerPosition - lightPosition;
        distance = diffPosition.magnitude;

        Material mat = rend.material;
        
        Color baseColor = new Color();
        ColorUtility.TryParseHtmlString("#FFF5BF", out baseColor);
        //mat.SetColor("_EmissionColor", finalColor);

        if (distance < triggerDistance)
        {
            if (!on)
            {
                on = true;
                //Reset timer and intensity
                timer = 0;
                intensity = maxIntensity;
                bulbIntensity = bulbMaxIntensity;
                volume = maxVolume;

                GetComponent<AudioSource>().volume = maxVolume;
                if(level >= 3)
                {
                    bowlHit1.PlayOneShot(impact, 5);
                    bowlHit1.PlayOneShot(impact2, .8f);
                }                
                bowlHit1.PlayOneShot(hum, .7f);
            }


        }
        if (on)
        {
            //Bulb color
            float emission = bulbIntensity * (1 - Mathf.Sin(Mathf.PI * 0.5f * Mathf.Repeat(Time.time * flickerFrequency, 1.0f)));
            finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
            mat.SetColor("_EmissionColor", finalColor);

            //Light emission
            GetComponentInChildren<Light>().intensity = intensity * (1 - Mathf.Sin(Mathf.PI * 0.5f * Mathf.Repeat(Time.time * flickerFrequency, 1.0f)));
            timer += Time.deltaTime;
            intensity = maxIntensity - timer * maxIntensity / flickerTime;
            bulbIntensity = bulbMaxIntensity - timer * bulbMaxIntensity / flickerTime;

            if(level < 5)
            {
                GetComponent<AudioSource>().volume = volume/2 + volume / 2 *  Mathf.Cos(Mathf.PI * Mathf.Repeat(Time.time * flickerFrequency, 1.0f));
            }            
            volume = maxVolume - timer * maxVolume / flickerTime;
            //GetComponent<AudioSource>().pitch = (1 - Mathf.Sin(Mathf.PI * 0.5f * Mathf.Repeat(Time.time * flickerFrequency, 1.0f)));
        }
        else
        {
            finalColor = baseColor * Mathf.LinearToGammaSpace(0);
            mat.SetColor("_EmissionColor", finalColor);
        }

        if (GetComponentInChildren<Light>().intensity <= 0)
        {
            on = false;
        }

    }

    void SetLightState (int lvl, float maxIntens, float bulbMaxIntens, float maxVol, float fFreq, float fTime)
    {
        if(level == lvl)
        {
            maxIntensity = maxIntens;
            bulbMaxIntensity = bulbMaxIntens;
            maxVolume = maxVol;
            flickerFrequency = fFreq;
            flickerTime = fTime;

        }        
    }
}
