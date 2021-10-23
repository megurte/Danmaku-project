using System;
using UnityEngine;
using UnityEngine.UI;

namespace Kirin
{
    public class KirinStats : MonoBehaviour
    {
        [SerializeField]
        public float CurrentHp { get; set; }
        public float MaxHp { get; set; }
        public float lerpspeed = 2f;
        public Image bar;
        private float _phase = 1;

        private void Start()
        {
            MaxHp = 1000;
            bar.fillAmount = MaxHp;
            CurrentHp = MaxHp;
        }

        private void Update()
        {
            HandleBar();
            if (CurrentHp <= 0)
            {
                Destroy(gameObject);
            }
        }


        private  void HandleBar()
        {
            if (CurrentHp / MaxHp != bar.fillAmount)
                bar.fillAmount = Mathf.Lerp(bar.fillAmount, CurrentHp / MaxHp, Time.deltaTime * lerpspeed);
        }
    }
}