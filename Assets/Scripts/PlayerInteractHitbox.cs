using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractHitbox : MonoBehaviour
{
    public BoxCollider Collider;

    public Interactable CurrentInteractable;

    private int OverlapsCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        Interactable interactComponent = other.GetComponent<Interactable>();

        if(interactComponent != null)
        {
            OverlapsCount++;
            CurrentInteractable = interactComponent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Interactable interactComponent = other.GetComponent<Interactable>();

        if (interactComponent != null)
        {
            OverlapsCount--;

            if (OverlapsCount == 0)
            {
                CurrentInteractable = null;
            }
        }
    }

    public void OnPlayerInteract()
    {
        if(CurrentInteractable != null)
        {
            CurrentInteractable.OnPlayerInteract();
        }
    }
}
