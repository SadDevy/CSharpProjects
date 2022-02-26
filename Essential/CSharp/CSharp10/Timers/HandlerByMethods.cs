using System;

namespace Timers
{
    public class HandlerByMethods : ICoutDownNotifier
    {
        private Timer timer;

        private Action<string, int> startAction;
        private Action<string> stopAction;

        public HandlerByMethods(Timer timer, Action<string, int> startAction, Action<string> stopAction)
        {
            this.timer = timer;
            this.startAction = startAction;
            this.stopAction = stopAction;
        }

        void ICoutDownNotifier.Init()
        {
            timer.CountdownStart += OnCountdownStart;
            timer.SecondsLeft += OnSecondsLeft;
            timer.CountdownStop += OnCountdownStop;
        }

        void ICoutDownNotifier.Run()
        {
            timer.Start();
        }

        void ICoutDownNotifier.Unsubscribe()
        {
            timer.CountdownStart -= OnCountdownStart;
            timer.SecondsLeft -= OnSecondsLeft;
            timer.CountdownStop -= OnCountdownStop;
        }

        private void OnCountdownStart(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender; 
            startAction(timer.Name, timer.RemainSeconds);
        }

        private void OnSecondsLeft(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            Informer.Show("{0} {1} seconds remaining.", timer.Name, timer.RemainSeconds);
        }

        private void OnCountdownStop(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            stopAction(((Timer)sender).Name);
        }
    }
}
