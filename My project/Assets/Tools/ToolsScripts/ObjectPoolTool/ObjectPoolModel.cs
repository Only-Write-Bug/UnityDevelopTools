using Tools.ObjectPoolTool.Container;
using Tools.ObjectPoolTool.Enum;

namespace Tools.ObjectPoolTool
{
    public class ObjectPoolModel<T> where T : class, IPoolElementRecycle, new()
    {
        private readonly string _poolName;
        public string poolName => _poolName;

        protected OBJECT_POOL_MEMORY_TYPE _memoryType;
        public OBJECT_POOL_MEMORY_TYPE memoryType => _memoryType;

        private readonly IPoolContainer<T> _curContainer = null;
        public IPoolContainer<T> curContainer => _curContainer;
        
        public ObjectPoolModel(int defaultSize, OBJECT_POOL_MEMORY_TYPE poolMemoryType)
        {
            _poolName = typeof(T).ToString() + "_pool";
            _memoryType = poolMemoryType;
            switch (_memoryType)
            {
                case OBJECT_POOL_MEMORY_TYPE.PREDICTABLE_CAPACITY:
                    _curContainer = new PoolArray<T>(defaultSize);
                    break;
                case OBJECT_POOL_MEMORY_TYPE.UNPREDICTABLE_CAPACITY:
                    break;
            }
        }
    }
}