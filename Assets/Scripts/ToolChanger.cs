using UnityEngine;

public class ToolChanger : MonoBehaviour, Iinteractable
{
    [SerializeField] Tool drill;
    [SerializeField] Tool pickAxe;


    ToolSwapper playerToolSwapper; 

    private void Start()
    {
        playerToolSwapper = FindObjectOfType<ToolSwapper>();
    }

    public void interact(PlayerInteract player) //TODO this is very hacky, just making it work
    {
        
    }
}
