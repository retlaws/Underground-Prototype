using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolPickup : MonoBehaviour, Iinteractable
{
    [SerializeField] GameObject tool;

    public void interact(PlayerInteract player)
    {
        player.GetComponent<ToolSwapper>().AddTool(tool, tool.GetComponent<Tool>().toolConfig.toolType, gameObject);
    }

}
