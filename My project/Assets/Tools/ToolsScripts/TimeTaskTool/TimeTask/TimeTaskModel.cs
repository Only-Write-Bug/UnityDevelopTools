using System;
using Tools.ObjectPoolTool;
using Tools.ToolsScripts.EventCenterTool.Enum;

public class TimeTaskModel : IPoolElementRecycle
{
    private long _timeTaskID;
    public long timeTaskID => _timeTaskID;
    
    private ulong _tickEventID;

    private Action _task = null;
    
    private int _curLastTimeStep = 0;
    private int _timeStep = 0;
    public int timeStep => _timeStep;

    public int loop = 1;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="timeTaskID"></param>
    /// <param name="step"></param>
    /// <param name="callback"></param>
    public void init(long timeTaskID, int step, Action callback)
    {
        _timeTaskID = timeTaskID;
        _curLastTimeStep = _timeStep = step;
        _task = callback;
        
        _tickEventID = EventTool.SubscribeEvent(TimeTaskManager.eventName, EVENT_SUBSCRIBER_PRIORITY.FIRST,
            objects => this.OnTick());
    }

    /// <summary>
    /// 每个时间单位执行的方法
    /// </summary>
    /// <param name="objects"></param>
    private void OnTick(params dynamic[] objects)
    {
        _curLastTimeStep -= TimeTaskManager.tickStep;

        if (_curLastTimeStep <= timeStep)
        {
            if (!PerformTask())
                return;
            else
            {
                _curLastTimeStep = timeStep;
            }
        }
    }

    /// <summary>
    /// 执行任务
    /// </summary>
    /// <returns></returns>
    private bool PerformTask()
    {
        if (loop == 0)
        {
            TimeTaskManager.Instance.RemoveTimeTask(timeTaskID);
            return false;
        }
        
        _task.Invoke();
        if(--loop == 0)
        {
            TimeTaskManager.Instance.RemoveTimeTask(timeTaskID);
            return false;
        }

        return true;
    }
    
    /// <summary>
    /// 回收
    /// </summary>
    public void Recycle()
    {
        EventTool.UnSubscribeEvent(TimeTaskManager.eventName, _tickEventID);
        _tickEventID = 0;
        _task = null;
        _timeStep = 0;
    }
}