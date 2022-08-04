using System;
using System.Collections;
using System.Collections.Generic;
using Bullets;
using Spells;
using UnityEngine;
using Random = System.Random;

namespace Boss.Camilla
{
    public class CamillaActions: MonoBehaviour
    {
        private ChainSpawner[] _chainSpawners;
        
        private void Start()
        {
            _chainSpawners = FindObjectsOfType<ChainSpawner>();

            CamillaPhases.CircleBulletWithRandomColorsSpawn.AddListener(CircleBulletWithRandomColorsSpawn);
            CamillaPhases.AllRandomSpawnersActivate.AddListener(AllRandomSpawnersActivate);
            CamillaPhases.RandomSpawnersActivate.AddListener(RandomSpawnersActivate);
            CamillaPhases.WaveChainsSpawn.AddListener(WaveChainSpawn);
            CamillaPhases.SpiralBulletSpawn.AddListener(SpiralBulletSpawn);
            CamillaPhases.ReverseBulletSpawn.AddListener(ReverseBulletSpawn);
            CamillaPhases.ForTest.AddListener(Test);
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

        private void AllRandomSpawnersActivate(int startIndex, int endIndex)
        {
            for (var spawnerIndex = startIndex; spawnerIndex <= endIndex; spawnerIndex++)
            {
                var rnd = new Random(Guid.NewGuid().GetHashCode());
                var randomDelay = rnd.NextFloat(0, 10);
            
                foreach (var spawner in _chainSpawners)
                {
                    StartCoroutine(spawner.Spawn(spawnerIndex, randomDelay));
                }
            }
        }
        
        private void CircleBulletWithRandomColorsSpawn(SpellSettingsWithCount  settings)
        {
            const float angle = 360 * Mathf.Deg2Rad;
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            for (var i = 1; i <= settings.Count; i++)
            {
                var degree = angle / settings.Count * i;
                position.y = settings.CenterPos.y + Mathf.Cos(degree) * settings.Distance;
                position.x = settings.CenterPos.x + Mathf.Sin(degree) * settings.Distance;

                direction.y = Mathf.Cos(degree);
                direction.x = Mathf.Sin(degree);

                var instObject = Instantiate(settings.Bullet, position, Quaternion.identity);
                //var rnd = new Random(Guid.NewGuid().GetHashCode());
                //instObject.GetComponent<Bullet>().SetColor(rnd.GetRandomColor());
                instObject.GetComponent<Bullet>().direction = direction;
            }
        }

        private void SpiralBulletSpawn(SpellSettingsWithDirectionAndAngle settings)
        {
            StartCoroutine(SpiralBulletSpawnRoutine(settings));
        }
        
        private void ReverseBulletSpawn(SpellSettingsWithDirectionAndAngle settings)
        {
            StartCoroutine(ReverseBulletSpawnRoutine(settings));
        }
        
        private IEnumerator SpiralBulletSpawnRoutine(SpellSettingsWithDirectionAndAngle settings)
        {
            const float angle = 360 * Mathf.Deg2Rad;
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            for (var i = 1; i <= settings.Count; i++)
            {
                var element = settings.RightDirection ? i : settings.Count - i;
                var degree = angle / settings.Count * element;
                position.y = settings.CenterPos.y 
                             + Mathf.Cos(degree + settings.Angle * Mathf.Deg2Rad) * settings.Distance;
                position.x = settings.CenterPos.x 
                             + Mathf.Sin(degree+ settings.Angle * Mathf.Deg2Rad) * settings.Distance;

                direction.y = Mathf.Cos(degree);
                direction.x = Mathf.Sin(degree);

                yield return new WaitForSeconds(0.01f);
                var instObject = Instantiate(settings.Bullet, position, Quaternion.identity);
                instObject.GetComponent<Bullet>().direction = direction;
            }
        }
        
                
        private void Test(SpellSettingsWithDirectionAndAngle settings)
        {
            StartCoroutine(TestRoutine(settings));
        }
        
        private IEnumerator TestRoutine(SpellSettingsWithDirectionAndAngle settings)
        {
            yield return null;
        }
        
        private IEnumerator ReverseBulletSpawnRoutine(SpellSettingsWithDirectionAndAngle settings)
        {
            const float angle = 360 * Mathf.Deg2Rad;
            var direction = new Vector2(-1, 1);
            var position = new Vector3();

            for (var i = 1; i <= settings.Count; i++)
            {
                var element = settings.RightDirection ? i : settings.Count - i;
                var degree = angle / settings.Count * element;
                position.y = settings.CenterPos.y 
                             + Mathf.Cos(degree + settings.Angle * Mathf.Deg2Rad) * settings.Distance;
                position.x = settings.CenterPos.x 
                             + Mathf.Sin(degree+ settings.Angle * Mathf.Deg2Rad) * settings.Distance;

                direction.x = Mathf.Cos(degree);
                direction.y = Mathf.Sin(degree);

                yield return new WaitForSeconds(0.01f);
                var instObject = Instantiate(settings.Bullet, position, Quaternion.identity);
                instObject.GetComponent<Bullet>().direction = direction;
            }
        }
    }
}