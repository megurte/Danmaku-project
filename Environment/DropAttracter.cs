﻿using Character;
using UnityEngine;
using Utils;

namespace Environment
{
    public class DropAttracter: MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            other.gameObject.HasComponent<PlayerBase>(component =>
            {
                UtilsBase.CollectDrop();
            });
        }
    }
}