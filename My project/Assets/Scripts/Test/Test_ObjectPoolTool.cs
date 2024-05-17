using Tools.ObjectPoolTool;
using UnityEngine;

namespace Test
{
    public class Test_ObjectPoolTool
    {
        
    }

    public class cars : IPoolElementRecycle
    {
        public void Recycle()
        {
            Debug.Log("Car is recycling");
        }
    }
}