using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionArea : MonoBehaviour
{
    private bool isInInteractableArea = false;

    void OnTriggerEnter(Collider other)
    {
        //UnityEngine.Debug.Log("Wow");

         isInInteractableArea = true;
        
    }

    void OnTriggerExit(Collider other)
    {
        isInInteractableArea = false;
     
    }

    public bool IsInInteractableArea()
    {
        return isInInteractableArea;
    }

}

