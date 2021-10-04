using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    #region singleton
    private static Score _instance;

    public static Score Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Score>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    public float score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private DeathMenu deathMenu;

    //the higher the difficulty level you are on the more points you get per second
    private int difficultyLevel = 1;
    [SerializeField] private int maxDifficultyLevel = 10;
    [SerializeField] private int scoreToNextLevel = 10;


    void Start()
    {
        if (scoreText == null)
            Debug.LogError("Assign the score text to the score script on the player");

        deathMenu = GameObject.FindObjectOfType<DeathMenu>();
    }

    
    void Update()
    {
        //if the player dies
        if (PlayerController.Instance.isDead)
            return;
            

        if (score >= scoreToNextLevel)
            LevelUP();

        score += Time.deltaTime * difficultyLevel; //Increases score over time // get more score per second the higher the difficulty level
        scoreText.text = ((int)score).ToString();
    }

    public void LevelUP()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2; // makes the next difficulty level twice as long to get to than the previous level
        difficultyLevel++; // increses the difficulty level

        GetComponent<PlayerController>().NextSpeedLevel(difficultyLevel); //increases the players movement speed when the difficulty level is increased
        //Debug.Log("Difficult Level = " + difficultyLevel);
    }

    public void OnDeath()
    {
        //if the score is higher than the previos highscore then make it the new highscore
        if (PlayerPrefs.GetFloat("Highscore") < score)
            PlayerPrefs.SetFloat("Highscore", score);

        DeathMenu.Instance.ToggleEndMenu(score); //sends the score reached to the death menu
    }
}
