using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TplConsole
{
    public class CancellationTokenExample
    {
        private static int counter = 0;
        public static void Run()
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("CancellationTokenExample Started...");
            Console.WriteLine("----------------------");

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task task = Task.Run(() => Process(cancellationToken));

            Console.WriteLine("Press enter to stop the task");
            Console.ReadLine();
            cancellationTokenSource.Cancel();
            Console.WriteLine($"Task Executed {counter} times");
        }

        private static void Process(CancellationToken cancellationToken) {
            while(true) {
                if(cancellationToken.IsCancellationRequested) {
                    return;
                }

                counter++;

                Console.Write($"{counter}!");
                Thread.Sleep(500);
            }
        }
    }
}
