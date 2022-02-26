using System;
using System.Threading;

namespace Timers
{
    public class HandlerByAnonymousDelegate : ICoutDownNotifier
    {
        private Timer timer;

        private Action<string, int> startAction;
        private Action<string> stopAction;

        private Timer.TimerEventHandler countdownStart;
        private Timer.TimerEventHandler secondsLeft;
        private Timer.TimerEventHandler countdownStop;

        public HandlerByAnonymousDelegate(Timer timer, Action<string, int> startAction, Action<string> stopAction)
        {
            this.timer = timer;
            this.startAction = startAction;
            this.stopAction = stopAction;
        }

        void ICoutDownNotifier.Init()
        {
            countdownStart = delegate (object sender, EventArgs e)
            {
                Timer timer = (Timer)sender;
                startAction(timer.Name, timer.RemainSeconds);
            };

            timer.CountdownStart += countdownStart;

            secondsLeft = delegate (object sender, EventArgs e)
            {
                Timer timer = (Timer)sender;
                Informer.Show("{0} {1} seconds remaining.", timer.Name, timer.RemainSeconds);
            };

            timer.SecondsLeft += secondsLeft;

            countdownStop = delegate (object sender, EventArgs e)
            {
                Timer timer = (Timer)sender;
                stopAction(timer.Name);
            };

            timer.CountdownStop += countdownStop;
        }

        void ICoutDownNotifier.Run()
        {
            timer.Start();
        }

        void ICoutDownNotifier.Unsubscribe()
        {
            timer.CountdownStart -= countdownStart;
            timer.SecondsLeft -= secondsLeft;
            timer.CountdownStop -= countdownStop;
        }
    }
}
