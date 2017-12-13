using NUnit.Framework;
using System.Threading.Tasks;

namespace TestAsyncCode
{
    public static class AvoidDeadlockByNotUsingAwaitWhenUnneccesary
    {
        private static async Task DelayAsync() => await Task.Delay(100);

        [Test, Timeout(200)]
        // thanks to https://stackoverflow.com/questions/40343572/simulate-async-deadlock-in-a-console-application
        public static void DemonstrateDeadlock()
        {
            TestHarness.TestAsyncBehaviour(DelayAsync);
        }

        private static Task DelayTask() => Task.Delay(100);

        [Test, Timeout(200)]
        public static void DemonstrateNoDeadlock()
        {
            TestHarness.TestAsyncBehaviour(DelayTask);
        }
    }
}
