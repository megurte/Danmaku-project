using System;
using System.Collections.Generic;
using Kirin;
using UnityEngine;

namespace Kirin
{
    [CreateAssetMenu(fileName = "new KirinConfig", menuName = "Kirin")]
    public class KirinSO: ScriptableObject
    {
        public float maxHp;
        public float lerpSpeed;
        public List<SubListSpell> phaseSpellSettings;
        public List<SubListMove> phaseMovementPositions;
    }
}

[Serializable]
public class SubListMove
{
    public Phases name;
    public List<KirinMoveSettings> list = new List<KirinMoveSettings>();
}

[Serializable]
public class SubListSpell
{
    public Phases name;
    public List<KirinSpellSettingsWithDelay> list = new List<KirinSpellSettingsWithDelay>();
}

public enum Phases: int
{
    PhaseOne = 1,
    PhaseTwo = 2,
    PhaseThree = 3,
    PhaseFour = 4
}