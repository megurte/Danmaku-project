using System.Collections;
using UnityEngine;

namespace Kirin
{
    public class KirinSpellsAPI : KirinSpells
    {
        /// <summary>
        /// Spell with delay between bullets spawn
        /// </summary>
        public IEnumerator RouletteSpellCastWithDelay(KirinSpellSettingsWithDelay settings)
        {
            yield return new WaitForSeconds(settings.waitTime);
            StartCoroutine(RouletteSpellFireball(settings.delay, settings.change, settings.bullet, settings.count));
        }
    
        /// <summary>
        /// SpiralSpell with delay
        /// </summary>
        public IEnumerator SpiralSpellCast(KirinSpellSettingsWithDelay settings)
        {
            yield return new WaitForSeconds(settings.waitTime);
            StartCoroutine(SpiralSpellFireball(settings.delay, settings.change, settings.bullet, settings.count));
        }

        /// <summary>
        /// Full Circle bullet spawn 
        /// </summary>
        public IEnumerator CircleSpellCast(KirinSpellSettings settings)
        {
            yield return new WaitForSeconds(settings.waitTime);
            FireballSpellCircle(settings.change, settings.bullet, settings.count);
        }

        /// <summary>
        /// Spell with delay between bullets spawn
        /// </summary>
        public IEnumerator LeftSideSpellCast(KirinSpellSettings settings)
        {
            yield return new WaitForSeconds(settings.waitTime);
            FireballSpellLeftToRight(settings.change, settings.bullet, settings.count);
        }
    }
}