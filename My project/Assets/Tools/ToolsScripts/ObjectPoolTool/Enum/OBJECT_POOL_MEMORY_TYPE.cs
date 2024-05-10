namespace Tools.ObjectPoolTool.Enum
{
    /// <summary>
    /// 对象池存储类型
    /// </summary>
    public enum OBJECT_POOL_MEMORY_TYPE
    {
        //可预估容量，如果确认扩容频率不高，建议选择
        PREDICTABLE_CAPACITY,
        //不可预估容量，如果不确定是否会扩容且使用频率很高，建议选择
        UNPREDICTABLE_CAPACITY,
    }
}