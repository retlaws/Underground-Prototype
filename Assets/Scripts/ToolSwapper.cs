using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.Rendering.ReloadAttribute;

public class ToolSwapper : MonoBehaviour
{
    [SerializeField] List<GameObject> toolList;

    GameObject equippedTool; 

    PlayerDig playerDig;

    private void Start()
    {
        playerDig = GetComponent<PlayerDig>();
        UnEquipAllTools();
        equippedTool = toolList[0]; 
        EquipTool();
    }

    private void UnEquipAllTools()
    {
        for (int i = 0; i < toolList.Count; i++)
        {
            toolList[i].SetActive(false);
        }
    }

    int currentIndex = 0;

    public void SwapTool() // using a string name, god that is hacky!!! :-) 
    {
        currentIndex++;
        if(currentIndex >= toolList.Count)
        {
            currentIndex = 0;
        }
        EquipTool();
    }

    private void EquipTool()
    {
        equippedTool.SetActive(false);  
        equippedTool = toolList[currentIndex];
        equippedTool.SetActive(true);
        playerDig.SetTool(equippedTool.GetComponent<Tool>());
    }


}
