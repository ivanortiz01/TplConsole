using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TplConsole
{
    internal class InterlockedExample
    {
        static int iterationsPerThread = 1000000;
        static int counter = 0;
        static object mutexLock = new object();
        internal static void Run()
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("InterlockedExample Started...");
            Console.WriteLine("----------------------");

            Console.WriteLine("Starting Interlocked Example...");

            Console.WriteLine("Using Interlocked Sync...");

            Stopwatch sw = new Stopwatch();
            sw.Start();

            InterlockedSync();
            
            sw.Stop();

            Console.WriteLine(string.Format("Interlocked Sync took:{0}", sw.Elapsed));

            Thread.Sleep(2000);

            sw.Reset();

            Console.WriteLine("Using Monitor Sync...");

            sw.Start();

            MonitorSync();

            sw.Stop();

            Console.WriteLine(string.Format("Monitor Sync took:{0}", sw.Elapsed));

            Console.ReadKey();

        }
        private static void Log(string msg)
        {
            Console.WriteLine(string.Format("Actor:{0}. Msg:{1}.", Thread.CurrentThread.Name, msg));
        }

        private static void InterlockedSync()
        {
            Parallel.For(0, Environment.ProcessorCount, x =>
            {
                for (int i = 0; i < iterationsPerThread; ++i)
                    Increment();
            });
            int missed = (iterationsPerThread * Environment.ProcessorCount) - counter;
            Console.WriteLine(string.Format("Final Count: {0}. We lost {1} values.", counter, missed));

        }
        private static void MonitorSync()
        {
            counter = 0;
            Parallel.For(0, Environment.ProcessorCount, x =>
            {
                for (int i = 0; i < iterationsPerThread; ++i)
                    IncrementWithLock();
            });
            int missed = (iterationsPerThread * Environment.ProcessorCount) - counter;
            Console.WriteLine(string.Format("Final Count: {0}. We lost {1} values.", counter, missed));

        }
        private static void Increment()
        {
            Interlocked.Increment(ref counter);
        }
        private static void IncrementWithLock()
        {
            lock(mutexLock)
                ++counter;
        }
    }
}