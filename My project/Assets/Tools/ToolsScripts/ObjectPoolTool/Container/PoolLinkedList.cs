using System.Collections.Generic;
using Unity.VisualScripting;

namespace Tools.ObjectPoolTool.Container
{
    public class PoolLinkedList<T> : PoolContainerBase, IPoolContainer<T> where T : class, IPoolElementRecycle, new()
    {
        private BilateralLinkedList<T> _curContainer = new BilateralLinkedList<T>();

        public PoolLinkedList()
        {
            _curMaxSize = _defaultSize;
            StuffContainer();
        }

        public PoolLinkedList(int initSize)
        {
            _defaultSize = _curMaxSize = initSize;
            StuffContainer();
        }

        public void Add(T go)
        {
            if (_curContainer.count >= _curMaxSize)
                return;

            _curContainer.Add(go);
        }

        public void Expansion()
        {
            _curMaxSize += _defaultSize;
            StuffContainer();
        }

        public void Shrink()
        {
            _curMaxSize -= _defaultSize;
            while (_curContainer.count > _curMaxSize)
            {
                _curContainer.TryPop(out var value);
            }
            StuffContainer();
        }

        public T ApplyObject()
        {
            if (_curContainer.count <= 0)
                return null;

            if (!_curContainer.TryPop(out var value))
            {
                return new T();
            }
            return value;
        }

        public void RecycleObject(T go)
        {
            go.Recycle();
            if (_curContainer.count >= _curMaxSize)
                return;
            
            _curContainer.Add(go);
        }

        public void StuffContainer()
        {
            if (_curContainer.count >= _curMaxSize)
                return;

            while (_curContainer.count < _curMaxSize)
            {
                var tmpGo = new T();
                tmpGo.Recycle();
                _curContainer.Add(tmpGo);
            }
        }

        public void Clear()
        {
            _curContainer.Clear();
            _curContainer = null;
        }
    }
}