using System;
using UnityEngine;

namespace Utils
{
    public static class GameObjectExtensions
    {
        public static void IfHasComponent<T>(this GameObject gameObject, Action<T> callback = null)
        {
            var component = gameObject.GetComponent<T>();

            if (component != null)
            {
                callback?.Invoke(component);
            }
        }
    }
}