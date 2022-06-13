using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Kirin
{
    public class KirinSpellsAPI : KirinSpells
    {
        /// <summary>
        /// Spell with delay between bullets spawn
        /// </summary>
        public IEnumerator RouletteSpellCast(float waitTime, bool change, GameObject bullet, float count, float delay)
        {
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(RouletteSpellFireball(delay, change, bullet, count));
        }
    
        /// <summary>
        /// SpiralSpell with delay
        /// </summary>
        public IEnumerator SpiralSpellCast(float waitTime, bool change, GameObject bullet, float count, float delay)
        {
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(SpiralSpellFireball(delay, change, bullet, count));
        }

        
        //
        public void DoSome()
        {
            Debug.Log("pass");
        }

    
        /// <summary>
        /// Spell with delay between bullets spawn
        /// </summary>
        /// TODO
        public IEnumerator CircleSpellCastTest(float waitTime, bool change, GameObject bullet, int count)
        {
            yield return new WaitForSeconds(waitTime);
            IcicleSpellCircle(change, bullet, count);
        }

        /// <summary>
        /// Full Circle bullet spawn 
        /// </summary>
        public IEnumerator CircleSpellCast(float waitTime, bool change, GameObject bullet, int count)
        {
            yield return new WaitForSeconds(waitTime);
            FireballSpellCircle(change, bullet, count);
        }

        /// <summary>
        /// Spell with delay between bullets spawn
        /// </summary>
        public IEnumerator LeftSideSpellCast(float waitTime, bool change, GameObject bullet, int count)
        {
            yield return new WaitForSeconds(waitTime);
            FireballSpellLeftToRight(change, bullet, count);
        }
    }
}