using NUnit.Framework;
using System.Threading.Tasks;

namespace TestAsyncCode
{
    class RemoveDeadlockByUsingContinueWith
    {
        private static async Task<object> DelayAsync()
        {
            await Task.Delay(100);
            return new object();
        }

        [Test, Timeout(200)]
        public static void DemonstrateDeadlock()
        {
            TestHarness.TestAsyncBehaviour(DelayAsync);
        }

        private static Task<object> DelayContinuation()
            => Task.Delay(100).ContinueWith(task => new object());

        [Test, Timeout(200)]
        public static void DemonstrateNoDeadlock()
        {
            TestHarness.TestAsyncBehaviour(DelayContinuation);
        }

    }
}
