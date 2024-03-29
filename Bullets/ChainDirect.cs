﻿using System.Collections;
using Boss.Camilla;
using Environment;
using Interfaces;
using UnityEngine;
using Utils;

namespace Bullets
{
    public class ChainDirect : ChainBase
    {

        private void Start()
        {
            StartCoroutine(ChainMove());
        }
        
        private void FixedUpdate()
        {
            if (IsMoving)
                Moving();
        }

        private IEnumerator ChainMove()
        {
            yield return StartCoroutine(ChargeAnimation());
            yield return new WaitForSeconds(1.5f);

            Direction = spawnerType == SpawnerType.Down 
                ? new Vector3(0, 1, 0) 
                : new Vector3(0, -1, 0);
            
            IsMoving = true;
        }
    }
}