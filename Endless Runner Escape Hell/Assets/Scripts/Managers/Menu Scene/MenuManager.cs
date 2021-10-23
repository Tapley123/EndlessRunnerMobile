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
    #endregion

    [Header("Audio")]
    public AudioSource audioS;
    public AudioClip sound_ButtonClick;

    [Header("Scene Managment")]
    [SerializeField] private int hellSceneIndex = 1;
    [SerializeField] private int overworldSceneIndex = 2;

    [Header("UI")]
    public TMP_Text highScoreText;
    private int highScore;
    public TMP_Text coinText;
    private int amountOfCoins;

    [Header("Panels")]
    public List<GameObject> Panels;

    void Awake()
    {
        Button_ToMainPanel();
    }

    private void Start()
    {
        highScore = (int)PlayerPrefs.GetFloat("Highscore");
        highScoreText.text = "Highscore: " + highScore.ToString();

        //amountOfCoins = (int)PlayerPrefs.GetFloat("Coins");
        //coinText.text = amountOfCoins.ToString();
    }

    

    #region Audio
    private void PlayButtonClickSound()
    {
        audioS.Stop();
        audioS.PlayOneShot(sound_ButtonClick);
    }
    #endregion



    #region Buttons
    public void Button_PlayHell()
    {
        PlayButtonClickSound(); //play the button click sound effect
        StartCoroutine(Button_PlayCoroutine(sound_ButtonClick.length, hellSceneIndex)); //delay so the whole button sound effect is not cut off
    }
    public void Button_PlayOverworld()
    {
        PlayButtonClickSound(); //play the button click sound effect
        StartCoroutine(Button_PlayCoroutine(sound_ButtonClick.length, overworldSceneIndex)); //delay so the whole button sound effect is not cut off
    }

    private IEnumerator Button_PlayCoroutine(float time, int levelIndex)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(levelIndex); //load the game level
    }


    public void Button_ToMainPanel()
    {
        Panels[0].SetActive(true);

        for (int i = 1; i < Panels.Count; i++)
        {
            Panels[i].SetActive(false);
        }
    }

    public void Button_ToShopPanel()
    {
        //shop panel index = 1

        for (int i = 0; i < Panels.Count; i++)
        {
            if (i != 1)
                Panels[i].SetActive(false);
            else
                Panels[i].SetActive(true);
        }
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
