using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCoinCount : MonoBehaviour
{
    public TMP_Text coinText;
    private int amountOfCoins;

    void Update()
    {
        amountOfCoins = (int)PlayerPrefs.GetFloat("Coins");
        coinText.text = amountOfCoins.ToString();
    }
}
