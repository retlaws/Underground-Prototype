using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LightController : MonoBehaviour
{


    public LightObject equippedLight;
    [SerializeField] List<LightObject> lights;
    [SerializeField] Transform instantiationPoint;
    [SerializeField] Transform headTransform;
    [SerializeField] int numberOfThrowableLights = 10;

    PlayerInteract playerInteract;

    private void Start()
    {
        TurnOffAllLights();
        EquipLight(0); // this is just starting the list at the start each time
        UIManager.Instance.updateThrowableLightUI(numberOfThrowableLights);
        playerInteract = GetComponent<PlayerInteract>();
    }

    private void TurnOffAllLights()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].parentObject.SetActive(false);
        }
    }

    public void PowerUpLight(float powerUpAmount)
    {
        if(!equippedLight.isRechargable) { return; }
        equippedLight.PowerUpLight(powerUpAmount);
        
    }



    public void PickupLightItem(LightObject lightPickup, GameObject pickup)
    {
        for (int i = 0; i < lights.Count; i++)
        {
            if (lights[i].lightType == lightPickup.lightType)
            {
                return;
            }
        }
        lights.Add(lightPickup);
        playerInteract.MakeCurrentObjectNull();
        playerInteract.HideInteractText();
        Destroy(pickup);
    }

    int currentIndex = 0;

    public void ScrollThroughDifferentLights(float scrollValue)
    {
        int numberOfLights = lights.Count;
        if (scrollValue > 0)
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
        if(lights.Count > 1) // this is a bit of a hack - should really check that the light has power / fuel
        {
            AudioManager.Instance.SwapLight(equippedLight.lightType);
        }
    }

    private void EquipLight(int lightIndex)
    {
        equippedLight.parentObject.SetActive(false); 
        equippedLight = lights[lightIndex];
        equippedLight.parentObject.SetActive(true); 
    }

    public void ThrowLight() // this is some really horrible code - lol! 
    {
        if(numberOfThrowableLights == 0) { return; }
        numberOfThrowableLights--;

        GameObject stickyLight = Instantiate(equippedLight.ThrowableLightPrefab, instantiationPoint.position, headTransform.rotation);
        stickyLight.GetComponent<StickyLight>().Init();
        UIManager.Instance.updateThrowableLightUI(numberOfThrowableLights);
    }

    public void PickupThrowableLight()
    {
        numberOfThrowableLights++;
        UIManager.Instance.updateThrowableLightUI(numberOfThrowableLights);
    }
}
