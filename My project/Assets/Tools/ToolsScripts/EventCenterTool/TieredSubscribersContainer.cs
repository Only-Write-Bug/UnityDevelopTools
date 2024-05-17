using System;
using System.Collections.Generic;
using Tools.ToolsScripts.EventCenterTool.Enum;
using UnityEngine;

/// <summary>
/// 分级订阅者容器
/// </summary>
public class TieredSubscribersContainer
{
    //订阅号发号机
    private ulong _curSubscriptionNumber = 0;
    private ulong _subscriptionNumberMachine => _curSubscriptionNumber++;

    private LinkedList<SubscriberEntity>[] _subscriptionSequence = null;
    private Dictionary<string, LinkedList<SubscriberEntity>> _prioritySubscriberDic =
        new Dictionary<string, LinkedList<SubscriberEntity>>();

    private Dictionary<string, List<ulong>> _subscriptionNumberDicByPriority = new Dictionary<string, List<ulong>>();

    public TieredSubscribersContainer()
    {
        var priorityArray = System.Enum.GetValues(typeof(EVENT_SUBSCRIBER_PRIORITY));

        _subscriptionSequence = new LinkedList<SubscriberEntity>[priorityArray.Length];

        for (var i = 0; i < priorityArray.Length; i++)
        {
            var linkedList = new LinkedList<SubscriberEntity>();
            _subscriptionSequence[i] = linkedList;
            _prioritySubscriberDic.Add(priorityArray.GetValue(i).ToString(), linkedList);
            _subscriptionNumberDicByPriority[priorityArray.GetValue(i).ToString()] = new List<ulong>();
        }
    }

    /// <summary>
    /// 订阅者实体，包含订阅号和回调
    /// </summary>
    private class SubscriberEntity
    {
        private readonly ulong _subscriptionNumber;
        public ulong subscriptionNumber => _subscriptionNumber;
        private readonly Action<dynamic[]> _callback;
        public Action<dynamic[]> callback => _callback;

        public SubscriberEntity(ulong subscriptionNumber, Action<dynamic[]> callback)
        {
            _subscriptionNumber = subscriptionNumber;
            _callback = callback;
        }
    }

    /// <summary>
    /// 添加订阅者
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="eventSubscriberPriority"></param>
    /// <returns></returns>
    public ulong AddSubscriber(EVENT_SUBSCRIBER_PRIORITY priority, Action<dynamic[]> callback)
    {
        if (!_prioritySubscriberDic.TryGetValue(priority.ToString(), out var linkedList))
        {
            //TODO 异常处理
        }

        foreach (var subscriberEntity in linkedList)
        {
            if (ClassUtil.IsSameMethod<dynamic[]>(callback, subscriberEntity.callback))
                return subscriberEntity.subscriptionNumber;
        }
        
        var subscriber = new SubscriberEntity(_subscriptionNumberMachine, callback);
        linkedList.AddLast(subscriber);
        _subscriptionNumberDicByPriority[priority.ToString()].Add(subscriber.subscriptionNumber);

        return subscriber.subscriptionNumber;
    }

    /// <summary>
    /// 移除订阅者
    /// </summary>
    /// <param name="subscriptionNumber"></param>
    public void RemoveSubscriber(ulong subscriptionNumber)
    {
        var priority = GetSubscriberPriority(subscriptionNumber);

        SubscriberEntity tmpSubscriber = null;
        foreach (var subscriber in _prioritySubscriberDic[priority])
        {
            if (subscriptionNumber == subscriber.subscriptionNumber)
            {
                tmpSubscriber = subscriber;
                break;
            }
        }

        if (tmpSubscriber == null)
            return;
        _prioritySubscriberDic[priority].Remove(tmpSubscriber);
        _subscriptionNumberDicByPriority[priority].Remove(subscriptionNumber);
    }

    /// <summary>
    /// 获取订阅者的优先级
    /// </summary>
    /// <param name="subscriptionNumber"></param>
    /// <returns></returns>
    public string GetSubscriberPriority(ulong subscriptionNumber)
    {
        foreach (var key in _subscriptionNumberDicByPriority.Keys)
        {
            foreach (var number in _subscriptionNumberDicByPriority[key])
            {
                if (number == subscriptionNumber)
                    return key;
            }
        }

        return null;
    }

    /// <summary>
    /// 发布事件
    /// </summary>
    /// <param name="data"></param>
    public void Publish(params dynamic[] data)
    {
        for (var i = 0; i < _subscriptionSequence.Length; i++)
        {
            foreach (var subscriber in _subscriptionSequence[i])
            {
                subscriber.callback.Invoke(data);
            }
        }
    }

    /// <summary>
    /// 清空
    /// </summary>
    public void Clear()
    {
        _subscriptionSequence = null;
        _prioritySubscriberDic.Clear();
        _prioritySubscriberDic = null;
        _subscriptionNumberDicByPriority.Clear();
        _subscriptionNumberDicByPriority = null;
    }
}