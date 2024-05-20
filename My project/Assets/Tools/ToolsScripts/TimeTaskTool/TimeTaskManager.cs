using System.Timers;
using UnityEngine;

public class TimeTaskManager : ToolBase<TimeTaskManager>
{
    public const string eventName = "TimeTask_Tick";
    public const int tickStep = 500;

    private Timer _timer;

    public TimeTaskManager()
    {
        EventTool.CreateEvent(eventName);
        Application.quitting += Destory;

        _timer = new Timer(tickStep);
        _timer.Elapsed += (object sender, ElapsedEventArgs e) => {  EventTool.PublishEvent(eventName, null); };
        _timer.Start();
    }
    
    

    private void Destory()
    {
        _timer.Stop();
        _timer.Dispose();
        _timer = null;
    }
}