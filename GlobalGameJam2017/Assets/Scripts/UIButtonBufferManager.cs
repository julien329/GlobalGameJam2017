using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonBufferManager : MonoBehaviour {

    public Image[] buttons;
    public GameObject[] effects;
    StringEffectManager sem;
    public bool[] buttonIsDisplayed;
    public int index;

    void Awake()
    {
        initialize();
    }

    void initialize()
    {
        buttons = new Image[12];
        effects = new GameObject[12];
        buttonIsDisplayed = new bool[12];
        index = 0;

        buttons[0] = GameObject.Find("A1_Button").GetComponent<Image>();
        buttons[1] = GameObject.Find("A2_Button").GetComponent<Image>();
        buttons[2] = GameObject.Find("A3_Button").GetComponent<Image>();
        buttons[3] = GameObject.Find("B1_Button").GetComponent<Image>();
        buttons[4] = GameObject.Find("B2_Button").GetComponent<Image>();
        buttons[5] = GameObject.Find("B3_Button").GetComponent<Image>();
        buttons[6] = GameObject.Find("X1_Button").GetComponent<Image>();
        buttons[7] = GameObject.Find("X2_Button").GetComponent<Image>();
        buttons[8] = GameObject.Find("X3_Button").GetComponent<Image>();
        buttons[9] = GameObject.Find("Y1_Button").GetComponent<Image>();
        buttons[10] = GameObject.Find("Y2_Button").GetComponent<Image>();
        buttons[11] = GameObject.Find("Y3_Button").GetComponent<Image>();

        effects[0] = GameObject.Find("A1_Effect");
        effects[1] = GameObject.Find("A2_Effect");
        effects[2] = GameObject.Find("A3_Effect");
        effects[3] = GameObject.Find("B1_Effect");
        effects[4] = GameObject.Find("B2_Effect");
        effects[5] = GameObject.Find("B3_Effect");
        effects[6] = GameObject.Find("X1_Effect");
        effects[7] = GameObject.Find("X2_Effect");
        effects[8] = GameObject.Find("X3_Effect");
        effects[9] = GameObject.Find("Y1_Effect");
        effects[10] = GameObject.Find("Y2_Effect");
        effects[11] = GameObject.Find("Y3_Effect");

        sem = GameObject.Find("StringEffect").GetComponent<StringEffectManager>();

        for (int i = 0; i < 12; i++) buttonIsDisplayed[i] = false;
        for (int i = 0; i < 12; i++) effects[i].GetComponent<Image>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 12; i++)
        {
            buttons[i].enabled = buttonIsDisplayed[i];
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button0)) setDisplay(0);
        if (Input.GetKeyDown(KeyCode.Joystick1Button1)) setDisplay(1);
        if (Input.GetKeyDown(KeyCode.Joystick1Button2)) setDisplay(2);
        if (Input.GetKeyDown(KeyCode.Joystick1Button3)) setDisplay(3);
        if (Input.GetKeyDown(KeyCode.Joystick1Button5)) playChord();
    }

    public void setDisplay(int button) // button is 0,1,2,3 for A,B,X,Y
    {
        if (index == 3 || sem.active == true) return;

        switch (button) {
            case 0:
                buttonIsDisplayed[0 + index] = true;
                playEffect(0 + index);
                index++;
                break;
            case 1:
                buttonIsDisplayed[3 + index] = true;
                playEffect(3 + index);
                index++;
                break;
            case 2:
                buttonIsDisplayed[6 + index] = true;
                playEffect(6 + index);
                index++;
                break;
            case 3:
                buttonIsDisplayed[9 + index] = true;
                playEffect(9 + index);
                index++;
                break;
            default:
                break;
        }
    }

    public void playChord()
    {
        if (index == 3)
        {
            sem.active = true;
            index = 0;
        }
    }

    public void playEffect(int effect)
    {
        effects[effect].GetComponent<Image>().enabled = true;
    }
}
