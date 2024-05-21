using System;

public static class TimeTaskTool
{
    /// <summary>
    /// 添加时间任务
    /// </summary>
    /// <param name="step">间隔(毫秒)</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static long AddTimeTask(int step, Action callback)
    {
        return TimeTaskManager.Instance.AddTimeTask(step, callback);
    }

    /// <summary>
    /// 添加时间任务
    /// </summary>
    /// <param name="step">间隔(毫秒)</param>
    /// <param name="loop">循环次数, 若为负数则一直执行</param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static long AddTimeTask(int step, int loop, Action callback)
    {
        return TimeTaskManager.Instance.AddTimeTask(step, loop, callback);
    }

    /// <summary>
    /// 移除时间任务
    /// </summary>
    /// <param name="timeTaskID"></param>
    public static void RemoveTimeTask(long timeTaskID)
    {
        TimeTaskManager.Instance.RemoveTimeTask(timeTaskID);
    }
}