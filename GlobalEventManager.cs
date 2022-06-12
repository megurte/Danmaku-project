using System;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

namespace DefaultNamespace
{
    public class GlobalEventManager
    {
        public static UnityEvent<int> OnPhaseChange = new UnityEvent<int>();

        public static void ChangePhase(int phase)
        {
            OnPhaseChange.Invoke(phase);
        }
    }
}