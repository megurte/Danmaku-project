using Bullets;
using UnityEngine.Events;

public static class GlobalEvents
{
    public static readonly UnityEvent<int> OnPhaseChange = new UnityEvent<int>();
        
    public static readonly UnityEvent<int> OnSpecialChange = new UnityEvent<int>();
        
    public static readonly UnityEvent<int> OnHealthChange = new UnityEvent<int>();
    
    public static readonly UnityEvent OnClearBullets = new UnityEvent();

    public static readonly UnityEvent OnBossFightFinish = new UnityEvent();
    
    private static int _phaseNumber = 1;

    public static void ChangePhase()
    {
        ++_phaseNumber;
        OnPhaseChange.Invoke(_phaseNumber);
    }

    public static void SpecialChanged(int value)
    {
        OnSpecialChange.Invoke(value);
    }
        
    public static void HealthChanged(int value)
    {
        OnHealthChange.Invoke(value);
    }
        
    public static void OnBossFightFinished()
    {
        OnBossFightFinish.Invoke();
    }
}