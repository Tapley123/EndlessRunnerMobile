using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private float score;
    [SerializeField] private TMP_Text scoreText;

    //the higher the difficulty level you are on the more points you get per second
    private int difficultyLevel = 1;
    [SerializeField] private int maxDifficultyLevel = 10;
    private int scoreToNextLevel;




    void Start()
    {
        if (scoreText == null)
            Debug.LogError("Assign the score text to the score script on the player");
    }

    
    void Update()
    {
        if (score >= scoreToNextLevel)
            LevelUP();

        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUP()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2; // makes the next difficulty level twice as long to get to than the previous level
        difficultyLevel++; // increses the difficulty level
    }
}
