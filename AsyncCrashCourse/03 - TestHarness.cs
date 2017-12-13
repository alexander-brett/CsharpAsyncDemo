using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestAsyncCode
{
    public static class TestHarness
    {
        private static async Task DelayAsync() => await Task.Delay(100);

        [Test, Timeout(200)]
        public static void ThisWorksFineHonest()
        {
            DelayAsync().Wait();
        }

        // thanks to https://stackoverflow.com/questions/40343572/simulate-async-deadlock-in-a-console-application
        [Test, Timeout(200)]
        public static void DemonstrateFailure()
        {
            new DedicatedThreadSynchronisationContext().Send(state =>
            {
                DelayAsync().Wait();
            }, null);
        }
        
        public static void TestAsyncBehaviour(Func<Task> test)
        {
            new DedicatedThreadSynchronisationContext().Send(state =>
            {
                test().Wait();
            }, null);
        }
    }
}
