using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPowerUp : MonoBehaviour, Iinteractable
{
    [SerializeField] float powerProvided = 15f; 

    public void interact()
    {
        FindObjectOfType<LightController>().PowerUpLight(powerProvided); 
    }

}
