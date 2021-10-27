using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchasingManager : MonoBehaviour
{
    #region singleton
    private static PurchasingManager _instance;

    public static PurchasingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PurchasingManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    private int coins;
    //public enCharacter skins;

    [Header("Prices")]
    [SerializeField] private int pearlPrice;
    [SerializeField] private TMP_Text pearlPriceText;
    [SerializeField] private int patientZeroPrice;
    [SerializeField] private TMP_Text patientZeroPriceText;
    [SerializeField] private int theEmpressPrice;
    [SerializeField] private TMP_Text theEmpressPriceText;

    [Header("Buttons")]
    [SerializeField] private GameObject pearlButton;
    [SerializeField] private GameObject patientZeroButton;
    [SerializeField] private GameObject theEmpressButton;

    [Header("Bools")]
    public bool ownPearl = false;
    public bool ownPatientZero = false;
    public bool ownTheEmpress = false;

    [Header("Dev Stuff")]
    [SerializeField] private TMP_InputField amountOfMoneyToCheatIn;

    void Start()
    {
        pearlPrice = int.Parse(pearlPriceText.text); //makes the price of pearl = to the amount the text displays
        patientZeroPrice = int.Parse(patientZeroPriceText.text); //makes the price of zombie = to the amount the text displays
        theEmpressPrice = int.Parse(theEmpressPriceText.text); //makes the price of japonese = to the amount the text displays

        DisplayTheBoughtIcons();
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
        //buy pearl
        if(coins >= pearlPrice)
        {
            Debug.Log("You Baught Pearl");
            PlayerPrefs.SetFloat("Coins", coins - pearlPrice);
            ownPearl = true;

            pearlButton.GetComponent<BuySkin>().BuyThisSkin();
        }
        else //cant afford pearl
        {
            Debug.Log("You Cant Afford Pearl");
        }
    }

    public void Buy_Skin_PatientZero()
    {
        //buy patient zero
        if (coins >= patientZeroPrice)
        {
            Debug.Log("You Baught PatientZero");
            PlayerPrefs.SetFloat("Coins", coins - patientZeroPrice);
            ownPatientZero = true;

            patientZeroButton.GetComponent<BuySkin>().BuyThisSkin();
        }
        else //cant afford patient zero
        {
            Debug.Log("You Cant Afford PatientZero");
        }
    }

    public void Buy_Skin_TheEmpress()
    {
        //buy The Empress
        if (coins >= theEmpressPrice)
        {
            Debug.Log("You Baught The Empress");
            PlayerPrefs.SetFloat("Coins", coins - theEmpressPrice);
            ownTheEmpress = true;

            theEmpressButton.GetComponent<BuySkin>().BuyThisSkin();
        }
        else //cant afford The Empress
        {
            Debug.Log("You Cant Afford The Empress");
        }
    }
    #endregion

    void DisplayTheBoughtIcons()
    {
        //Pearl
        if(ownPearl)
        {
            pearlButton.GetComponent<BuySkin>().BoughtImage.SetActive(true);
        }
        else
        {
            pearlButton.GetComponent<BuySkin>().BoughtImage.SetActive(false);
        }


        //Patient Zero
        if (ownPatientZero)
        {
            patientZeroButton.GetComponent<BuySkin>().BoughtImage.SetActive(true);
        }
        else
        {
            patientZeroButton.GetComponent<BuySkin>().BoughtImage.SetActive(false);
        }


        //The Empress
        if (ownTheEmpress)
        {
            theEmpressButton.GetComponent<BuySkin>().BoughtImage.SetActive(true);
        }
        else
        {
            theEmpressButton.GetComponent<BuySkin>().BoughtImage.SetActive(false);
        }
    }
}
