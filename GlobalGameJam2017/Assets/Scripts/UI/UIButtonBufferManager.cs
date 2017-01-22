using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonBufferManager : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public int coolDownTime = 5;
    public float soundVolume = 0.01f;
    public float soundFadeSpeed = 9f;
    public AudioClip[] singleNotesClips;
    public AudioClip[] specialCombosClips;
    public AudioClip[] longFinishersClips;

    private StringEffectManager stringEffectManager;
    private PlayerMovement playerMovement;
    private WaveController waveController;
    private Image[] buttons;
    private Image[] effects;
    private RawImage[] cooldownLines;
    private AudioSource[] singleNotesPlayers;
    private AudioSource specialComboSource;
    private AudioSource regularAttackSource;
    private bool[] buttonIsDisplayed;
    private bool[] buttonOnCooldown;
    private int slotIndex = 0;
    private GuitarInput[] guitarInputs;
    private int countDown;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
        buttons = new Image[12];
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        waveController = player.GetComponent<WaveController>();

        guitarInputs = new GuitarInput[3];

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

        cooldownLines = new RawImage[4];
        cooldownLines[0] = GameObject.Find("LineBloc4").GetComponent<RawImage>();
        cooldownLines[1] = GameObject.Find("LineBloc3").GetComponent<RawImage>();
        cooldownLines[2] = GameObject.Find("LineBloc2").GetComponent<RawImage>();
        cooldownLines[3] = GameObject.Find("LineBloc1").GetComponent<RawImage>();

        singleNotesPlayers = new AudioSource[3];

        stringEffectManager = GameObject.Find("StringEffect").GetComponent<StringEffectManager>();
    }


    void Start() {
        buttonIsDisplayed = new bool[12];
        for (int i = 0; i < 12; i++) {
            buttonIsDisplayed[i] = false;
            effects[i].enabled = false;
        }

        buttonOnCooldown = new bool[4];
        for(int i = 0; i < 4; i++) {
            buttonOnCooldown[i] = false;
        }

        for(int i = 0; i < 3; i++) {
            singleNotesPlayers[i] = gameObject.AddComponent<AudioSource>();
            singleNotesPlayers[i].playOnAwake = false;
            singleNotesPlayers[i].loop = false;
            singleNotesPlayers[i].volume = soundVolume;
        }

        specialComboSource = gameObject.AddComponent<AudioSource>();
        specialComboSource.playOnAwake = false;
        specialComboSource.loop = false;
        specialComboSource.volume = 1.0f;

        regularAttackSource = gameObject.AddComponent<AudioSource>();
        regularAttackSource.playOnAwake = false;
        regularAttackSource.loop = false;
        regularAttackSource.volume = soundVolume;

    }


    void Update() {
        if (playerMovement) {
            for (int i = 0; i < 12; i++) {
                buttons[i].enabled = buttonIsDisplayed[i];
            }

            if (Input.GetButtonDown("Heal") && !buttonOnCooldown[0])
                SetDisplay(GuitarInput.A_HEAL);
            if (Input.GetButtonDown("Damage") && !buttonOnCooldown[1])
                SetDisplay(GuitarInput.B_POWER);
            if (Input.GetButtonDown("Radius") && !buttonOnCooldown[2])
                SetDisplay(GuitarInput.X_SPREAD);
            if (Input.GetButtonDown("Speed") && !buttonOnCooldown[3])
                SetDisplay(GuitarInput.Y_SPEED);
            if (Input.GetAxisRaw("Play Chord") != 0)
                PlayChord();
            if (Input.GetAxisRaw("Clear Chord") != 0)
                ClearChord();
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    /////////////////////////////////////////////////////////////////////////////////////////////////

    private void SetDisplay(GuitarInput button) {
        if (slotIndex == 3 || stringEffectManager.GetIsActive() == true) return;

        AudioSource activeSource = singleNotesPlayers[slotIndex];
        activeSource.volume = soundVolume;
        int index = ((int)button - 1) * 2;
        activeSource.clip = GetAndSwitchClips(index, index + 1);
        activeSource.Play();

        switch (button) {
            case GuitarInput.A_HEAL:
                buttonIsDisplayed[0 + slotIndex] = true;
                PlayEffect(0 + slotIndex);
                guitarInputs[slotIndex] = GuitarInput.A_HEAL;
                slotIndex++;
                break;
            case GuitarInput.B_POWER:
                buttonIsDisplayed[3 + slotIndex] = true;
                PlayEffect(3 + slotIndex);
                guitarInputs[slotIndex] = GuitarInput.B_POWER;
                slotIndex++;
                break;
            case GuitarInput.X_SPREAD:
                buttonIsDisplayed[6 + slotIndex] = true;
                PlayEffect(6 + slotIndex);
                guitarInputs[slotIndex] = GuitarInput.X_SPREAD;
                slotIndex++;
                break;
            case GuitarInput.Y_SPEED:
                buttonIsDisplayed[9 + slotIndex] = true;
                PlayEffect(9 + slotIndex);
                guitarInputs[slotIndex] = GuitarInput.Y_SPEED;
                slotIndex++;
                break;
            default:
                break;
        }
    }


    private void PlayChord() {
        if (slotIndex != 3) return;
        stringEffectManager.SetIsActive(true);
        slotIndex = 0;
        waveController.chargeWave(guitarInputs);
        playerMovement.StartAttackAnim();
    }


    private void ClearChord() {
        slotIndex = 0;
        for (int i = 0; i < buttonIsDisplayed.Length; i++) {
            buttonIsDisplayed[i] = false;
        }
        for (int i = 0; i < singleNotesPlayers.Length; i++) {
            singleNotesPlayers[i].volume = Mathf.Lerp(singleNotesPlayers[i].volume, 0, Time.deltaTime * soundFadeSpeed);
        }
    }


    private void PlayEffect(int effect) {
        effects[effect].enabled = true;
    }


    private AudioClip GetAndSwitchClips(int index1, int index2) {
        AudioClip temp = singleNotesClips[index1];
        singleNotesClips[index1] = singleNotesClips[index2];
        singleNotesClips[index2] = temp;
        return temp;
    }


    public void PlayRegularAttack() {
        regularAttackSource.clip = longFinishersClips[Random.Range(0, longFinishersClips.Length)];
        regularAttackSource.Play();
    }


    public IEnumerator AbilityCooldown(int index) {
        specialComboSource.clip = specialCombosClips[index];
        specialComboSource.Play();

        buttonOnCooldown[index] = true;
        cooldownLines[index].enabled = true;

        countDown = coolDownTime;
        while (countDown > 0) {
            yield return new WaitForSeconds(1f);
            countDown--;
        }

        buttonOnCooldown[index] = false;
        cooldownLines[index].enabled = false;
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// GET / SET
    /////////////////////////////////////////////////////////////////////////////////////////////////

    public void SetButtonIsDisplayed(int index, bool value) {
        buttonIsDisplayed[index] = value;
    }


    public void SetHealingDisabled() {
        StartCoroutine(AbilityCooldown((int)GuitarInput.A_HEAL - 1));
    }


    public void SetPowerDisabled() {
        StartCoroutine(AbilityCooldown((int)GuitarInput.B_POWER - 1));
    }


    public void SetRadiusDisabled() {
        StartCoroutine(AbilityCooldown((int)GuitarInput.X_SPREAD - 1));
    }


    public void SetSpeedDisabled() {
        StartCoroutine(AbilityCooldown((int)GuitarInput.Y_SPEED - 1));
    }
}
