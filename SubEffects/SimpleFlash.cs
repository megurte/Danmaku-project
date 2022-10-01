using System.Collections;
using UnityEngine;

namespace SubEffects
{
    public class SimpleFlash : MonoBehaviour
    {
        [SerializeField] private Material flashMaterial;

        [SerializeField] private float flashDuration;
    
        [SerializeField] private float duration;
    
        private SpriteRenderer _spriteRenderer;

        private Material _originalMaterial;

        private Coroutine _flashRoutine;
    
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalMaterial = _spriteRenderer.material;
        }
    
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
    }
}
