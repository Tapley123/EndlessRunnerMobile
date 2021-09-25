using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        PlayButtonClickSound();
        SceneManager.LoadScene(1);
    }

    public void Button_Quit()
    {
        PlayButtonClickSound();
        Application.Quit();
    }
    #endregion
}
