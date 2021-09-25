using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    #region singleton
    private static DeathMenu _instance;

    public static DeathMenu Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DeathMenu>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    [Header("Audio")]
    public AudioSource audioS;
    public AudioClip sound_ButtonClick;

    [Header("Scene Managment")]
    [SerializeField] private int gameSceneIndex = 1;

    [Header("UI")]
    public TMP_Text highScoreText;
    private int highScore;

    private void Start()
    {
        highScore = (int)PlayerPrefs.GetFloat("Highscore");
        highScoreText.text = "Highscore: " + highScore.ToString();
    }

    #region Audio
    private void PlayButtonClickSound()
    {
        audioS.Stop();
        audioS.PlayOneShot(sound_ButtonClick);
    }
    #endregion



    #region Buttons
    public void Button_Play()
    {
        PlayButtonClickSound(); //play the button click sound effect
        StartCoroutine(Button_PlayCoroutine(sound_ButtonClick.length)); //delay so the whole button sound effect is not cut off
    }
    private IEnumerator Button_PlayCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(gameSceneIndex); //load the game level
    }

    public void Button_Quit()
    {
        PlayButtonClickSound(); //play the button click sound effect
        StartCoroutine(Button_QuitCoroutine(sound_ButtonClick.length)); //delay so the whole button sound effect is not cut off
    }
    private IEnumerator Button_QuitCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit(); //exits the game
    }
    #endregion
}
