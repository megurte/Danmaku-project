using System.Collections;
using System.Collections.Generic;
using Bullets;
using DefaultNamespace;
using UnityEngine;

namespace Kirin 
{
    public class KirinPhases : MonoBehaviour
    {
        public void InitPhaseOne(KirinSpellsAPI kirinSpells, KirinMove kirinPositions)
        {
            GlobalEventManager.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("InitPhaseOne");
            // SPELLS 
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(2, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(3, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.RouletteSpellCast(3.5f, true, kirinSpells.fireBullet, 70,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.RouletteSpellCast(4.5f, true, kirinSpells.fireBullet, 80,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(5, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(5.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(6, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(6.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(7, false, kirinSpells.fireballSmall, 70));
            kirinSpells.StartCoroutine(kirinSpells.SpiralSpellCast(7.3f, true, kirinSpells.fireBullet, 100, 0.5f));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(8, true, kirinSpells.fireBullet, 26));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(8.5f, true, kirinSpells.fireball, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(9, false, kirinSpells.fireball, 60));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(9.5f, true, kirinSpells.timedFireball, 32));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(10, false, kirinSpells.timedFireball, 70));
            
            // POSITIONS
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(2, kirinPositions.position0));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(5, kirinPositions.position1));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(7, kirinPositions.position2));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(9, kirinPositions.position3));
        }
        
        public void InitPhaseTwo(KirinSpellsAPI kirinSpells, KirinMove kirinPositions)
        {
            GlobalEventManager.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("InitPhaseTwo");
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(2, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(3, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.RouletteSpellCast(3.5f, true, kirinSpells.fireBullet, 70,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.RouletteSpellCast(4.5f, true, kirinSpells.fireBullet, 80,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(5, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(5.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(6, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(6.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(7, false, kirinSpells.fireballSmall, 70));
            kirinSpells.StartCoroutine(kirinSpells.SpiralSpellCast(7.3f, true, kirinSpells.fireBullet, 100, 0.5f));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(8, true, kirinSpells.fireBullet, 26));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(8.5f, true, kirinSpells.fireball, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(9, false, kirinSpells.fireball, 60));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(9.5f, true, kirinSpells.timedFireball, 32));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(10, false, kirinSpells.timedFireball, 70));
            
            // POSITIONS
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(2, kirinPositions.position0));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(5, kirinPositions.position1));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(7, kirinPositions.position2));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(9, kirinPositions.position3));
            
        }
        
        public void InitPhaseThree(KirinSpellsAPI kirinSpells, KirinMove kirinPositions)
        {
            GlobalEventManager.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("InitPhaseThree");
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(2, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(3, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.RouletteSpellCast(3.5f, true, kirinSpells.fireBullet, 70,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.RouletteSpellCast(4.5f, true, kirinSpells.fireBullet, 80,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(5, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(5.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(6, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(6.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(7, false, kirinSpells.fireballSmall, 70));
            kirinSpells.StartCoroutine(kirinSpells.SpiralSpellCast(7.3f, true, kirinSpells.fireBullet, 100, 0.5f));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(8, true, kirinSpells.fireBullet, 26));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(8.5f, true, kirinSpells.fireball, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(9, false, kirinSpells.fireball, 60));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(9.5f, true, kirinSpells.timedFireball, 32));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(10, false, kirinSpells.timedFireball, 70));
            
            // POSITIONS
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(2, kirinPositions.position0));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(5, kirinPositions.position1));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(7, kirinPositions.position2));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(9, kirinPositions.position3));

        }
        
        public void InitPhaseFour(KirinSpellsAPI kirinSpells, KirinMove kirinPositions)
        {
            GlobalEventManager.OnPhaseChange.AddListener(OnPhaseChange);
            Debug.Log("InitPhaseFour");
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(2, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(3, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.RouletteSpellCast(3.5f, true, kirinSpells.fireBullet, 70,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.RouletteSpellCast(4.5f, true, kirinSpells.fireBullet, 80,0.001f));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(5, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(5.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(6, true, kirinSpells.fireBullet, 70));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(6.5f, true, kirinSpells.fireBullet, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(7, false, kirinSpells.fireballSmall, 70));
            kirinSpells.StartCoroutine(kirinSpells.SpiralSpellCast(7.3f, true, kirinSpells.fireBullet, 100, 0.5f));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(8, true, kirinSpells.fireBullet, 26));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(8.5f, true, kirinSpells.fireball, 80));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(9, false, kirinSpells.fireball, 60));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(9.5f, true, kirinSpells.timedFireball, 32));
            kirinSpells.StartCoroutine(kirinSpells.CircleSpellCast(10, false, kirinSpells.timedFireball, 70));
            
            // POSITIONS
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(2, kirinPositions.position0));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(5, kirinPositions.position1));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(7, kirinPositions.position2));
            kirinPositions.StartCoroutine(kirinPositions.MoveTo(9, kirinPositions.position3));
        }

        private void OnPhaseChange(int phase)
        {
            StopAllCoroutines();
        }
    }
}