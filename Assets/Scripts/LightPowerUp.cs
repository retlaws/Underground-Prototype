using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPowerUp : MonoBehaviour, Iinteractable
{
    [SerializeField] float powerProvided = 15f;
    [SerializeField] LightType typeOfLightPowered; 

    public void interact(PlayerInteract player)
    {
        LightController lightController = player.GetComponent<LightController>();

        if (lightController.equippedLight.lightType == typeOfLightPowered)
        {
            lightController.PowerUpLight(powerProvided);
        }
    }

}
