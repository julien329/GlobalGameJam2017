using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScoreHandler {

    static bool isInit = false;
    static public int totalScore;
    static bool[] achievements;
    static int[] ultimates;
    static int[] sequences;
    static int flawlessStreak = 0;
    static string rank;

    static Text Annoucement;
    static Text AnnText;
    static Text Ach1;
    static Text Ach2;
    static Text Ach3;
    static Text Score;

    static List<string> achievedFeats;

    static public void UpdateScore()
    {
        Score.text = totalScore.ToString();
    }

    static public void InitRound()
    {
        if(!isInit)
        {
            isInit = true;
            Annoucement = GameObject.Find("AnnTitle").GetComponent<Text>();
            AnnText = GameObject.Find("AnnText").GetComponent<Text>();
            Ach1 = GameObject.Find("Ach1").GetComponent<Text>();
            Ach2 = GameObject.Find("Ach2").GetComponent<Text>();
            Ach3 = GameObject.Find("Ach3").GetComponent<Text>();
            Score = GameObject.Find("ScoreValue").GetComponent<Text>();

        }

        Annoucement.text = "";
        AnnText.text = "";
        Ach1.text = "";
        Ach2.text = "";
        Ach3.text = "";

        achievedFeats = new List<string>();
        achievements = new bool[(int)Achievement.TOTAL];
        ultimates = new int[4];
        sequences = new int[3];

        for (int i = 0; i < 4; i++)
            ultimates[i] = 0;

        for (int i = 0; i < 3; i++)
            sequences[i] = 0;

        for (int i = 0; i < achievements.Length; ++i)
            achievements[i] = true;

        achievements[(int)Achievement.ONETRICKPONY] = false;
    }

    static public void DamageTaken() {totalScore -= 5; Score.text = totalScore.ToString(); achievements[(int)Achievement.FLAWLESS] = false; }
    static public void UltimateUsed(Ultimate ult) { achievements[(int)Achievement.BICOLOR] = false; achievements[(int)Achievement.TASTETHERAINBOW] = false;
                                        achievements[(int)Achievement.HUMBLE] = false; ultimates[(int)ult]++; }
    static public void BicolorUsed() { achievements[(int)Achievement.TASTETHERAINBOW] = false; }
    static public void TricolorUsed() { achievements[(int)Achievement.BICOLOR] = false; }

    static public void EndOfRound()
    {
        for(int i = 0; i < 4; i++)
        {
            if (ultimates[i] <= 2)
                achievements[(int)Achievement.SHOWOFF] = false;
            else if (ultimates[i] >5)
                achievements[(int)Achievement.ONETRICKPONY] = true;
        }

        //Announce achievements and extra points
        if (achievements[0])
            achievedFeats.Add("Can't touch this! \n Now stop... Hammertime!");
        if (achievements[1])
            achievedFeats.Add("Dyad-y issues! \n Two notes is as far as you'll go!");
        if (achievements[2])
            achievedFeats.Add("Tri-hard! \n Taste the rainbow!");
        if (achievements[3])
            achievedFeats.Add("Bassist! \n You are a humble man!");
        if (achievements[4])
            achievedFeats.Add("Someone feels special! \n Go big or go home!");
        if (achievements[5])
            achievedFeats.Add("One hit wonder! \n We're no strangers to love!");

        switch(achievedFeats.Count)
        {
            case 0:
                rank = "Garage Band \n Nothing to write home about.";
                Annoucement.text = "Garage Band";
                AnnText.text = "\"Nothing to write home about.\"";
                break;
            case 1:
                rank = "Kinda good \n Don't you dare brag about this one. +100 fame";
                Annoucement.text = "Kinda good";
                AnnText.text = "\"Don't you dare brag about this one.\" +100 fame";
                Ach1.text = achievedFeats[0];
                totalScore += 100;
                break;
            case 2:
                rank = "Local Legend \n On a scale from 1 to Batman: Robin. +400 fame";
                Annoucement.text = "Local Legend";
                AnnText.text = "\"On a scale from 1 to Batman: Robin.\" +400 fame";
                Ach1.text = achievedFeats[0];
                Ach2.text = achievedFeats[1];
                totalScore += 400;
                break;
            case 3:
                rank = "Magic Fingers \n Don't need a pick when you got fingers like these. +1000 fame";
                Annoucement.text = "Magic Fingers";
                AnnText.text = "\"Don't need a pick when you got fingers like these.\" +1000 fame";
                Ach1.text = achievedFeats[0];
                Ach2.text = achievedFeats[1];
                Ach3.text = achievedFeats[2];
                totalScore += 1000;
                break;

        }
        Score.text = totalScore.ToString();

    }

    public enum Achievement
    {
        FLAWLESS,
        BICOLOR,
        TASTETHERAINBOW,
        HUMBLE,
        SHOWOFF,
        ONETRICKPONY,
        TOTAL
    }
	
    public enum Ultimate
    {
        HARMONY,
        POWER,
        SPREAD,
        SPEED
    }
}
