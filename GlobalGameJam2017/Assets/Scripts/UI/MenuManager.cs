﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public Transform[] tickets;
    public Image[] ticketsImg;
    public Image logo;
    public Image layout;
    public Image[] creditNames;
    public int choice;

    // Use this for initialization
    void Awake() {
        tickets = new Transform[4];
        ticketsImg = new Image[4];
        creditNames = new Image[6];

        tickets[0] = GameObject.Find("Play").GetComponent<Transform>();
        tickets[1] = GameObject.Find("Help").GetComponent<Transform>();
        tickets[2] = GameObject.Find("Quit").GetComponent<Transform>();
        tickets[3] = GameObject.Find("Credit").GetComponent<Transform>();

        ticketsImg[0] = GameObject.Find("Play").GetComponent<Image>();
        ticketsImg[1] = GameObject.Find("Help").GetComponent<Image>();
        ticketsImg[2] = GameObject.Find("Quit").GetComponent<Image>();
        ticketsImg[3] = GameObject.Find("Credit").GetComponent<Image>();

        creditNames[0] = GameObject.Find("Francis").GetComponent<Image>();
        creditNames[1] = GameObject.Find("Nam").GetComponent<Image>();
        creditNames[2] = GameObject.Find("Maxime").GetComponent<Image>();
        creditNames[3] = GameObject.Find("Julien").GetComponent<Image>();
        creditNames[4] = GameObject.Find("Leandre").GetComponent<Image>();
        creditNames[5] = GameObject.Find("Philippe").GetComponent<Image>();

        layout = GameObject.Find("ControlLayout").GetComponent<Image>();
    }

    void Start()
    {
        modeMenu();
        choice = 1;
        updateChoice();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.DownArrow) && choice < 4 && ticketsImg[0].enabled == true) { choice++; updateChoice(); }
        if (Input.GetKeyDown(KeyCode.UpArrow) && choice > 1 && ticketsImg[0].enabled == true) { choice--; updateChoice(); }
        if (Input.GetKeyDown(KeyCode.Return) && choice == 4 && ticketsImg[0].enabled == true) { modeCredits(); }
        if (Input.GetKeyDown(KeyCode.Return) && choice == 2 && ticketsImg[0].enabled == true) { modeHelp(); }
        if (Input.GetKeyDown(KeyCode.Return) && choice == 3 && ticketsImg[0].enabled == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) { modeMenu(); }
    }

    void updateChoice()
    {
        switch (choice)
        {
            case 1:
                tickets[0].SetAsLastSibling();
                break;
            case 2:
                tickets[1].SetAsLastSibling();
                break;
            case 3:
                tickets[2].SetAsLastSibling();
                break;
            case 4:
                tickets[3].SetAsLastSibling();
                break;
            default:
                break;
        }
    }

    void modeMenu()
    {
        for (int i = 0; i < 4; i++) ticketsImg[i].enabled = true;
        for (int i = 0; i < 6; i++) creditNames[i].enabled = false;
        layout.enabled = false;
    }

    void modeCredits()
    {
        for (int i = 0; i < 4; i++) ticketsImg[i].enabled = false;
        for (int i = 0; i < 6; i++) creditNames[i].enabled = true;
        layout.enabled = false;
    }

    void modeHelp()
    {
        for (int i = 0; i < 4; i++) ticketsImg[i].enabled = false;
        for (int i = 0; i < 6; i++) creditNames[i].enabled = false;
        layout.enabled = true;
    }
}