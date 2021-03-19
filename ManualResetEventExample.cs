using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TplConsole
{
    public class ManualResetEventExample
    {
        private static int pizzaRequests = 5;
        private static bool stop = false;
        static ManualResetEvent[] handles = { new ManualResetEvent(false), new ManualResetEvent(false) };
        public static void Run()
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("ManualResetEventExample Started...");
            Console.WriteLine("----------------------");

            Console.WriteLine("Starting Manual Reset Event example...");
            Thread t1 = new Thread(() => BakePizza()) { Name = "1.Baker" };
            Thread t2 = new Thread(() => AddSauce()) { Name = "2.SauceGuy" };
            Thread t3 = new Thread(() => Deliver()) { Name = "3.DeliveryBoy" };

            t1.Start();
            t2.Start();
            t3.Start();

            Console.WriteLine("Press enter to stop the tasks");
            Console.ReadLine();
            stop = true;

            t1.Join();
            t2.Join();
            t3.Join();
        }

        private static void Log(string msg)
        {
            Console.WriteLine(string.Format("Actor:{0}. Msg:{1}.", Thread.CurrentThread.Name, msg));
        }
        private static void Deliver()
        {
            while (!stop)
            {
                handles[1].WaitOne(10000);
                Log("Pizza has been Delivered!");
                handles[1].Reset();
            }
        }

        private static void AddSauce()
        {
            while (!stop)
            {
                handles[0].WaitOne(10000);
                Log("Sauce added!");
                handles[1].Set();
                handles[0].Reset();
            }
        }

        private static void BakePizza()
        {
            while (pizzaRequests != 0)
            {
                Thread.Sleep(3000);
                Log(string.Format("Pizza {0} Baked!", pizzaRequests));
                handles[0].Set();
                --pizzaRequests;
            }
            stop = true;
        }
    }
}
