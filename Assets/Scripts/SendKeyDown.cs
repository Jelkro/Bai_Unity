using UnityEngine;
using System.Collections;

public class SendKeyDown : MonoBehaviour {

	public OSC osc;
    public int level = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        SetLevel(KeyCode.Alpha1, 1);
        SetLevel(KeyCode.Alpha2, 2);
        SetLevel(KeyCode.Alpha3, 3);
        SetLevel(KeyCode.Alpha4, 4);
        SetLevel(KeyCode.Alpha5, 5);
    }
    

    void SetLevel(KeyCode kc, int lvl)
    {
        if (Input.GetKeyDown(kc))
        {
            level = lvl;

            //Send level
            OscMessage message;
            message = new OscMessage();
            message.address = "/Level";
            message.values.Add(level);
            osc.Send(message);

        }
    }

	
}
