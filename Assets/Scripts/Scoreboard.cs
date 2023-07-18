using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    TMP_Text scoreText;
    int score;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }
    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        scoreText.text = "Score: " + score.ToString();
    }
}
