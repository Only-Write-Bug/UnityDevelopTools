using System;
using System.Reflection;

public class ClassUtil
{
    /// <summary>
    /// 是否为同一个类中的同一个方法
    /// </summary>
    /// <param name="action1"></param>
    /// <param name="action2"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsSameMethod<T>(Action<T> action1, Action<T> action2)
    {
        var method1 = action1.Method;
        var method2 = action2.Method;

        return method1.DeclaringType == method2.DeclaringType && method1.Name == method2.Name;
    }
}