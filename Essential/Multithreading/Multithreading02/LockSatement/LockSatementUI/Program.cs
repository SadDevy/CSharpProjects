using System.Collections.Generic;
using System.Threading;

namespace LockSatementUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Locker locker = new Locker(new ConsoleViewer());

            SetupAndStartThreads(locker.LockByGetType);
            
            SetupAndStartThreads(locker.LockByTypeOf);
            
            SetupAndStartThreads(locker.LockByStringLiteral);

            SetupAndStartThreads(locker.LockByTypeOfObject);

            SetupAndStartThreads(locker.LockThis);

            object obj = new object();
            ParametrizedSetupAndStartThreads(obj, locker.LockByObjectInstance);

            A a = new A();
            ParametrizedSetupAndStartThreads(a, locker.LockByUserClass);

            List<int> ints = new List<int>();
            ParametrizedSetupAndStartThreads(ints, locker.LockByIntList);

            const int intsCount = 10;
            int[] intsArray = new int[intsCount];
            ParametrizedSetupAndStartThreads(intsArray, locker.LockByIntArray);
        }

        private static void SetupAndStartThreads(ThreadStart threadStart)
        {
            Thread a = new Thread(threadStart);
            Thread b = new Thread(threadStart);

            a.Start();
            b.Start();

            a.Join();
            b.Join();
        }

        private static void ParametrizedSetupAndStartThreads(object locker, ParameterizedThreadStart threadStart)
        {
            Thread a = new Thread(threadStart);
            Thread b = new Thread(threadStart);

            a.Start(locker);
            b.Start(locker);

            a.Join();
            b.Join();
        }
    }
}
