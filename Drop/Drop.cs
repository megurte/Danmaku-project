using System;
using Character;
using Environment;
using UnityEngine;
using Utils;

namespace Drop
{
    public class Drop: MonoBehaviour
    {
        public DropSO dropSo;

        private int Value => dropSo.value;
        private DropType DropType => dropSo.dropType;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.IfHasComponent<PlayerBase>(component =>
            {
                PlayerBase.GetDrop(DropType, Value);
                Destroy(gameObject);
            });
            
            other.gameObject.IfHasComponent<Border>(component => Destroy(gameObject));
        }
    }
}