using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
    #region Datamembers

    #region Editor Settings

    [SerializeField] private Material flashMaterial;

    [SerializeField] private float flashDuration;
    
    [SerializeField] private float duration;

    #endregion

    #region Private Fields

    private SpriteRenderer _spriteRenderer;

    private Material _originalMaterial;

    private Coroutine _flashRoutine;
    
    #endregion

    #endregion


    #region Methods

    #region Unity Callbacks

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
    }

    #endregion

    public void FlashEffect()
    {
        var interval = flashDuration * 2;
        
        for (float i = 0; i <= duration; i += interval)
        {
            StartCoroutine(FlashRepeat(i));
        }
    }

    private void Flash()
    {
        if (_flashRoutine != null)
        {
            StopCoroutine(_flashRoutine);
        }

        _flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.material = _originalMaterial;
        _flashRoutine = null;
    }

    private IEnumerator FlashRepeat(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Flash();
    }

    #endregion
}
