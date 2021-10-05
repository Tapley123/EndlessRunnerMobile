using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipePlayerPrefs : MonoBehaviour
{
    public bool wipeCoins = false;
    public bool wipeHighScore = false;


    void Start()
    {
        if(wipeCoins)
            PlayerPrefs.SetFloat("Coins", 0);

        if(wipeHighScore)
            PlayerPrefs.SetFloat("Highscore", 0);
    }
}
