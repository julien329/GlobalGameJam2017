using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonBufferManager : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    private StringEffectManager stringEffectManager;
    private Image[] buttons;
    private Image[] effects;
    private bool[] buttonIsDisplayed;
    private int slotIndex = 0;
    private const int BUTTON_A = 0;
    private const int BUTTON_B = 1;
    private const int BUTTON_X = 2;
    private const int BUTTON_Y = 3;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        buttons = new Image[12];
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

        effects = new Image[12];
        effects[0] = GameObject.Find("A1_Effect").GetComponent<Image>();
        effects[1] = GameObject.Find("A2_Effect").GetComponent<Image>();
        effects[2] = GameObject.Find("A3_Effect").GetComponent<Image>();
        effects[3] = GameObject.Find("B1_Effect").GetComponent<Image>();
        effects[4] = GameObject.Find("B2_Effect").GetComponent<Image>();
        effects[5] = GameObject.Find("B3_Effect").GetComponent<Image>();
        effects[6] = GameObject.Find("X1_Effect").GetComponent<Image>();
        effects[7] = GameObject.Find("X2_Effect").GetComponent<Image>();
        effects[8] = GameObject.Find("X3_Effect").GetComponent<Image>();
        effects[9] = GameObject.Find("Y1_Effect").GetComponent<Image>();
        effects[10] = GameObject.Find("Y2_Effect").GetComponent<Image>();
        effects[11] = GameObject.Find("Y3_Effect").GetComponent<Image>();

        stringEffectManager = GameObject.Find("StringEffect").GetComponent<StringEffectManager>();
    }


    void Start() {
        buttonIsDisplayed = new bool[12];
        for (int i = 0; i < 12; i++) {
            buttonIsDisplayed[i] = false;
            effects[i].enabled = false;
        }
    }
	

	void Update () {
        for (int i = 0; i < 12; i++) {
            buttons[i].enabled = buttonIsDisplayed[i];
        }

        if (Input.GetButtonDown("Heal"))
            SetDisplay(BUTTON_A);
        if (Input.GetButtonDown("Damage"))
            SetDisplay(BUTTON_B);
        if (Input.GetButtonDown("Radius"))
            SetDisplay(BUTTON_X);
        if (Input.GetButtonDown("Speed"))
            SetDisplay(BUTTON_Y);
        if (Input.GetAxisRaw("Play Chord") != 0)
            PlayChord();
        if (Input.GetAxisRaw("Clear Chord") != 0)
            ClearChord();
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////

    private void SetDisplay(int button) {
        if (slotIndex == 3 || stringEffectManager.GetIsActive() == true) return;

        switch (button) {
            case 0:
                buttonIsDisplayed[0 + slotIndex] = true;
                PlayEffect(0 + slotIndex);
                slotIndex++;
                break;
            case 1:
                buttonIsDisplayed[3 + slotIndex] = true;
                PlayEffect(3 + slotIndex);
                slotIndex++;
                break;
            case 2:
                buttonIsDisplayed[6 + slotIndex] = true;
                PlayEffect(6 + slotIndex);
                slotIndex++;
                break;
            case 3:
                buttonIsDisplayed[9 + slotIndex] = true;
                PlayEffect(9 + slotIndex);
                slotIndex++;
                break;
            default:
                break;
        }
    }


    private void PlayChord() {
        if (slotIndex == 3) {
            stringEffectManager.SetIsActive(true);
            slotIndex = 0;
        }
    }


    private void ClearChord() {
        slotIndex = 0;
        for (int i = 0; i < buttonIsDisplayed.Length; i++) {
            buttonIsDisplayed[i] = false;
        }
    }


    private void PlayEffect(int effect) {
        effects[effect].enabled = true;
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// GET / SET
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public void SetButtonIsDisplayed(int index, bool value) {
        buttonIsDisplayed[index] = value;
    }
}
