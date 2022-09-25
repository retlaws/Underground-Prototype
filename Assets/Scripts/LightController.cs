using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] Light equippedLight;
    [SerializeField] float maxBrightness = 30f; 
    [SerializeField] float amountToReducePerSecond = 1f; 
    
    private void Update()
    {
        equippedLight.intensity -= amountToReducePerSecond * Time.deltaTime;
        equippedLight.spotAngle -= (amountToReducePerSecond * Time.deltaTime) / 2;
    }

    public void PowerUpLight(float powerUpAmount)
    {
        if(equippedLight.intensity + powerUpAmount > maxBrightness)
        {
            equippedLight.intensity = maxBrightness;
        }
        else
        {
            equippedLight.intensity += powerUpAmount;
        }

        if(equippedLight.spotAngle + powerUpAmount > 75)
        {
            equippedLight.spotAngle = 75;
        }
        else
        {
            equippedLight.spotAngle += powerUpAmount;
        }
    }
}
