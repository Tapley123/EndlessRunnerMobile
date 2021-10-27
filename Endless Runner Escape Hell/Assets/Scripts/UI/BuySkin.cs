using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySkin : MonoBehaviour
{
    public GameObject BoughtImage;

    public void BuyThisSkin()
    {
        BoughtImage.SetActive(true);
    }
}
