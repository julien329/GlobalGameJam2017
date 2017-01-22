using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HealthKnobManager : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public struct pair { public bool isRed; public float angle; }

    RectTransform knobTransform;
    pair[] knobPositions;
    Image[] dashes;
    int knobValue { get; set; }


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        knobTransform = GameObject.Find("Knob").GetComponent<RectTransform>();

        dashes = new Image[19];
        dashes[18] = GameObject.Find("Dash1").GetComponent<Image>();
        dashes[17] = GameObject.Find("Dash2").GetComponent<Image>();
        dashes[16] = GameObject.Find("Dash3").GetComponent<Image>();
        dashes[15] = GameObject.Find("Dash4").GetComponent<Image>();
        dashes[14] = GameObject.Find("Dash5").GetComponent<Image>();
        dashes[13] = GameObject.Find("Dash6").GetComponent<Image>();
        dashes[12] = GameObject.Find("Dash7").GetComponent<Image>();
        dashes[11] = GameObject.Find("Dash8").GetComponent<Image>();
        dashes[10] = GameObject.Find("Dash9").GetComponent<Image>();
        dashes[9] = GameObject.Find("Dash10").GetComponent<Image>();
        dashes[8] = GameObject.Find("Dash11").GetComponent<Image>();
        dashes[7] = GameObject.Find("Dash12").GetComponent<Image>();
        dashes[6] = GameObject.Find("Dash13").GetComponent<Image>();
        dashes[5] = GameObject.Find("Dash14").GetComponent<Image>();
        dashes[4] = GameObject.Find("Dash15").GetComponent<Image>();
        dashes[3] = GameObject.Find("Dash16").GetComponent<Image>();
        dashes[2] = GameObject.Find("Dash17").GetComponent<Image>();
        dashes[1] = GameObject.Find("Dash18").GetComponent<Image>();
        dashes[0] = GameObject.Find("Dash19").GetComponent<Image>();
    }

    void Start() {
        knobPositions = new pair[19];
        for (int i = 0; i < 19; i++) {
            knobPositions[i].isRed = true;
            knobPositions[i].angle = 90 - i*10;
        }
        knobValue = 18;
    }


	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow) && knobValue < 18) {
            knobValue++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && knobValue > 0) {
            knobValue--;
        }

        for (int i = 0; i < 19; i++) {
            if (i > knobValue) {
                knobPositions[i].isRed = false;
            }
            else {
                knobPositions[i].isRed = true;
            }
        }

        for (int i = 0; i < 19; i++) {
            dashes[i].enabled = knobPositions[i].isRed;
        }

        knobTransform.transform.localRotation = Quaternion.Euler(knobTransform.transform.localRotation.x, knobTransform.transform.localRotation.y, knobPositions[knobValue].angle);
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public void damage(float value, float max) {
        float damTaken = 19.0f * value / max;
        knobValue -= (int)(Mathf.Round(damTaken));
        if (knobValue < 0) knobValue = 0;
    }


    public void heal(float value, float max) {
        float healAmount = 19.0f * value / max;
        knobValue += (int)(Mathf.Round(healAmount));
        if (knobValue > 18) knobValue = 18;
    }
}
