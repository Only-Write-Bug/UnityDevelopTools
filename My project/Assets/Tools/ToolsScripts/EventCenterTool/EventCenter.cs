using System;
using System.Collections.Generic;
using Tools.ToolsScripts.EventCenterTool;
using Tools.ToolsScripts.EventCenterTool.Enum;

public class EventCenter : ToolBase<EventCenter>
{
    private Dictionary<string, TieredSubscribersContainer> EventsDic =
        new Dictionary<string, TieredSubscribersContainer>();

    /// <summary>
    /// 创建事件
    /// </summary>
    public void CreateEvent(string eventName)
    {
        EventsDic.TryAdd(eventName, new TieredSubscribersContainer());
    }
    private void CreateEvent(string eventName, ref TieredSubscribersContainer tieredSubscriber)
    {
        CreateEvent(eventName);
        tieredSubscriber = EventsDic[eventName];
    }

    /// <summary>
    /// 订阅事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <returns>订阅号</returns>
    public ulong SubscribeEvent(string eventName,EVENT_SUBSCRIBER_PRIORITY priority, Action<dynamic[]> callback)
    {
        if(!EventsDic.TryGetValue(eventName, out var tieredSubscriber))
            CreateEvent(eventName, ref tieredSubscriber);
        
        return tieredSubscriber.AddSubscriber(priority, callback);
    }

    /// <summary>
    /// 取消订阅事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="SubscriptionNumber"></param>
    public void UnSubscribeEvent(string eventName, ulong subscriptionNumber)
    {
        if (!EventsDic.TryGetValue(eventName, out var tieredSubscriber))
            return;
        
        tieredSubscriber.RemoveSubscriber(subscriptionNumber);
    }

    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    public void PublishEvent(string eventName, params dynamic[] data)
    {
        if (!EventsDic.TryGetValue(eventName, out var tieredSubscriber))
            return;
        
        tieredSubscriber.Publish(data);
    }

    /// <summary>
    /// 获取订阅号的订阅优先级
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="subscriptionNumber"></param>
    /// <returns></returns>
    public EVENT_SUBSCRIBER_PRIORITY? GetSubscriberPriority(string eventName, ulong subscriptionNumber)
    {
        if (!EventsDic.TryGetValue(eventName, out var tieredSubscriber))
            return null;

        var priority = tieredSubscriber.GetSubscriberPriority(subscriptionNumber);
        return Enum.Parse<EVENT_SUBSCRIBER_PRIORITY>(priority);
    }

    /// <summary>
    /// 注销事件
    /// </summary>
    /// <param name="eventName"></param>
    public void LogoutEvent(string eventName)
    {
        if (!EventsDic.TryGetValue(eventName, out var tieredSubscriber))
            return;
        
        tieredSubscriber.Clear();
        EventsDic.Remove(eventName);
    }
}