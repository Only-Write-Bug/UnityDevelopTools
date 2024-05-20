using System;
using Tools.ObjectPoolTool;
using Tools.ToolsScripts.EventCenterTool.Enum;

public class TimeTaskBase : IPoolElementRecycle
{
    private ulong _tickEventID;
    public ulong tickEventID => _tickEventID;

    protected dynamic[] _datas = null;
    protected Action _task = null;
    
    private int _curLastTimeStep = 0;
    private int _timeStep = 0;
    public int timeStep => _timeStep;

    public virtual void init(int time, int loop, Action callback)
    {
        _curLastTimeStep = _timeStep = time;
        _task = callback;
        
        _tickEventID = EventTool.SubscribeEvent(TimeTaskManager.eventName, EVENT_SUBSCRIBER_PRIORITY.FIRST,
            _datas => this.OnTick());
    }

    protected virtual void OnTick(params dynamic[] datas)
    {
        _curLastTimeStep -= TimeTaskManager.tickStep;
    }
    
    public void Recycle()
    {
        EventTool.UnSubscribeEvent(TimeTaskManager.eventName, tickEventID);
        _tickEventID = 0;
        _task = null;
        _timeStep = 0;
    }
}