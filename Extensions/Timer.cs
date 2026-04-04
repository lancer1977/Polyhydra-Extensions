using System;
using System.Diagnostics;

namespace PolyhydraGames.Extensions;

public class Timer
{
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    public int Ticks { get; private set; }
    public long ElapsedMilliseconds => _stopwatch.ElapsedMilliseconds;

    public void Start()
    {
        Ticks = 0;
        _stopwatch.Restart();
    }

    public int Tick()
    {
        Ticks += 1;
        return Ticks;
    }

    public void Pause()
    {
        _stopwatch.Stop();
    }

    public void Resume()
    {
        _stopwatch.Start();
    }

    public void Stop()
    {
        _stopwatch.Stop();
    }

    public string TimeStamp()
    {
        var elapsed = _stopwatch.Elapsed;
        var average = Ticks > 0 ? elapsed.TotalMilliseconds / Ticks : elapsed.TotalMilliseconds;
        return $"Average = {average:0.##}\nTimeSpan={elapsed.TotalMilliseconds:0.##}";
    }
}
//public class MethodTimer: Timer
//{
//    private DateTime StartTime;
//    private DateTime EndTime;

//    public void WrapStart()
//    {
//        StartTime = DateTime.Now;

//        Start();
//        Console.WriteLine("Start Time:" + DateTime.Now.ToString());
//    }

//    public void WrapStop()
//    {
//        EndTime = DateTime.Now;
//        Stop();
//        Console.WriteLine("End Time:" + DateTime.Now.ToString());
//        Console.WriteLine("Total Time:" + (StartTime - EndTime) );
//    }

//}
