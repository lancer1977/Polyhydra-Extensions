using System;

namespace PolyhydraGames.Extensions;

public class Timer
{
    private DateTime StartTime { get; set; }
    private DateTime CurrentTime { get; set; }
    private DateTime HoldTime { get; set; }
    public int Ticks { get; private set; }
    public Timer()
    {
          
    }

    public void Start()
    {
        StartTime = DateTime.Now;
        CurrentTime = DateTime.Now;
        HoldTime = DateTime.Now;
    }

    public int Tick()
    {
        Ticks += 1;
        return Ticks;
    }
    public string TimeStamp()
    {
          
        HoldTime = CurrentTime;
        CurrentTime = DateTime.Now;
        var span = (CurrentTime - HoldTime).Milliseconds;
        var totalSpan = "Average = " + ((CurrentTime - StartTime).Milliseconds / Ticks);
        return "Average" + totalSpan +
               "\nTimeSpan=" + span;
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