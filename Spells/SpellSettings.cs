using System;
using UnityEngine;

namespace Spells
{
    [Serializable]
    public struct CommonSpellSettings
    {
        public GameObject Bullet;
        public Vector3 CenterPosition;
        public float Distance;

        public CommonSpellSettings(GameObject bullet, Vector3 centerPosition, float distance)
        {
            Bullet = bullet;
            CenterPosition = centerPosition;
            Distance = distance;
        }
    }
    
    [Serializable]
    public struct SpellRandomShootingSettings
    {
        public GameObject Bullet;
        public Vector3 CenterPosition;
        public float Distance;
        public float Delay;

        public SpellRandomShootingSettings(GameObject bullet,  Vector3 centerPosition, float distance, float delay)
        {
            Delay = delay;
            Distance = distance;
            CenterPosition = centerPosition;
            Bullet = bullet;
        }
    }
    
    [Serializable]
    public struct SpellPropellerBulletShootSettings
    {
        public GameObject Bullet;
        public Vector3 CenterPosition;
        public float Distance;
        public float StartAngle;
        public float StepAngle;
        public float Delay;
        public float Duration;
        public bool IsReverse;

        public SpellPropellerBulletShootSettings(GameObject bullet, Vector3 centerPosition, float distance, float startAngle,
            float stepAngle,float delay, float duration, bool isReverse)
        {
            Duration = duration;
            Delay = delay;
            StepAngle = stepAngle;
            StartAngle = startAngle;
            Distance = distance;
            CenterPosition = centerPosition;
            Bullet = bullet;
            IsReverse = isReverse;
        }
    }
    
    [Serializable]
    public struct SpellSettingsWithCount
    {
        public GameObject Bullet;
        public Vector3 CenterPosition;
        public float Distance;
        public int Count;

        public SpellSettingsWithCount(GameObject bullet, Vector3 centerPosition, float distance, int count)
        {
            Bullet = bullet;
            CenterPosition = centerPosition;
            Distance = distance;
            Count = count;
        }
    }
    
    [Serializable]
    public struct SpellReverseBulletShootSettings
    {
        public GameObject Bullet;
        public Vector3 CenterPosition;
        public float Distance;
        public int Count;
        public bool RightDirection;
        public float Angle;
        public float Delay;
    }
   
    [Serializable]
    public struct CommonSpellSettingsWithTarget
    {
        public GameObject Bullet;
        public Vector3 CenterPosition;
        public float Distance;
        public Vector3 TargetDirection;
        public float Delay;

        public CommonSpellSettingsWithTarget(GameObject bullet, Vector3 centerPosition, float distance, 
            Vector3 targetDirection, float delay)
        {
            Bullet = bullet;
            CenterPosition = centerPosition;
            Distance = distance;
            TargetDirection = targetDirection;
            Delay = delay;
        }
    }
}