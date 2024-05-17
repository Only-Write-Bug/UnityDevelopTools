
using Tools.ObjectPoolTool;
using Tools.ObjectPoolTool.Enum;

public static class ObjectPoolTool
{
    /// <summary>
    /// 申请创建对象池，每个类只能创建一个对象池，若对象池已存在，获取原有对象池
    /// </summary>
    /// <param name="defaultSize">默认对象池容量</param>
    /// <param name="memoryType">存储类型，影响对象池扩容效率</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ObjectPoolModel<T> ApplyObjectPool<T>(int defaultSize, OBJECT_POOL_MEMORY_TYPE memoryType)
        where T : class, IPoolElementRecycle, new()
    {
        return ObjectPoolManager.Instance.ApplyObjectPool<T>(defaultSize, memoryType);
    }
}