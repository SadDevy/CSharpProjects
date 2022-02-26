using System;
using Timers;

namespace TimersUI
{
    class Program
    {
        static void Main()
        {
            Action<string, int> startAction = OnStartAction;
            Action<string> stopAction = OnStopAction;

            Timer firstTimer = new Timer("First", 1);
            Timer secondTimer = new Timer("Second", 2);
            Timer thirdTimer = new Timer("Third", 5);

            ICoutDownNotifier[] handlers = new ICoutDownNotifier[]
            {
                new HandlerByMethods(firstTimer, startAction, stopAction),
                new HandlerByAnonymousDelegate(secondTimer, startAction, stopAction),
                new HandlerByLambdaExpression(thirdTimer, startAction, stopAction)
            };

            foreach (ICoutDownNotifier handler in handlers)
            {
                handler.Init();
            }

            Array.ForEach(handlers, e => e.Run());
            Array.ForEach(handlers, Unsubscribe);
        }

        private static void Unsubscribe(ICoutDownNotifier handler)
        {
            handler.Unsubscribe();
        }

        private static void OnStartAction(string taskName, int timeAllotted)
        {
            Informer.Show("{0} has started for {1} seconds.", taskName, timeAllotted);
        }
        
        private static void OnStopAction(string taskName)
        {
            Informer.Show("{0} has finished.", taskName);
        }
    }
}
