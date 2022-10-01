using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, Iinteractable
{
    [SerializeField] Transform topOfLadder; 
    [SerializeField] Transform bottomOfLadder;
    
    Fader fader;   
    PlayerController playerController;
    

    private void Start()
    {
        fader = FindObjectOfType<Fader>();  
        playerController = FindObjectOfType<PlayerController>();
    }

    public void interact(PlayerInteract player)
    {
        StartCoroutine(UseLadder(player));
    }

    IEnumerator UseLadder(PlayerInteract player)
    {
        playerController.LockControls();
        yield return StartCoroutine(fader.FadeDown());
        TeleportPlayer(player);
        yield return StartCoroutine(fader.FadeUp());
        playerController.UnlockControls();
    }

    bool playerAtTopOfLadder = true;
    Vector3 TeleportationLocation; 

    private void TeleportPlayer(PlayerInteract player)
    {
        if(playerAtTopOfLadder)
        {
            playerAtTopOfLadder = false;
            TeleportationLocation = bottomOfLadder.position;
        }
        else
        {
            playerAtTopOfLadder = true;
            TeleportationLocation = topOfLadder.position;
        }

        player.GetComponent<Transform>().localPosition = TeleportationLocation;
    }
}
