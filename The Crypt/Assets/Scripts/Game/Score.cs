using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Static class for score to be accessed between scenes
public static class Score
{
    private static int score;
    
    public static int GetScore()
    {
        return score;
    }

    public static void AddToScore(int changeAmount)
    {
        score += changeAmount;
    }

    public static void ResetScore() 
    {
        score = 0;
    }

}
