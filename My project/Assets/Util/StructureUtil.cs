using System;
using System.Collections.Generic;
using System.Linq;

public static class StructureUtil
{
    /// <summary>
    /// 清除无效数据
    /// </summary>
    /// <param name="source">清理的值</param>
    /// <param name="data">需要被清理的数据</param>
    /// <typeparam name="T"></typeparam>
    /// <returns>未被清理的数据</returns>
    public static T[] ClearInvalidData<T>(T[] source, T[] data)
    {
        var tmp = new List<T>();
        
        for (int i = 0; i < data.Length; i++)
        {
            if(!source.Contains(data[i]))
                tmp.Add(data[i]);
        }

        return tmp.ToArray();
    }

    /// <summary>
    /// 拼接数组
    /// </summary>
    /// <param name="preArray"></param>
    /// <param name="lastArray"></param>
    /// <typeparam name="T"></typeparam>
    public static void SpliceArray<T>(ref T[] preArray, T[] lastArray)
    {
        var tmpArray = new T[preArray.Length + lastArray.Length];
        for (int i = 0; i < preArray.Length; i++)
        {
            tmpArray[i] = preArray[i];
        }

        for (int i = 0; i < lastArray.Length; i++)
        {
            tmpArray[preArray.Length + i] = lastArray[i];
        }

        preArray = tmpArray;
    }
}