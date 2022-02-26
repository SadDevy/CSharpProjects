using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<string> s;
            FooThread(out s);
            FooTask(out s);
            FooParallelFor(out s);

            List<string> r = await FooAsync();
        }

        static async Task<List<string>> FooAsync()
        {
            List<string> result = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                Task<string> t = Task<string>.Run(() =>
                {
                    var res =
                        $"TID:{Thread.CurrentThread.ManagedThreadId:D2} IsPooled:{Thread.CurrentThread.IsThreadPoolThread}; IsBackground:{Thread.CurrentThread.IsBackground} #{i}";
                    Console.WriteLine(res);
                    Thread.Sleep(100);

                    return res;
                });

                string str = await t;
                result.Add(str);
            }

            return result;
        }

        //Не по порядку
        static void FooParallelFor(out List<string> result)
        {
            List<string> r = new List<string>();

            Parallel.For((long)0, 10, (i) =>
           {
               var res =
                   $"TID:{Thread.CurrentThread.ManagedThreadId:D2} IsPooled:{Thread.CurrentThread.IsThreadPoolThread}; IsBackground:{Thread.CurrentThread.IsBackground} #{i}";
               Console.WriteLine(res);
               Thread.Sleep(100);

               r.Add(res);
           });

            result = r;
        }

        static void FooThread(out List<string> result)
        {
            object obj = new object();

            result = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                int k = i;
                Thread thread = new Thread((r) =>
                {
                    lock (obj)
                    {
                        List<string> result = (List<string>)r;
                        var res = $"TID:{Thread.CurrentThread.ManagedThreadId:D2} IsPooled:{Thread.CurrentThread.IsThreadPoolThread}; IsBackground:{Thread.CurrentThread.IsBackground} #{k}";
                        Console.WriteLine(res);
                        Thread.Sleep(100);

                        result.Add(res);
                    }
                });

                thread.Start(result);
            }
        }

        static void FooTask(out List<string> result)
        {
            Task<List<string>> task = new Task<List<string>>(() =>
            {
                List<string> result = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    Task<string> t = new Task<string>(() =>
                    {
                        var res = $"TID:{Thread.CurrentThread.ManagedThreadId:D2} IsPooled:{Thread.CurrentThread.IsThreadPoolThread}; IsBackground:{Thread.CurrentThread.IsBackground} #{i}";
                        Console.WriteLine(res);
                        Thread.Sleep(100);

                        return res;
                    });
                    t.Start();

                    t.Wait();
                    result.Add(t.Result);
                }

                return result;
            });

            task.Start();
            task.Wait();

            result = task.Result;
        }
    }
}
