using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TplConsole
{
    public class BasicTaskExample {

        public static async Task Run() 
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("BasicTaskExample Started...");
            Console.WriteLine("----------------------");

            await TaskExample();
        }

        public static async Task TaskExample()
        {
            var task = new Task(() =>
                        {
                            Thread.Sleep(1000);
                            Console.WriteLine("Task 1");
                        });
            task.Start();

            var task2 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task 2");
            });
            task2.Start();

            Console.WriteLine("Hago algo mas");

            await task;
            await task2;

            int number = await Random(5);

            Console.WriteLine($"Random number: {number}");

            Console.WriteLine("Fin");
        }

        public static async Task<int> Random(int start)
        {
            var task = new Task<int>(() => new Random().Next(start, 1000));
            task.Start();
            int result = await task;
            return result;
        }
    }
}