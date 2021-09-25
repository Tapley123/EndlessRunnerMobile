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

    [SerializeField] private AudioSource playerAudioSource;
    public AudioClip sound_ButtonClick;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void PlayButtonSound()
    {
        playerAudioSource.Stop();
        playerAudioSource.PlayOneShot(sound_ButtonClick);
    }
}
