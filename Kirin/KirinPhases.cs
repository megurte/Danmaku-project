using DefaultNamespace;
using UnityEngine;

namespace Kirin 
{
    public static class KirinPhases
    {
        public static void InitPhaseOne(KirinSpells kirinSpells)
        {
            kirinSpells.StartCoroutine(kirinSpells.RouletteFireball(2, true, kirinSpells.fireBullet, 70,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.RouletteFireball(3, true, kirinSpells.fireBullet, 80,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.RouletteFireball(3.5f, true, kirinSpells.fireBullet, 70,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.RouletteFireball(4.5f, true, kirinSpells.fireBullet, 80,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(5, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(5.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(6, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(6.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(7, false, kirinSpells.fireballSmall, 70));
            kirinSpells.StartCoroutine(kirinSpells.SpiralFireball(7.3f, true, kirinSpells.fireBullet, 100, 0.5f));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(8, true, kirinSpells.fireBullet, 26));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(8.5f, true, kirinSpells.fireball, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(9, false, kirinSpells.fireball, 60));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(9.5f, true, kirinSpells.fireballSmall, 32));
            kirinSpells.StartCoroutine(kirinSpells.CircleFireball(10, false, kirinSpells.fireball, 70));
        }
        
        public static void InitPhaseTwo(KirinSpells kirinSpells)
        {
            Debug.Log("InitPhaseTwo");
        }
        
        public static void InitPhaseThree(KirinSpells kirinSpells)
        {
            Debug.Log("InitPhaseThree");
        }
        
        public static void InitPhaseFour(KirinSpells kirinSpells)
        {
            Debug.Log("InitPhaseFour");
        }
    }
}