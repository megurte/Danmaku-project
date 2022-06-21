using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public DropType dropType;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // DO SOMETHING
            Destroy(gameObject);
        }
            
        if (other.CompareTag("Border"))
            Destroy(gameObject);
    }
}

[Serializable]
public enum DropType
{
    ExpDrop,
    PointDrop,
    HealthDrop,
    SpecialDrop
}