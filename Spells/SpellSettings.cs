using UnityEngine;

namespace Spells
{
        public readonly struct CommonSpellSettings
    {
        public readonly GameObject Bullet;
        public readonly Vector3 CenterPosition;
        public readonly float Distance;

        public CommonSpellSettings(GameObject bullet, Vector3 centerPosition, float distance)
        {
            Bullet = bullet;
            CenterPosition = centerPosition;
            Distance = distance;
        }
    }
    
    public readonly struct CommonSpellSettingsWithDelay
    {
        public readonly GameObject Bullet;
        public readonly Vector3 CenterPosition;
        public readonly float Distance;
        public readonly float Delay;

        public CommonSpellSettingsWithDelay( GameObject bullet,  Vector3 centerPosition, float distance, float delay)
        {
            Delay = delay;
            Distance = distance;
            CenterPosition = centerPosition;
            Bullet = bullet;
        }
    }
    
    public readonly struct PropellerSpellSettings
    {
        public readonly GameObject Bullet;
        public readonly Vector3 CenterPosition;
        public readonly float Distance;
        public readonly float StartAngle;
        public readonly float StepAngle;
        public readonly float Delay;
        public readonly float Duration;
        public readonly bool IsReverse;

        public PropellerSpellSettings(GameObject bullet, Vector3 centerPosition, float distance, float startAngle,
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
    
    public readonly struct SpellSettingsWithCount
    {
        public readonly GameObject Bullet;
        public readonly Vector3 CenterPosition;
        public readonly float Distance;
        public readonly int Count;

        public SpellSettingsWithCount(GameObject bullet, Vector3 centerPosition, float distance, int count)
        {
            Bullet = bullet;
            CenterPosition = centerPosition;
            Distance = distance;
            Count = count;
        }
    }
    
    public readonly struct SpellSettingsWithDirectionAndAngle
    {
        public readonly GameObject Bullet;
        public readonly Vector3 CenterPosition;
        public readonly float Distance;
        public readonly int Count;
        public readonly bool RightDirection;
        public readonly float Angle;
        public readonly float Delay;

        public SpellSettingsWithDirectionAndAngle(GameObject bullet, Vector3 centerPosition, float distance, 
            int count, bool rightDirection, float angle, float delay)
        {
            Bullet = bullet;
            CenterPosition = centerPosition;
            Distance = distance;
            Count = count;
            RightDirection = rightDirection;
            Angle = angle;
            Delay = delay;
        }
    }
    
    public readonly struct CommonSpellSettingsWithTarget
    {
        public readonly GameObject Bullet;
        public readonly Vector3 CenterPosition;
        public readonly float Distance;
        public readonly Vector3 TargetDirection;
        public readonly float Delay;

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