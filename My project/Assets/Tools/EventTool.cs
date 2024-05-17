using System;
using Tools.ToolsScripts.EventCenterTool.Enum;

public static class EventTool
{
    /// <summary>
    /// 创建事件
    /// </summary>
    public static void CreateEvent(string eventName)
    {
        EventCenter.Instance.CreateEvent(eventName);
    }

    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns>订阅号</returns>
    public static void SubscribeEvent(string eventName, EVENT_SUBSCRIBER_PRIORITY priority, Action<dynamic[]> callback)
    {
        EventCenter.Instance.SubscribeEvent(eventName, priority, callback);
    }

    /// <summary>
    /// 取消订阅事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="SubscriptionNumber"></param>
    public static void UnSubscribeEvent(string eventName, ulong subscriptionNumber)
    {
        EventCenter.Instance.UnSubscribeEvent(eventName, subscriptionNumber);
    }

    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    public static void PublishEvent(string eventName, params dynamic[] data)
    {
        EventCenter.Instance.PublishEvent(eventName, data);
    }

    /// <summary>
    /// 获取订阅号的订阅优先级
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="subscriptionNumber"></param>
    /// <returns></returns>
    public static EVENT_SUBSCRIBER_PRIORITY? GetSubscriberPriority(string eventName, ulong subscriptionNumber)
    {
        return EventCenter.Instance.GetSubscriberPriority(eventName, subscriptionNumber);
    }

    /// <summary>
    /// 注销事件
    /// </summary>
    /// <param name="eventName"></param>
    public static void LogoutEvent(string eventName)
    {
        EventCenter.Instance.LogoutEvent(eventName);
    }
}