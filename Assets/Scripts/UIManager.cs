using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI throwableLights;
    [SerializeField] GameObject ControlText; 

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void updateThrowableLightUI(int numberOfThrowableLights)
    {
        throwableLights.text = "Number of Throwable Lights: " + numberOfThrowableLights;
    }

    bool controlsVisible; 

    public void HideOrUnHideControls()
    {
        if(controlsVisible)
        {
            controlsVisible = false;
            ControlText.SetActive(false); 
        }
        else
        {
            controlsVisible = true;
            ControlText.SetActive(true);
        }
    }
}
