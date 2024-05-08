using Tools.ObjectPoolTool.Enum;

namespace Tools.ObjectPoolTool
{
    public class ObjectPoolBase<T> where T : class
    {
        protected string _poolName = null;
        public string poolName => _poolName;

        protected OBJECT_POOL_MEMORY_TYPE _memoryType;
        public OBJECT_POOL_MEMORY_TYPE memoryType => _memoryType;
        
        
    }
}