﻿using System;
using Character;
using UnityEngine;

namespace Environment
{
    public class SceneEnvironment: MonoBehaviour
    {
        public GameObject environmentPrefab;
        public Vector3 direction;
        public float speed;

        private void Start()
        {
            PlayerBase.OnDeath.AddListener((int a) => speed = 0);
        }

        private void FixedUpdate()
        {
            Move();
        }
        
        private void Move()
        {
            environmentPrefab.transform.Translate(direction.normalized * speed, Space.World);
        }
    }
}