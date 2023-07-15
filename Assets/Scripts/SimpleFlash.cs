using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;
    private Material originalMat;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void OnEnable()
    {
        originalMat = spriteRenderer.material;
    }

    public void Flash()
    {
        if (flashCoroutine != null)
        {
            StopAllCoroutines();
        }
        if (gameObject.activeInHierarchy)
        {
            flashCoroutine = StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        // Uses color for now; Since we don't have our sprites yet.
        spriteRenderer.material = SingletonManager.Get<UIManager>().flashMaterial;
        yield return new WaitForSeconds(SingletonManager.Get<UIManager>().flashDuration);
        spriteRenderer.material = originalMat;
        flashCoroutine = null;
    }
}
