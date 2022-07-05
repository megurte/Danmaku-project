using System;
using System.Collections.Generic;
using Kirin;
using UnityEngine;
using UnityEngine.Events;

public class TestScript1 : MonoBehaviour
{
    [Serializable]
    public class MethodEvent : UnityEvent {}
    
    [SerializeField]
    private MethodEvent methodList = new MethodEvent();
    
    [SerializeField]
    private UnityEvent<KirinSpellSettings> list = new UnityEvent<KirinSpellSettings>();
    
    [SerializeField]
    public MyIntEvent m_MyEvent;

    [SerializeField] public List<KirinSpellSettings> methodParams;
    [SerializeField] public List<KirinSpellSettingsWithDelay> methodParamsWithDelays;

    public void Start0() { }
    public void Start1() { }
    public void Start2() { }
    public void Start3() { }

    private void Start()
    {
        // InitSpells();
    }

    public void InitSpells()
    {
        methodList.Invoke();
    }
}
[Serializable]
public class Settings
{
    public float ssf;
    public float fsaf;
}

[Serializable]
public class MyIntEvent : UnityEvent<KirinSpellSettings>
{
}