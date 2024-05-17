namespace Tools.ObjectPoolTool.Container
{
    public interface IPoolContainer<T>
    {
        /// <summary>
        /// 添加实例
        /// </summary>
        /// <param name="go"></param>
        public void Add(T go);
        /// <summary>
        /// 扩充容量
        /// </summary>
        public void Expansion();
        /// <summary>
        /// 缩减容量
        /// </summary>
        public void Shrink();
        /// <summary>
        /// 申请实例
        /// </summary>
        public T ApplyObject();
        /// <summary>
        /// 回收实例
        /// </summary>
        public void RecycleObject(T go);
        /// <summary>
        /// 填充容器
        /// </summary>
        public void StuffContainer();
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear();
    }
}