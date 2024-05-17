using System.Collections.Generic;
using Tools.ObjectPoolTool.Container;
using Tools.ObjectPoolTool.Enum;
using UnityEngine;

namespace Tools.ObjectPoolTool
{
    public class ObjectPoolManager : ToolBase<ObjectPoolManager>
    {
        private Dictionary<string, object> _objectPoolsDic = new Dictionary<string, object>();
        
        /// <summary>
        /// 申请创建对象池，每个类只能创建一个对象池，若对象池已存在，获取原有对象池
        /// </summary>
        /// <param name="defaultSize">默认对象池容量</param>
        /// <param name="memoryType">存储类型，影响对象池扩容效率</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ObjectPoolModel<T> ApplyObjectPool<T>(int defaultSize, OBJECT_POOL_MEMORY_TYPE memoryType)
            where T : class, IPoolElementRecycle, new()
        {
            if (!_objectPoolsDic.ContainsKey(typeof(T).ToString()) || _objectPoolsDic[typeof(T).ToString()] == null)
            {
                _objectPoolsDic[typeof(T).ToString()] = new ObjectPoolModel<T>(defaultSize, memoryType);
            }
            
            return _objectPoolsDic[typeof(T).ToString()] as ObjectPoolModel<T>;
        }

        /// <summary>
        /// 申请获取对象池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ObjectPoolModel<T> GetObjectPool<T>() where T : class, IPoolElementRecycle, new()
        {
            if (_objectPoolsDic.ContainsKey(typeof(T).ToString()) && _objectPoolsDic[typeof(T).ToString()] != null)
            {
                return _objectPoolsDic[typeof(T).ToString()] as ObjectPoolModel<T>;
            }

            return null;
        }

        /// <summary>
        /// 清空对象池
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ClearObjectPool<T>() where T : class, IPoolElementRecycle, new()
        {
            if (_objectPoolsDic.ContainsKey(typeof(T).ToString()))
            {
                var objPool = _objectPoolsDic[typeof(T).ToString()] as ObjectPoolModel<T>;
                _objectPoolsDic.Remove(typeof(T).ToString());
            }
        }
    }
}