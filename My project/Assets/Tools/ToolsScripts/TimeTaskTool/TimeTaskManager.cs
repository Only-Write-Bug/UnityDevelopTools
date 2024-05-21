using System;
using System.Collections.Generic;
using System.Timers;
using Tools.ObjectPoolTool;
using Tools.ObjectPoolTool.Enum;
using UnityEngine;

public class TimeTaskManager : ToolBase<TimeTaskManager>
{
    public const string eventName = "TimeTask_Tick";
    public const int tickStep = 500;

    private Timer _timer;

    private readonly ObjectPoolModel<TimeTaskModel> _taskPool;
    private readonly Dictionary<long, TimeTaskModel> _timeTaskDic = new Dictionary<long, TimeTaskModel>();

    public TimeTaskManager()
    {
        _taskPool = ObjectPoolTool.ApplyObjectPool<TimeTaskModel>(16, OBJECT_POOL_MEMORY_TYPE.UNPREDICTABLE_CAPACITY);
        EventTool.CreateEvent(eventName);
        ApplicationLifeCycle.onPause += () => { _timer.Stop(); };
        ApplicationLifeCycle.onReopen += () => { _timer.Start(); };
        ApplicationLifeCycle.onQuit += Destory;

        _timer = new Timer(tickStep);
        _timer.Elapsed += (object sender, ElapsedEventArgs e) => { EventTool.PublishEvent(eventName, null); };
        _timer.Start();
    }

    /// <summary>
    /// 添加时间任务
    /// </summary>
    /// <param name="step">间隔</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public long AddTimeTask(int step, Action callback)
    {
        var timeTask = _taskPool.ApplyObject();
        timeTask.init(SnowFlakNumberTool.CreateID(), step, callback);
        _timeTaskDic[timeTask.timeTaskID] = timeTask;

        return timeTask.timeTaskID;
    }

    /// <summary>
    /// 添加时间任务
    /// </summary>
    /// <param name="step"></param>
    /// <param name="loop">循环次数</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public long AddTimeTask(int step, int loop, Action callback)
    {
        var timeTaskID = AddTimeTask(step, callback);
        var timeTask = _timeTaskDic[timeTaskID];
        timeTask.loop = loop;

        return timeTaskID;
    }

    /// <summary>
    /// 移除时间任务
    /// </summary>
    /// <param name="timeTaskID"></param>
    public void RemoveTimeTask(long timeTaskID)
    {
        if (_timeTaskDic.TryGetValue(timeTaskID, out var timeTask))
        {
            timeTask.Recycle();
            _taskPool.RecycleObject(timeTask);
            _timeTaskDic.Remove(timeTaskID);
        }
    }
    
    private void Destory()
    {
        _timer.Stop();
        _timer.Dispose();
        _timer = null;
    }
}