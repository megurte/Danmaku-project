using System;
using UnityEngine;

namespace Kirin
{
    public class KirinScript : MonoBehaviour
    {
        private KirinSpells _kirinSpells;

        private void Start()
        {
            _kirinSpells = GetComponent<KirinSpells>();
            
            
            //_kirinSpells.StartCoroutine(_kirinSpells.RouletteFireball(1, true, _kirinSpells.fireball, 70, 0.01f));


            _kirinSpells.StartCoroutine(_kirinSpells.RouletteFireball(2, true, _kirinSpells.fireBullet, 70,0.001f));
            _kirinSpells.StartCoroutine(_kirinSpells.RouletteFireball(3, true, _kirinSpells.fireBullet, 80,0.001f));
            _kirinSpells.StartCoroutine(_kirinSpells.RouletteFireball(3.5f, true, _kirinSpells.fireBullet, 70,0.001f));
            _kirinSpells.StartCoroutine(_kirinSpells.RouletteFireball(4.5f, true, _kirinSpells.fireBullet, 80,0.001f));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(5, true, _kirinSpells.fireBullet, 70));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(5.5f, true, _kirinSpells.fireBullet, 80));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(6, true, _kirinSpells.fireBullet, 70));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(6.5f, true, _kirinSpells.fireBullet, 80));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(7, false, _kirinSpells.fireballSmall, 70));
            _kirinSpells.StartCoroutine(_kirinSpells.SpiralFireball(7.3f, true, _kirinSpells.fireBullet, 100, 0.5f));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(8, true, _kirinSpells.fireBullet, 26));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(8.5f, true, _kirinSpells.fireball, 80));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(9, false, _kirinSpells.fireball, 60));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(9.5f, true, _kirinSpells.fireballSmall, 32));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(10, false, _kirinSpells.fireball, 70));
            
            /*_kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(2, false, _kirinSpells.fireball, 40));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(3, true, _kirinSpells.fireball, 40));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(3.5f, false, _kirinSpells.fireball, 50));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(4.5f, true, _kirinSpells.fireball, 50));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(5, false, _kirinSpells.fireball, 70));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(5.5f, true, _kirinSpells.fireball, 80));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(6, false, _kirinSpells.fireball, 70));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(6.5f, true, _kirinSpells.fireball, 80));*/
            
            
            /*_kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(4, false, _kirinSpells.fireballSmall, 26));
            _kirinSpells.StartCoroutine(_kirinSpells.SpiralFireball(4.3f, true, _kirinSpells.fireBullet, 100, 0.5f));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(7, true, _kirinSpells.fireBullet, 26));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(8, true, _kirinSpells.fireball, 15));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(9, false, _kirinSpells.fireball, 18));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(10, true, _kirinSpells.fireballSmall, 32));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(11, false, _kirinSpells.fireball, 40));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(12, false, _kirinSpells.fireball, 20));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(13, true, _kirinSpells.fireball, 14));*/
        }
    }
}