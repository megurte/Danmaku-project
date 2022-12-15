using System.Collections.Concurrent;
using System.Numerics;
using Bullets;

namespace Utils
{
    public class ObjectPool<T> where T : new()
    {
        private readonly ConcurrentBag<T> _items = new ConcurrentBag<T>();
        private int _counter = 0;
        private int _maxCounter = 1000;

        public void Release(T item)
        {
            if (_counter >= _maxCounter) return;
            
            _items.Add(item);
            _counter++;
        }

        public T Get(T gameObjectOfType)
        {
            if (_items.TryTake(out var item))
            {
                _counter--;
                return item;
            }

            _items.Add(gameObjectOfType);
            _counter++;
            return gameObjectOfType;
        }
    }
}