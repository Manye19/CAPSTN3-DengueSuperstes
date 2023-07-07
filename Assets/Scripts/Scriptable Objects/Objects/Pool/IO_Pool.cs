using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Pool : InteractableObject
{
    public float xpAmount;

    protected override void OnInteract(GameObject go)
    {
        go.SetActive(false);
    }
}
