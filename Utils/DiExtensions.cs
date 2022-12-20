using UnityEngine;
using Zenject;

namespace Utils
{
    public static class DiExtensions
    {
        public static T InstantiatePrefabAs<T>(this DiContainer container, Object prefab, Vector3 position)
        {
            var newObject = container.InstantiatePrefab(prefab, position, Quaternion.identity, null);
            return newObject.GetComponent<T>();
        }
    }
}