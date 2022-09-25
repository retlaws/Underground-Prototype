using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class Tool : MonoBehaviour, Iinteractable
{
    [SerializeField] DiggingTool drill;
    [SerializeField] DiggingTool pickAxe;
    bool currentToolDrill = false; 


    public void interact() //TODO this is very hacky, just making it work
    {
        if(currentToolDrill)
        {
            currentToolDrill = false;
            FindObjectOfType<PlayerDig>().SetTool(pickAxe);  
            GetComponent<Renderer>().material.color = Color.red;
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        }
        else
        {
            currentToolDrill = true;
            FindObjectOfType<PlayerDig>().SetTool(drill);  
            GetComponent<Renderer>().material.color = Color.green;
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        }


        //Destroy(gameObject);
    }
}
