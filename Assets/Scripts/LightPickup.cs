using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPickup : MonoBehaviour, Iinteractable
{
    [SerializeField] LightObject lightObject;



    public void interact(PlayerInteract player)
    {
        player.GetComponent<LightController>().PickupLightItem(lightObject, gameObject);
    }
}
