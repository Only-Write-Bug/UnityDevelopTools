namespace Tools.ObjectPoolTool.Container
{
    public class PoolContainerBase
    {
        protected int _defaultSize = 4;
        protected int _curMaxSize;
        //超界计数，当回收实例超出当前容器容量时+1，会影响自动扩容
        protected int _outBoundsCount = 0;
    }
}