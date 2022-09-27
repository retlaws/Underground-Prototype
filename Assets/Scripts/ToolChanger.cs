using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class ToolChanger : MonoBehaviour, Iinteractable
{
    [SerializeField] Tool drill;
    [SerializeField] Tool pickAxe;
    bool currentToolDrill = false;

    ToolSwapper playerToolSwapper; 

    private void Start()
    {
        playerToolSwapper = FindObjectOfType<ToolSwapper>();
    }


    public void interact() //TODO this is very hacky, just making it work
    {
        if(currentToolDrill)
        {
            currentToolDrill = false;
            FindObjectOfType<PlayerDig>().SetTool(pickAxe);  
            GetComponent<Renderer>().material.color = Color.red;
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
            playerToolSwapper.SwapTool("Pickaxe");
        }
        else
        {
            currentToolDrill = true;
            FindObjectOfType<PlayerDig>().SetTool(drill);  
            GetComponent<Renderer>().material.color = Color.green;
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            playerToolSwapper.SwapTool("Drill");
        }


        //Destroy(gameObject);
    }
}
