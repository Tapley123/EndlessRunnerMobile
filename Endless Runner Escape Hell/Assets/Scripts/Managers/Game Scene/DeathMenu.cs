using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
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

    [SerializeField] private GameObject panel; //the panel that contains all of the ui for the menu
    [SerializeField] private TMP_Text finalScoreText;

    private bool isShown = false;
    private float transition;

    void Start()
    {
        if(panel != null)
            panel.SetActive(false);
    }

    
    void Update()
    {
        if (!isShown)
            return;

        transition += Time.deltaTime;
        panel.GetComponent<Image>().color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
    }

    public void ToggleEndMenu(float score)
    {
        panel.SetActive(true); //displaying the menu
        isShown = true;

        int s = (int)score; //turning the score into an int so there is no decimals
        finalScoreText.text = s.ToString(); //displaying your final score
        //Debug.Log("Your final score was: " + s);
    }


    #region Buttons

    public void Button_Play()
    {
        AudioManager.Instance.PlayButtonSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //restart the game
    }

    public void Button_Menu()
    {
        AudioManager.Instance.PlayButtonSound();
        SceneManager.LoadScene("Menu"); //go to the game menu

    }
    #endregion
}
