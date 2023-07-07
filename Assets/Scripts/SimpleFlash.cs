using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    public Color originalColor;
    
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
        // Color for now; Since we don't have our sprites yet.
        spriteRenderer.color = originalColor;
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
        // spriteRenderer.material = SingletonManager.Get<UIManager>().flashMaterial;
        spriteRenderer.color = SingletonManager.Get<UIManager>().flashColor;
        yield return new WaitForSeconds(SingletonManager.Get<UIManager>().flashDuration);
        // spriteRenderer.material = originalMat;
        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }
}
