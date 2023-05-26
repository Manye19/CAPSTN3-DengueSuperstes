using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    private bool canInteract = true;
    private InteractableObject iObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (iObject)
            {
                OnInteractButtonPressed();
            }
        }
    }
    
    public void OnInteractButtonPressed()
    {
        if (canInteract)
        {
            canInteract = false;
            iObject?.onInteractEvent.Invoke();
            //Debug.Log("Button pressed");
            //StartCoroutine(Cooldown());
            canInteract = true;
        }

    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canInteract = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out InteractableObject interactableObject))
        {
            iObject = interactableObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<InteractableObject>())
        {
            iObject = null;
        }
    }
}
