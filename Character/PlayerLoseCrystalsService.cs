using System;
using Drop;
using UnityEngine;
using Utils;
using Random = System.Random;

namespace Character
{
    public class PlayerLoseCrystalsService : MonoBehaviour
    {
        public void LoseExperienceCrystals(PlayerBase player)
        {
            var maxVisualCrystals = GetRandomNumberOfCrystals();

            for (var i = 0; i < maxVisualCrystals; i++)
            {
                var seed = Guid.NewGuid().GetHashCode();
                CreateLostDrop(seed);
            }
            
            if (player.Experience - player.experienceLoseByDamage < 0)
            {
                var keyMap = player.playerScriptableObject.levelUpMap;
                var remainder = Mathf.Abs(player.Experience - player.experienceLoseByDamage);

                while (remainder > 0)
                {
                    for (var index = keyMap.keys.Count - 1; index >= 0; index--)
                    {
                        if (player.Level < keyMap.keys[index]) continue;

                        if (keyMap.keys[index] == 1)
                        {
                            player.Experience = 0;
                            remainder = 0;
                            break;
                        }

                        remainder = keyMap.values[index] - remainder;

                        if (remainder < 0)
                        {
                            remainder = Mathf.Abs(remainder);
                            player.Level -= 1;
                            break;
                        }
                        
                        if (keyMap.keys[index] > 1)
                            player.Level -= 1;

                        player.Experience = remainder;
                        remainder = 0;
                        break;
                    }
                }
            }
            else
            {
                player.Experience -= player.experienceLoseByDamage;
            }
        }
        
        private void CreateLostDrop(int seed)
        {        
            var prefab = Resources.Load<GameObject>("Prefab/Drops/expDrop");
            var rnd = new Random(seed);
            var startPos = transform.position;
            var randomXOffset = rnd.NextFloat(-1, 1);
            var randomYOffset = rnd.NextFloat(-1, 1);
            var dropPosition = new Vector3(startPos.x + randomXOffset, startPos.y + randomYOffset, 0);
            var newObject = Instantiate(prefab, dropPosition, Quaternion.identity);
                
            newObject.gameObject.HasComponent<Collider2D>(component => component.enabled = false);
            newObject.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            newObject.gameObject.HasComponent<DropBase>(Destroy);
        }
        
        private int GetRandomNumberOfCrystals()
        {
            var seed = Guid.NewGuid().GetHashCode();
            return new Random(seed).Next(6, 12);
        }
    }
}