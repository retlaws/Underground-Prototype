using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class LightController : MonoBehaviour
{


    [SerializeField] LightObject equippedLight;
    [SerializeField] List<LightObject> lights;


    private void Start()
    {
        TurnOffAllLights();
        EquipLight(0); // this is just starting the list at the start each time

    }

    private void TurnOffAllLights()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].parentObject.SetActive(false);
        }
    }

    private void Update() // need to improve this so it is a percentage reduction so it fades out a bit slowerwq
    {
        equippedLight.lightItem.intensity -= equippedLight.amountToReducePerSecond * Time.deltaTime;
        equippedLight.lightItem.spotAngle -= (equippedLight.amountToReducePerSecond * Time.deltaTime) / 2;
    }

    public void PowerUpLight(float powerUpAmount)
    {
        if(!equippedLight.isRechargable) { return; }

        if(equippedLight.lightItem.intensity + powerUpAmount > equippedLight.maxBrightness)
        {
            equippedLight.lightItem.intensity = equippedLight.maxBrightness;
        }
        else
        {
            equippedLight.lightItem.intensity += powerUpAmount;
        }

        if(equippedLight.lightItem.spotAngle + powerUpAmount > 75)
        {
            equippedLight.lightItem.spotAngle = 75;
        }
        else
        {
            equippedLight.lightItem.spotAngle += powerUpAmount;
        }
    }

    int currentIndex = 0;

    public void ScrollThroughDifferentLights(float scrollValue)
    {
        int numberOfLights = lights.Count;

        if(scrollValue > 0)
        {
            currentIndex++; 
            if(currentIndex < numberOfLights)
            {
                EquipLight(currentIndex);
            }
            else
            {
                currentIndex = 0;
                EquipLight(0);
            }
        }
        else if(scrollValue < 0)
        {
            currentIndex--;
            if(currentIndex >= 0)
            {
                EquipLight(currentIndex);
            }
            else
            {
                currentIndex = numberOfLights - 1;
                EquipLight(currentIndex);
            }
        }
    }

    private void EquipLight(int lightIndex)
    {
        equippedLight.parentObject.SetActive(false); 
        equippedLight = lights[lightIndex];
        equippedLight.parentObject.SetActive(true);
    }
}
