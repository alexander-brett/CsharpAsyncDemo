using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestAsyncCode
{
    // reference: https://blogs.msdn.microsoft.com/pfxteam/2012/01/20/await-synchronizationcontext-and-console-apps/
    class AwaitVersusContinueWith
    {
        private static Task Method1()
        {
            Console.WriteLine("Method1");
            return Task.Delay(10);
        }

        private static Task Method2()
        {
            Console.WriteLine("Method2");
            return Task.Delay(10);
        }

        [Test]
        public static async Task DemonstrateAwait()
        {
            await Method1();
            await Method2();
        }

        [Test]
        public static async Task DemonstrateContinueWith()
        {
            await Method1().ContinueWith(task => Method2());
        }
    }
}
