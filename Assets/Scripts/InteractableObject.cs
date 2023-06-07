using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public InteractEvent onInteractEvent = new();

    private bool canInteract;
    protected Sprite hintSprite;
    protected TextMeshPro hintText;

    protected void OnEnable()
    {
        canInteract = true;
        onInteractEvent.AddListener(OnInteract);
    }

    protected void OnDisable()
    {
        onInteractEvent.RemoveListener(OnInteract);
    }

    protected virtual void OnInteract(GameObject go)
    {
        
    }
}
