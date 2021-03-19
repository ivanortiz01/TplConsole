using System;
using System.Threading.Tasks;

namespace TplConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await BasicTaskExample.Run();

            CancellationTokenExample.Run();

            ManualResetEventExample.Run();

            InterlockedExample.Run();
        }
    }
}
