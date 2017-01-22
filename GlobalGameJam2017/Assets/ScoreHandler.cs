using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreHandler {

    static int totalScore;
    static bool[] achievements;
    static int[] ultimates;
    static int[] sequences;
    static int flawlessStreak = 0;

    static public void InitRound()
    {
        achievements = new bool[(int)Achievement.TOTAL];
        ultimates = new int[4];

        for (int i = 0; i < 4; i++)
            ultimates[i] = 0;

        for (int i = 0; i < 3; i++)
            sequences[i] = 0;

        for (int i = 0; i < achievements.Length; ++i)
            achievements[i] = true;

        achievements[(int)Achievement.ONETRICKPONY] = false;
    }

    static public void DamageTaken() { achievements[(int)Achievement.FLAWLESS] = false; }
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
        SPREAD,
        SPEED,
        POWER,
        HARMONY
    }
}
