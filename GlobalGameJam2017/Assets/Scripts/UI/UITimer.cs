using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimer : MonoBehaviour {

    int minutes;
    int seconds;

	// Use this for initialization
	void Start () {
        minutes = 0;
        seconds = 0;
	}

    public void SetTime(float time)
    {
        minutes = Mathf.FloorToInt(time / 60.0f);
        seconds = Mathf.FloorToInt(time) % 60;

        //Set text for minutes
        //if(seconds < 10)
        //  gameObject.GetComponent<Text>().text = minutes + ":0" + seconds;
        //else
        //  gameObject.GetComponent<Text>().text = minutes +":"+ seconds;

    }
}
