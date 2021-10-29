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
            
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(2, false, _kirinSpells.fireballSmall, 32));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(3, true, _kirinSpells.fireball, 24));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(4, false, _kirinSpells.fireballSmall, 26));
            _kirinSpells.StartCoroutine(_kirinSpells.SpiralFireball(4.3f, true, _kirinSpells.fireball, 100, 0.5f));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(7, true, _kirinSpells.fireball, 26));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(8, true, _kirinSpells.fireball, 15));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(9, false, _kirinSpells.fireball, 18));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(10, true, _kirinSpells.fireballSmall, 32));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(11, false, _kirinSpells.fireball, 40));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(12, false, _kirinSpells.fireball, 20));
            _kirinSpells.StartCoroutine(_kirinSpells.CircleFireball(13, true, _kirinSpells.fireball, 14));
        }
    }
}