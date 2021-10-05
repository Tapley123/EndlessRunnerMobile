using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enCharacter { zombie, pearl, japonese };


public class DefaultLevelCharacter : MonoBehaviour
{
    #region singleton
    private static DefaultLevelCharacter _instance;

    public static DefaultLevelCharacter Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DefaultLevelCharacter>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    public enCharacter defaultLevelCharacter;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
