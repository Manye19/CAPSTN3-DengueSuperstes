using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Pool : InteractableObject
{
    protected override void OnInteract()
    {
        gameObject.SetActive(false);
        // Event call
    }
}
