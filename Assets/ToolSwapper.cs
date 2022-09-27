using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSwapper : MonoBehaviour
{
    [SerializeField] GameObject Drill;
    [SerializeField] GameObject Pickaxe;

    public void SwapTool(string toolName) // using a string name, god that is hacky!!! :-) 
    {
        if(toolName == "Drill")
        {
            Drill.SetActive(true);
            Pickaxe.SetActive(false);
        }
        else if(toolName == "Pickaxe")
        {
            Drill.SetActive(false);
            Pickaxe.SetActive(true);
        }
    }
}
