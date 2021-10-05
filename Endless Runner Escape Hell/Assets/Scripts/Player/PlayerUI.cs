using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
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
        adButtonPanel.SetActive(true);
        noAdButtonPanel.SetActive(false);
    }

    public void DeactivateAdButtons()
    {
        adButtonPanel.SetActive(false);
        noAdButtonPanel.SetActive(true);
    }
}
