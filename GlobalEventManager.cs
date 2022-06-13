using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;

namespace DefaultNamespace
{
    public class GlobalEventManager
    {
        public static UnityEvent<int> OnPhaseChange = new UnityEvent<int>();
        
        public static int phaseNumber = 1;

        public static void ChangePhase()
        {
            phaseNumber++;
            OnPhaseChange.Invoke(phaseNumber);
        }
    }
}