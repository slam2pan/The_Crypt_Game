using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndingScore : MonoBehaviour
{
    private TextMeshProUGUI endScore;

    void Start()
    {
        endScore = GetComponent<TextMeshProUGUI>();
        SetFinalScore();
    }

    private void SetFinalScore()
    {
        endScore.SetText("Score: " + Score.GetScore());
    }
}
