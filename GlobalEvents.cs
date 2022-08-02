using UnityEngine.Events;

namespace DefaultNamespace
{
    public class GlobalEvents
    {
        public static UnityEvent<int> OnPhaseChange = new UnityEvent<int>();
        
        public static UnityEvent<int> OnSpecialChange = new UnityEvent<int>();
        
        public static UnityEvent<int> OnHealthChange = new UnityEvent<int>();
        


        public static int phaseNumber = 1;

        public static void ChangePhase()
        {
            ++phaseNumber;
            OnPhaseChange.Invoke(phaseNumber);
        }

        public static void SpecialChanged(int value)
        {
            OnSpecialChange.Invoke(value);
        }
        
        public static void HealthChanged(int value)
        {
            OnHealthChange.Invoke(value);
        }
    }
}