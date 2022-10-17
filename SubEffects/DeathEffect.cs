using System.Collections;
using UnityEngine;

namespace SubEffects
{
    public class DeathEffect : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(Destroy());
        }

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}
