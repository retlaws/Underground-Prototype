using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSwapper : MonoBehaviour
{
    [SerializeField] List<GameObject> toolList;

    GameObject equippedTool; 

    PlayerDig playerDig;
    PlayerInteract playerInteract;

    private void Start()
    {
        playerDig = GetComponent<PlayerDig>();
        playerInteract = GetComponent<PlayerInteract>();
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

    public void AddTool(GameObject gameObject, ToolType toolType, GameObject toolPickup)
    {
        for (int i = 0; i < toolList.Count; i++)
        {
            if (toolList[i].GetComponent<Tool>().toolConfig.toolType == toolType)
            {
                return; 
            }
        }
        toolList.Add(gameObject);
        playerInteract.MakeCurrentObjectNull();
        playerInteract.HideInteractText();
        Destroy(toolPickup);
    }

    public void SwapTool() 
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
