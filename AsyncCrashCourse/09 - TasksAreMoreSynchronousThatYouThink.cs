using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestAsyncCode
{
    class TasksAreMoreSynchronousThatYouThink
    {
        // this is called the 'fast path'
        // https://blogs.msdn.microsoft.com/lucian/2011/04/15/async-ctp-refresh-design-changes/
        
        public static Task DoSomeThings()
        {

            var tcs = new TaskCompletionSource<object>(TaskContinuationOptions.RunContinuationsAsynchronously)
            Console.WriteLine("1");
            Console.WriteLine("2");
            Console.WriteLine("3");
            Console.WriteLine("4");
            Console.WriteLine("5");
            return Task.CompletedTask;
        }

        [Test]
        public static void DemonstrateSynchronousExecution()
        {
            DoSomeThings(); // nb! not awaited
            DoSomeThings();
        }


        public static async Task DoSomeThingsWithTheAwaitKeyword()
        {
            Console.WriteLine("1");
            Console.WriteLine("2");
            Console.WriteLine("3");
            Console.WriteLine("4");
            Console.WriteLine("5");
            await Task.CompletedTask;
        }

        [Test]
        public static void DemonstrateSynchronousExecutionWithAsyncKeyword()
        {
            DoSomeThingsWithTheAwaitKeyword(); // nb! not awaited - and this time it's a warning
            DoSomeThingsWithTheAwaitKeyword();
        }

        public static async Task DoSomethingButAwaitHalfwayThrough()
        {
            Console.WriteLine("1");
            Console.WriteLine("2");
            Console.WriteLine("3");
            await Task.CompletedTask;
            Console.WriteLine("4");
            Console.WriteLine("5");
        }

        [Test]
        public static async Task DemonstrateSynchronousExecutionWithAwaitHalfwayThrough()
        {
            var task1 = DoSomethingButAwaitHalfwayThrough();
            var task2 = DoSomethingButAwaitHalfwayThrough();

            await Task.WhenAll(task1, task2);
        }

        [Test]
        public static void SynchronousExecutionWillNotCauseAProblemInSta()
        {
            TestHarness.TestAsyncBehaviour(DemonstrateSynchronousExecutionWithAwaitHalfwayThrough);
        }

        public static async Task DoSomethingButActuallyPause()
        {
            Console.WriteLine("1");
            Console.WriteLine("2");
            Console.WriteLine("3");
            await Task.Delay(50).ConfigureAwait(false);
            Console.WriteLine("4");
            Console.WriteLine("5");
        }

        [Test]
        public static async Task DemonstrateASynchronousExecutionWithYield()
        {
            var task1 = DoSomethingButActuallyPause();
            var task2 = DoSomethingButActuallyPause();

            await Task.WhenAll(task1, task2);
        }
    }
}
