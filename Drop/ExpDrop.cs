using System;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace DefaultNamespace.Drop
{
    public class ExpDrop: MonoBehaviour, IDrop
    {
        private int _value;
        private DropType _dropType;

        public DropSO dropSo;

        private void Start()
        {
            SetParams();
        }

        public void SetParams()
        {
            _value = dropSo.value;
            _dropType = dropSo.dropType;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                CharacterController.GetDrop(_dropType, _value);
                Destroy(gameObject);
            }
            
            if (other.CompareTag("Border"))
                Destroy(gameObject);
        }
    }
}