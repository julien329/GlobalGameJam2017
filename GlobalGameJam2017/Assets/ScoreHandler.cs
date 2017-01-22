using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreHandler {

    static bool isInit = false;
    static public int totalScore;
    static bool[] achievements;
    static int[] ultimates;
    static int[] sequences;
    static int flawlessStreak = 0;
    static string rank;

    static List<string> achievedFeats;

    static public void InitRound()
    {
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

    static public void DamageTaken() {totalScore -= 5; achievements[(int)Achievement.FLAWLESS] = false; }
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
                break;
            case 1:
                rank = "Kinda good \n Don't you dare brag about this one. +100 fame";
                totalScore += 100;
                break;
            case 2:
                rank = "Local Legend \n On a scale from 1 to Batman: Robin. +400 fame";
                totalScore += 400;
                break;
            case 3:
                rank = "Magic Fingers \n Don't need a pick when you got fingers like these. +1000 fame";
                totalScore += 1000;
                break;
        }
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
