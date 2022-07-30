using System.Collections;
using System.Collections.Generic;

namespace Enemy
{
    public interface IShootable
    {
        public IEnumerator Shoot();
    }
}