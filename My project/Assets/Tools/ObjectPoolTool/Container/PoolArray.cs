using System.Collections.Generic;

namespace Tools.ObjectPoolTool.Container
{
    public class PoolArray<T> : PoolContainerBase, IPoolContainer<T> where T : class, IPoolElementRecycle, new()
    {
        private Queue<T> _curContainer = null;

        public PoolArray()
        {
            _curContainer = new Queue<T>(_defaultSize);
            _curMaxSize = _defaultSize;
        }

        public PoolArray(int initSize)
        {
            _defaultSize = initSize;
            _curContainer = new Queue<T>(_defaultSize);
            _curMaxSize = _defaultSize;
        }

        public void Add(T go)
        {
            if (_curContainer.Count >= _curMaxSize)
                return;
            
            _curContainer.Enqueue(go);
        }

        public void Expansion()
        {
        }

        public void Shrink()
        {
        }

        public T ApplyElement()
        {
            return _curContainer.Count > 0 ? _curContainer.Dequeue() : new T();
        }

        public void RecycleElement(T go)
        {
            if (_curContainer.Count < _curMaxSize)
            {
                go.Recycle();
                _curContainer.Enqueue(go);
                return;
            }

            _outBoundsCount++;
        }
    }
}