using System;
using System.Collections.Generic;
using Unity.VisualScripting;

namespace Tools.ObjectPoolTool.Container
{
    public class PoolArray<T> : PoolContainerBase, IPoolContainer<T> where T : class, IPoolElementRecycle, new()
    {
        private Queue<T> _curContainer = null;

        public PoolArray()
        {
            _curContainer = new Queue<T>(_defaultSize);
            _curMaxSize = _defaultSize;
            StuffContainer();
        }

        public PoolArray(int initSize)
        {
            _defaultSize = initSize;
            _curContainer = new Queue<T>(_defaultSize);
            _curMaxSize = _defaultSize;
            StuffContainer();
        }

        public void Add(T go)
        {
            if (_curContainer.Count >= _curMaxSize)
                return;
            
            _curContainer.Enqueue(go);
        }

        public void Expansion()
        {
            _curMaxSize += _defaultSize;

            var tmpContainer = new Queue<T>(_curMaxSize);
            for (int i = 0; i < _curMaxSize; i++)
            {
                if (_curContainer.Count > 0)
                {
                    tmpContainer.Enqueue(_curContainer.Dequeue());
                    continue;
                }
            }

            StuffContainer();
            _curContainer.Clear();
            _curContainer = tmpContainer;
        }

        public void Shrink()
        {
            _curMaxSize -= _defaultSize;
            while (_curContainer.Count > _curMaxSize)
            {
                _curContainer.Dequeue();
            }

            StuffContainer();
        }

        public T ApplyObject()
        {
            return _curContainer.Count > 0 ? _curContainer.Dequeue() : new T();
        }

        public void RecycleObject(T go)
        {
            if (_curContainer.Count < _curMaxSize)
            {
                go.Recycle();
                _curContainer.Enqueue(go);
                return;
            }

            _outBoundsCount++;
        }

        public void StuffContainer()
        {
            if(_curContainer.Count >= _curMaxSize)
                return;

            while (_curContainer.Count < _curMaxSize)
            {
                var tmpGo = new T();
                tmpGo.Recycle();
                _curContainer.Enqueue(tmpGo);
            }
        }

        public void Clear()
        {
            _curContainer.Clear();
            _curContainer = null;
        }
    }
}