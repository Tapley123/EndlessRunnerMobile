using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchasingManager : MonoBehaviour
{
    private int coins;
    //public enCharacter skins;

    [Header("Prices")]
    [SerializeField] private int pearlPrice;
    [SerializeField] private TMP_Text pearlPriceText;
    [SerializeField] private int zombiePrice;
    [SerializeField] private TMP_Text zombiePriceText;
    [SerializeField] private int japonesePrice;
    [SerializeField] private TMP_Text japonesePriceText;

    [Header("Dev Stuff")]
    [SerializeField] private TMP_InputField amountOfMoneyToCheatIn;

    void Start()
    {
        pearlPrice = int.Parse(pearlPriceText.text); //makes the price of pearl = to the amount the text displays
        zombiePrice = int.Parse(zombiePriceText.text); //makes the price of zombie = to the amount the text displays
        japonesePrice = int.Parse(japonesePriceText.text); //makes the price of japonese = to the amount the text displays
    }

    
    void Update()
    {
        coins = (int)PlayerPrefs.GetFloat("Coins");
    }


    #region DEV TESTING FEATURES

    public void GiveMoney()
    {
        int amt = int.Parse(amountOfMoneyToCheatIn.text);
        PlayerPrefs.SetFloat("Coins", coins + amt);
    }

    public void ClearMoney()
    {
        PlayerPrefs.SetFloat("Coins", 0);
    }
    #endregion


    #region Buying Buttons
    public void Buy_Skin_Pearl()
    {
        if(coins >= pearlPrice)
        {
            Debug.Log("You Baught Pearl");
            PlayerPrefs.SetFloat("Coins", coins - pearlPrice);
        }
        else
        {
            Debug.Log("You Cant Afford Pearl");
        }
    }

    public void Buy_Skin_Zombie()
    {
        if (coins >= zombiePrice)
        {
            Debug.Log("You Baught zombie");
            PlayerPrefs.SetFloat("Coins", coins - zombiePrice);
        }
        else
        {
            Debug.Log("You Cant Afford zombie");
        }
    }

    public void Buy_Skin_Japonese()
    {
        if (coins >= japonesePrice)
        {
            Debug.Log("You Baught japonese");
            PlayerPrefs.SetFloat("Coins", coins - japonesePrice);
        }
        else
        {
            Debug.Log("You Cant Afford japonese");
        }
    }
    #endregion
}
