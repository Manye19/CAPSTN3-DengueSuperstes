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

    private void Start()
    {
        canInteract = true;
        onInteractEvent.AddListener(OnInteract);
    }

    private void OnDisable()
    {
        onInteractEvent.RemoveListener(OnInteract);
    }

    protected virtual void OnInteract(GameObject go)
    {
        
    }
}
