using System;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    public LightType lightType; 
    public Light lightItem;
    public GameObject parentObject;
    public float maxBrightness = 20f;
    public bool isRechargable = false;
    public bool isThrowable = false;
    public GameObject ThrowableLightPrefab;
    


    float flickerLightMaxIntensity;
    [SerializeField] bool flickeringLight = false; 

    [Range(0f, 5f)]
    public float amountToReducePerSecond = 1f;

    private void Awake()
    {
        lightItem = GetComponent<Light>();
        flickerLightMaxIntensity = maxBrightness;
    }

    private void Update()
    {
        flickerLightMaxIntensity -= amountToReducePerSecond * Time.deltaTime;
        lightItem.intensity -= amountToReducePerSecond * Time.deltaTime;
        lightItem.spotAngle -= (amountToReducePerSecond * Time.deltaTime) / 2;
        if(flickeringLight == false)
        {
            UIManager.Instance.UpdateLightMeter(lightItem.intensity, maxBrightness);
        }
        else
        {
            UIManager.Instance.UpdateLightMeter(flickerLightMaxIntensity, maxBrightness);
        }

    }

    public float GetMaxIntensityForLightFlicker()
    {
        return flickerLightMaxIntensity;
    }

    public void PowerUpLight(float powerUpAmount)
    {
        if (lightItem.intensity + powerUpAmount > maxBrightness)
        {
            lightItem.intensity = maxBrightness;
            flickerLightMaxIntensity = maxBrightness;
        }
        else
        {
            lightItem.intensity += powerUpAmount;
            flickerLightMaxIntensity += powerUpAmount;
        }

        if (lightItem.spotAngle + powerUpAmount > 75)
        {
            lightItem.spotAngle = 75;
        }
        else
        {
            lightItem.spotAngle += powerUpAmount;
        }
    }
}
