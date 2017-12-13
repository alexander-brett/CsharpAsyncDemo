using NUnit.Framework;
using System.Threading.Tasks;

namespace TestAsyncCode
{
    class AvoidDeadlockByUsingConfigureAwait
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

        private static async Task<object> DelayWithConfigureAwait()
        {
            await Task.Delay(100).ConfigureAwait(false);
            return new object();
        }

        [Test, Timeout(200)]
        public static void DemonstrateNoDeadlock()
        {
            TestHarness.TestAsyncBehaviour(DelayWithConfigureAwait);
        }
    }
}