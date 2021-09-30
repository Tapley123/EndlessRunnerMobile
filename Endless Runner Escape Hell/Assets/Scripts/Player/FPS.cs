using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    private TMP_Text fpsText;
    private float deltaTime;

    private void Start()
    {
        fpsText = this.GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (fpsText != null)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
        }
        else
            Debug.LogError("Assign A TMP_Text To the FPS Script on the player");
    }
}
