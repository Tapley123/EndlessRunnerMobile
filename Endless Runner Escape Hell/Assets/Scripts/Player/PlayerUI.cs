using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    #region Check if Mobile
#if UNITY_IOS || UNITY_ANDROID
    private bool mobile = true;
#else
    private bool mobile = false;
#endif
    #endregion

    public GameObject adButtonPanel;
    public GameObject noAdButtonPanel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateAdButtons()
    {
        if(mobile)
        {
            adButtonPanel.SetActive(true);
            noAdButtonPanel.SetActive(false);
        }
        else
        {
            noAdButtonPanel.SetActive(true);
            adButtonPanel.SetActive(false);
        }
    }

    public void DeactivateAdButtons()
    {
        if(mobile)
        {
            adButtonPanel.SetActive(false);
            noAdButtonPanel.SetActive(true);
        }
    }
}
