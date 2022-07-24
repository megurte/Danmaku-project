using System;
using Character;
using UnityEngine;

namespace Drop
{
    public class ExpDrop: MonoBehaviour
    {
        public DropSO dropSo;

        private int Value => dropSo.value;
        private DropType DropType => dropSo.dropType;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerModel.GetDrop(DropType, Value);
                Destroy(gameObject);
            }
            
            if (other.CompareTag("Border"))
                Destroy(gameObject);
        }
    }
}