using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StringEffectManager : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public int lifeTimeMax = 3;

    private UIButtonBufferManager buttonBufferManager;
    private Image[] strings;
    private bool active = false;
    private int lifeTime = 0;
    private int index = 0;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        strings = new Image[6];
        strings[0] = GameObject.Find("StringEffect1").GetComponent<Image>();
        strings[1] = GameObject.Find("StringEffect2").GetComponent<Image>();
        strings[2] = GameObject.Find("StringEffect3").GetComponent<Image>();
        strings[3] = GameObject.Find("StringEffect4").GetComponent<Image>();
        strings[4] = GameObject.Find("StringEffect5").GetComponent<Image>();
        strings[5] = GameObject.Find("StringEffect6").GetComponent<Image>();

        buttonBufferManager = GameObject.Find("Tab").GetComponent<UIButtonBufferManager>();
    }


    void Start () {
        for (int i = 0; i < 6; i++) {
            strings[i].GetComponent<Image>().enabled = false;
        }
    }
	

	void Update () {
        if (active) {
            switch (index) {
                case 0:
                    strings[0].enabled = true;
                    break;
                case 1:
                    strings[0].enabled = false;
                    strings[1].enabled = true;
                    for (int i = 0; i < 3; i++) {
                        buttonBufferManager.SetButtonIsDisplayed(9 + i, false);
                    }
                    break;
                case 2:
                    strings[1].enabled = false;
                    strings[2].enabled = true;
                    for (int i = 0; i < 3; i++) {
                        buttonBufferManager.SetButtonIsDisplayed(6 + i, false);
                    }
                    break;
                case 3:
                    strings[2].enabled = false;
                    strings[3].enabled = true;
                    for (int i = 0; i < 3; i++) {
                        buttonBufferManager.SetButtonIsDisplayed(3 + i, false);
                    }
                    break;
                case 4:
                    strings[3].enabled = false;
                    strings[4].enabled = true;
                    for (int i = 0; i < 3; i++) {
                        buttonBufferManager.SetButtonIsDisplayed(0 + i, false);
                    }
                    break;
                case 5:
                    strings[4].enabled = false;
                    strings[5].enabled = true;
                    break;
                default:
                    break;
            }

            lifeTime++;

            if (lifeTime >= lifeTimeMax) {
                lifeTime = 0;
                index++;
            }

            if (index == 6) {
                index = 0;
                strings[5].enabled = false;
                active = false;
            }
        } 
	}


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// GET / SET
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public bool GetIsActive() {
        return active;
    }


    public void SetIsActive(bool active) {
        this.active = active;
    }
}
