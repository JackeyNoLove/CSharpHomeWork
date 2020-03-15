using System;
using System.Threading;

namespace homework4_2
{
    public delegate void TickHandler(int CurrentTime);
    public delegate void AlarmHandler();
    public class Alarm
    {
        public event TickHandler OnTick;
        public event AlarmHandler OnAlarm;
        public int CurrentTime=1;
        public Alarm()
        {
            OnTick += tick;
            OnAlarm += alarming;
        }
        public void Start()
        {
            for(CurrentTime = 1; CurrentTime < 100; CurrentTime++)
            {
                OnTick(CurrentTime);
                Thread.Sleep(1000);
            }
        }
        void tick(int CurrentTime)
        {
            if (CurrentTime % 10 == 0) OnAlarm();
            else Console.WriteLine("现在时间:"+CurrentTime);
        }
        void alarming()
        {
            Console.WriteLine("整点报时！");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Alarm alarm = new Alarm();
            alarm.Start();
        }
    }
}
