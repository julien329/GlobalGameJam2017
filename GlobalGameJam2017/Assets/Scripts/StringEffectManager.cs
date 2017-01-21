using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringEffectManager : MonoBehaviour {

    public int index;
    public int counter;
    public int limit;
    public bool active;

    public GameObject[] strings;

    // Use this for initialization
    void Start () {
        index = 0;
        counter = 0;
        limit = 3;
        active = false;

        strings = new GameObject[6];

        strings[0] = GameObject.Find("StringEffect1");
        strings[1] = GameObject.Find("StringEffect2");
        strings[2] = GameObject.Find("StringEffect3");
        strings[3] = GameObject.Find("StringEffect4");
        strings[4] = GameObject.Find("StringEffect5");
        strings[5] = GameObject.Find("StringEffect6");

        for (int i = 0; i < 6; i++) strings[i].GetComponent<Image>().enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (active)
        {
            switch (index)
            {
                case 0:
                    strings[0].GetComponent<Image>().enabled = true;
                    break;
                case 1:
                    strings[0].GetComponent<Image>().enabled = false;
                    strings[1].GetComponent<Image>().enabled = true;
                    for (int i = 0; i < 3; i++) GameObject.Find("Tab").GetComponent<UIButtonBufferManager>().buttonIsDisplayed[9 + i] = false;
                    break;
                case 2:
                    strings[1].GetComponent<Image>().enabled = false;
                    strings[2].GetComponent<Image>().enabled = true;
                    for (int i = 0; i < 3; i++) GameObject.Find("Tab").GetComponent<UIButtonBufferManager>().buttonIsDisplayed[6 + i] = false;
                    break;
                case 3:
                    strings[2].GetComponent<Image>().enabled = false;
                    strings[3].GetComponent<Image>().enabled = true;
                    for (int i = 0; i < 3; i++) GameObject.Find("Tab").GetComponent<UIButtonBufferManager>().buttonIsDisplayed[3 + i] = false;
                    break;
                case 4:
                    strings[3].GetComponent<Image>().enabled = false;
                    strings[4].GetComponent<Image>().enabled = true;
                    for (int i = 0; i < 3; i++) GameObject.Find("Tab").GetComponent<UIButtonBufferManager>().buttonIsDisplayed[0 + i] = false;
                    break;
                case 5:
                    strings[4].GetComponent<Image>().enabled = false;
                    strings[5].GetComponent<Image>().enabled = true;
                    break;
                default:
                    break;
            }

            counter++;

            if (counter >= limit)
            {
                counter = 0;
                index++;
            }

            if (index == 6)
            {
                index = 0;
                strings[5].GetComponent<Image>().enabled = false;
                active = false;
            }
        } 
	}
}
