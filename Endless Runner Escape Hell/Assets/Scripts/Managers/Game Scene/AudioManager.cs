using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region singleton
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    #endregion
    [Header ("Refs")]
    [SerializeField] private AudioSource playerAudioSource;

    [Header("Tweaking")]
    [SerializeField] [Range(0, 1)] private float buttonClickVolume; 
    [SerializeField] [Range(0, 1)] private float collectCoinVolume; 

    [Header("Audio Clips")]
    public AudioClip sound_ButtonClick;
    public AudioClip sound_CollectCoin;



    public void PlayButtonSound()
    {
        if (playerAudioSource != null)
        {
            playerAudioSource.Stop();
            playerAudioSource.PlayOneShot(sound_ButtonClick, buttonClickVolume);
        }
        else
            Debug.LogError("You dont have an audio source hooked up here");
    }

    public void PlayCoinCollectSound()
    {
        if (playerAudioSource != null)
        {
            playerAudioSource.Stop();
            playerAudioSource.PlayOneShot(sound_CollectCoin, collectCoinVolume);
        }
        else
            Debug.LogError("You dont have an audio source hooked up here");
    }
}
