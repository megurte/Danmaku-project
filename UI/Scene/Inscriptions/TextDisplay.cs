using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Scene.Inscriptions
{
    public class TextDisplay: MonoBehaviour
    {
        [SerializeField] private float fadeInTime;
        [SerializeField] private float fadeOutTime;

        private Animator _animator;
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            StartCoroutine(InscriptionFadeIn());
        }

        private IEnumerator InscriptionFadeIn()
        {
            yield return new WaitForSeconds(fadeInTime);
            
            _animator.SetTrigger(FadeIn);
            
            yield return InscriptionFadeOut();
        }
        
        private IEnumerator InscriptionFadeOut()
        {
            yield return new WaitForSeconds(fadeOutTime);
            
            _animator.SetTrigger(FadeOut);
        }
    }
}