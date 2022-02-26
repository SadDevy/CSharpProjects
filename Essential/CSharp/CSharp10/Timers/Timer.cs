using System;
using System.Threading;

namespace Timers
{
    public class Timer
    {
        public delegate void TimerEventHandler(object sender, EventArgs e);

        public event TimerEventHandler CountdownStart;
        public event TimerEventHandler SecondsLeft;
        public event TimerEventHandler CountdownStop;

        private TimeSpan oneSecond = TimeSpan.FromSeconds(1);

        public string Name { get; private set; }
        public int RemainSeconds { get; private set; }

        public Timer(string name, int remainSeconds)
        {
            Name = name;
            RemainSeconds = remainSeconds;
        }

        public void Start()
        {
            CountdownStart?.Invoke(this, EventArgs.Empty);

            while (RemainSeconds >= 0)
            {
                SecondsLeft?.Invoke(this, EventArgs.Empty);
                RemainSeconds--;

                if (RemainSeconds > 0)
                    Thread.Sleep(oneSecond);
            }

            CountdownStop?.Invoke(this, EventArgs.Empty);
        }
    }
}
