using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject interactText;

    Iinteractable currentObject = null;

    private void Start()
    {
        interactText.SetActive(false);
    }

    public void PlayerInteracted()
    {
        if (currentObject != null)
        {
            currentObject.interact(this);
        }
    }

    public void MakeCurrentObjectNull()
    {
        currentObject = null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Iinteractable>() != null)
        {
            currentObject = other.transform.GetComponent<Iinteractable>();
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        currentObject = null;
        interactText.SetActive(false);
    }
}
