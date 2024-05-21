using System;

public static class SnowFlakNumberTool
{
    /// <summary>
    /// 生成ID
    /// </summary>
    /// <returns></returns>
    public static long CreateID()
    {
        return SnowFlakNumberMechine.Instance.CreateID();
    }
}

public class SnowFlakNumberMechine : ToolBase<SnowFlakNumberMechine>
{
    //默认时间戳：2000/01/01
    private const long _timestamp = 946684800000L;
    //工作机ID位数
    private const int _workMechineIDBits = 5;
    //序列号位数
    private const int _sequenceBits = 12;

    private const long _workMechineID = 1;

    private readonly object _lock = new object();

    private long _sequence = 0;
    private long _lastTimestamp = -1L;

    /// <summary>
    /// 生成ID
    /// </summary>
    /// <returns></returns>
    public long CreateID()
    {
        lock (_lock)
        {
            var curTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            if (curTimestamp < _lastTimestamp)
                throw new Exception("Clock moved backwards, unable to generate ID");

            if (_lastTimestamp == curTimestamp)
            {
                _sequence = (_sequence + 1) & ((1 << _sequenceBits) - 1);

                if (_sequence == 0)
                {
                    curTimestamp = WaitNextMillis(curTimestamp);
                }
            }
            else
            {
                _sequence = 0;
            }

            _lastTimestamp = curTimestamp;

            var id = ((curTimestamp - _timestamp) << (_workMechineIDBits + _sequenceBits))
                     | (_workMechineID << _sequenceBits)
                     | _sequence;

            return id;
        }
    }

    private long WaitNextMillis(long lastTimestamp)
    {
        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        while (timestamp <= lastTimestamp)
        {
            timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

        return timestamp;
    }
}