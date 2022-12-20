using Drop;
using Interfaces;
using UnityEngine;
using Utils;
using Random = System.Random;


namespace Factories
{
    public class DropFactory : MonoBehaviour, IFactory<DropBase, Vector3, int>
    {
        public DropBase Create(DropBase prefab, Vector3 startPosition, int seed)
        {
            var rnd = new Random(seed);
            var randomXOffset = rnd.NextFloat(-1, 1);
            var randomYOffset = rnd.NextFloat(-1, 1);
            var dropPosition = new Vector3(startPosition.x + randomXOffset, startPosition.y + randomYOffset, 0);

            return Instantiate(prefab, dropPosition, Quaternion.identity);
        }
    }
}