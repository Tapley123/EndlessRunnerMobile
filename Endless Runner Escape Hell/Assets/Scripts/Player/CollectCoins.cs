using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectCoins : MonoBehaviour
{
    #region singleton
    private static CollectCoins _instance;

    public static CollectCoins Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CollectCoins>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    [SerializeField] private TMP_Text amountOfCoinsText;
    public float amountOfCoins;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the player dies
        if (PlayerController.Instance.isDead)
            return;

        amountOfCoinsText.text = amountOfCoins.ToString();
    }

    public void OnDeath()
    {
        DeathMenu.Instance.CoinsCollected(amountOfCoins);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            amountOfCoins++;
        }
    }
}
