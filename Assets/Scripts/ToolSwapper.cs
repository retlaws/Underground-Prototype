using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.ReloadAttribute;

public class ToolSwapper : MonoBehaviour
{
    [SerializeField] GameObject Drill;
    [SerializeField] GameObject Pickaxe;

    PlayerDig playerDig;

    private void Start()
    {
        playerDig = GetComponent<PlayerDig>();
    }

    public void SwapTool(string toolName) // using a string name, god that is hacky!!! :-) 
    {
        if(toolName == "Drill")
        {
            Drill.SetActive(true);
            playerDig.SetTool(Drill.GetComponent<Tool>());
            Pickaxe.SetActive(false);
        }
        else if(toolName == "Pickaxe")
        {
            Drill.SetActive(false);
            playerDig.SetTool(Pickaxe.GetComponent<Tool>());
            Pickaxe.SetActive(true);
        }
    }
}
