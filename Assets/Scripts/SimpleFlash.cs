using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;
    private Material originalMat;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMat = spriteRenderer.material;
        // Color for now; Since we don't have our sprites yet.
        originalColor = spriteRenderer.color;
    }

    public void Flash()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        
        flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        // Uses color for now; Since we don't have our sprites yet.
        // spriteRenderer.material = SingletonManager.Get<UIManager>().flashMaterial;
        spriteRenderer.color = SingletonManager.Get<UIManager>().flashColor;
        yield return new WaitForSeconds(SingletonManager.Get<UIManager>().flashDuration);
        // spriteRenderer.material = originalMat;
        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }
}
