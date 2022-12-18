using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Enemy;
using ObjectPool;
using Spells;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Boss.Camilla
{
    public class CamillaActions: MonoBehaviour
    {
        private ChainSpawner[] _chainSpawners;
        
        private void Start()
        {
            _chainSpawners = FindObjectsOfType<ChainSpawner>();

            CamillaPhases.StopAllSpells.AddListener(StopAllSpells);
            CamillaPhases.RandomShooting.AddListener(RandomShooting);
            CamillaPhases.WaveChainsSpawn.AddListener(WaveChainSpawn);
            CamillaPhases.SpiralBulletSpawn.AddListener(SpiralBulletSpawn);
            CamillaPhases.ReverseBulletSpawn.AddListener(ReverseBulletSpawn);
            CamillaPhases.PropellerBulletSpawn.AddListener(PropellerBulletSpawn);
            CamillaPhases.CreateMagicalBarrier.AddListener(CreateMagicalBarrier);
            CamillaPhases.TargetPositionShooting.AddListener(TargetPositionShooting);
            CamillaPhases.RandomSpawnersActivate.AddListener(RandomSpawnersActivate);
            CamillaPhases.AllRandomSpawnersActivate.AddListener(AllRandomSpawnersActivate);
            CamillaPhases.CircleBulletWithRandomColorsSpawn.AddListener(CircleBulletWithRandomColorsSpawn);
        }

        private void CreateMagicalBarrier()
        {
            var barrier = Resources.Load<GameObject> ("Prefab/Effects/Barrier");
            var newBarrierInstance = Instantiate(barrier, transform.position, Quaternion.identity);
            
            newBarrierInstance.transform.parent = gameObject.transform;
            newBarrierInstance.GetComponent<Barrier>().SwitchIsMagicBarrierActive(true);
        }

        private void WaveChainSpawn(int startIndex, int endIndex, bool fromLeft = true)
        {
            var delay = 0.2f;
            var start = fromLeft ? startIndex : endIndex;
            var end = fromLeft ? endIndex - 1 : startIndex - 1;

            for (var spawnerIndex = start; spawnerIndex < end; spawnerIndex++)
            {
                foreach (var spawner in _chainSpawners)
                {
                    StartCoroutine(spawner.Spawn(spawnerIndex, delay));
                }
                
                delay++;
            }
        }

        private void RandomSpawnersActivate(int startIndex, int endIndex, int amount)
        {
            var spawnerIndexes = new List<int>();
            
            for (var i = 0; i < amount; i++)
            {
                var rnd = new Random(Guid.NewGuid().GetHashCode());
                var inx = rnd.Next(startIndex, endIndex);
                
                spawnerIndexes.Add(inx);
            }

            foreach (var spawnerIndex in spawnerIndexes)
            {
                foreach (var spawner in _chainSpawners)
                {
                    StartCoroutine(spawner.Spawn(spawnerIndex));
                }
            }
        }

        private void AllRandomSpawnersActivate(int startIndex, int endIndex, int times)
        {
            for (var i = 0; i < times; i++)
            {
                var rnd = new Random(Guid.NewGuid().GetHashCode());
                var randomIndex = rnd.NextFloat(startIndex, endIndex);
                
                foreach (var spawner in _chainSpawners)
                {
                    StartCoroutine(spawner.Spawn((int)randomIndex));
                }
            }
        }
        
        private void CircleBulletWithRandomColorsSpawn(SpellSettingsWithCount settings)
        {
            const float angle = 360 * Mathf.Deg2Rad;
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            for (var i = 1; i <= settings.Count; i++)
            {
                var degree = angle / settings.Count * i;
                
                position.y = settings.CenterPosition.y + Mathf.Cos(degree) * settings.Distance;
                position.x = settings.CenterPosition.x + Mathf.Sin(degree) * settings.Distance;

                direction.y = Mathf.Cos(degree);
                direction.x = Mathf.Sin(degree);

                var instObject = ObjectPoolBase.GetBulletFromPool(settings.Bullet.objectTag, position);
                instObject.GetComponent<Bullet>().Direction = direction;
            }
        }
        
        private void RandomShooting(SpellRandomShootingSettings randomShootingSettings)
        {
            StartCoroutine(RandomShootingRoutine(randomShootingSettings));
        }

        private void SpiralBulletSpawn(SpellReverseBulletShootSettings reverseBulletShootSettings)
        {
            StartCoroutine(SpiralBulletSpawnRoutine(reverseBulletShootSettings));
        }
        
        private void ReverseBulletSpawn(SpellReverseBulletShootSettings reverseBulletShootSettings)
        {
            StartCoroutine(ReverseBulletSpawnRoutine(reverseBulletShootSettings));
        }
        
        private void PropellerBulletSpawn(SpellPropellerBulletShootSettings settings)
        {
            StartCoroutine(PropellerBulletSpawnRoutine(settings));
        }
        
        private void StopAllSpells()
        {
            StopAllCoroutines();
        }
        
        private IEnumerator RandomShootingRoutine(SpellRandomShootingSettings randomShootingSettings)
        {
            while (true)
            {
                var seed = Guid.NewGuid().GetHashCode();

                var degree = new Random(seed).Next(0, 360);
                var direction = new Vector2(0, 0);
                var position = new Vector3
                {
                    y = randomShootingSettings.CenterPosition.y + Mathf.Cos(degree) * randomShootingSettings.Distance,
                    x = randomShootingSettings.CenterPosition.x + Mathf.Sin(degree) * randomShootingSettings.Distance
                };

                direction.y = Mathf.Cos(degree);
                direction.x = Mathf.Sin(degree);

                yield return new WaitForSeconds(randomShootingSettings.Delay);
                
                var instObject = ObjectPoolBase.GetBulletFromPool(randomShootingSettings.Bullet.objectTag, position);
                instObject.GetComponent<Bullet>().Direction = direction;
            }
        }
        
        private IEnumerator SpiralBulletSpawnRoutine(SpellReverseBulletShootSettings reverseBulletShootSettings)
        {
            const float angle = 360 * Mathf.Deg2Rad;
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            for (var i = 1; i <= reverseBulletShootSettings.Count; i++)
            {
                var element = reverseBulletShootSettings.RightDirection ? i : reverseBulletShootSettings.Count - i;
                var degree = angle / reverseBulletShootSettings.Count * element;
                position.y = reverseBulletShootSettings.CenterPosition.y 
                             + Mathf.Cos(degree + reverseBulletShootSettings.Angle * Mathf.Deg2Rad) * reverseBulletShootSettings.Distance;
                position.x = reverseBulletShootSettings.CenterPosition.x 
                             + Mathf.Sin(degree+ reverseBulletShootSettings.Angle * Mathf.Deg2Rad) * reverseBulletShootSettings.Distance;

                direction.y = Mathf.Cos(degree);
                direction.x = Mathf.Sin(degree);

                yield return new WaitForSeconds(0.01f);
                
                var instObject = ObjectPoolBase.GetBulletFromPool(reverseBulletShootSettings.Bullet.objectTag, position);
                instObject.GetComponent<Bullet>().Direction = direction;
            }
        }
        
        private void TargetPositionShooting(CommonSpellSettingsWithTarget settings)
        {
            StartCoroutine(TargetPositionShootingRoutine(settings));
        }
        
        private IEnumerator ReverseBulletSpawnRoutine(SpellReverseBulletShootSettings reverseBulletShootSettings)
        {
            const float angle = 360 * Mathf.Deg2Rad;
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            for (var i = 1; i <= reverseBulletShootSettings.Count; i++)
            {
                var element = reverseBulletShootSettings.RightDirection ? i : reverseBulletShootSettings.Count - i;
                var degree = angle / reverseBulletShootSettings.Count * element;
                position.y = reverseBulletShootSettings.CenterPosition.y 
                             + Mathf.Cos(degree + reverseBulletShootSettings.Angle * Mathf.Deg2Rad)
                             * reverseBulletShootSettings.Distance;
                position.x = reverseBulletShootSettings.CenterPosition.x 
                             + Mathf.Sin(degree+ reverseBulletShootSettings.Angle * Mathf.Deg2Rad) 
                             * reverseBulletShootSettings.Distance;

                direction.x = Mathf.Cos(degree);
                direction.y = Mathf.Sin(degree);

                yield return new WaitForSeconds(0.01f);
                
                var instObject = ObjectPoolBase.GetBulletFromPool(reverseBulletShootSettings.Bullet.objectTag, position);
                instObject.GetComponent<Bullet>().Direction = direction;
            }
        }
        
        private IEnumerator PropellerBulletSpawnRoutine(SpellPropellerBulletShootSettings settings)
        {
            var direction = new Vector2();
            var position = new Vector3();
            var duration = settings.Duration;
            var currentAngle = settings.StartAngle;

            while (duration > 0)
            {
                if (currentAngle > 360)
                {
                    currentAngle = settings.IsReverse 
                        ? settings.StartAngle - 3 
                        : settings.StartAngle + 3;
                }
                
                position.y = settings.CenterPosition.y + Mathf.Cos(currentAngle) * settings.Distance;
                position.x = settings.CenterPosition.x + Mathf.Sin(currentAngle) * settings.Distance;

                direction.y = Mathf.Cos(currentAngle);
                direction.x = Mathf.Sin(currentAngle);

                yield return new WaitForSeconds(settings.Delay);
                
                var instObject = ObjectPoolBase.GetBulletFromPool(settings.Bullet.objectTag, position);
                instObject.GetComponent<Bullet>().Direction = direction;

                currentAngle = settings.IsReverse
                    ? currentAngle - settings.StepAngle * Mathf.Deg2Rad 
                    : currentAngle + settings.StepAngle * Mathf.Deg2Rad;
                duration -= Time.deltaTime;
            }
        }

        private IEnumerator TargetPositionShootingRoutine(CommonSpellSettingsWithTarget settings)
        {
            while (true)
            {
                var seed = Guid.NewGuid().GetHashCode();
                var rnd = new Random(seed);
                var startPos = settings.CenterPosition;
                var randomXOffset = rnd.NextFloat(-2, 2);
                var randomYOffset = rnd.NextFloat(-2, 2);
                var newPosition = new Vector3(startPos.x + randomXOffset, startPos.y + randomYOffset, 0);
                var direction = UtilsBase.GetDirection(UtilsBase.GetNewPlayerPosition(), startPos);
                
                var instObject = ObjectPoolBase.GetBulletFromPool(settings.Bullet.objectTag, newPosition);
                instObject.GetComponent<Bullet>().Direction = direction;
                
                yield return new WaitForSeconds(settings.Delay);
            }
        }
    }
}