using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    public Light lightItem;
    public GameObject parentObject;
    public float maxBrightness = 20f;
    public bool isRechargable = false;
    public bool isThrowable = false;
    public GameObject ThrowableLightPrefab; 

    [Range(0f, 5f)]
    public float amountToReducePerSecond = 1f;

    private void Start()
    {
        lightItem = GetComponent<Light>();
    }
}
