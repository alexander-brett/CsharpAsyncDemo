using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestAsyncCode
{
    class DoNotSwallowExceptions
    {
        public static async Task ThrowException()
        {
            await Task.Delay(10).ConfigureAwait(false);
            throw new Exception();
        }

        [Test]
        public static void DoesNotThrowWhenAsynchronous()
        {
            Assert.DoesNotThrow(() => ThrowException());
        }

        [Test]
        public static void HaveToWaitToGetAnException()
        {
            Assert.Throws<AggregateException>(() => ThrowException().Wait());
        }

        public static async Task ThrowExceptionBeforeAwaiting()
        {
            if ("1".Equals(1.ToString())) throw new Exception();
            await Task.Delay(10).ConfigureAwait(false);
        }

        [Test]
        public static void DoesNotThrowWhenHappensToBeAsynchronous()
        {
            Assert.DoesNotThrow(() => ThrowExceptionBeforeAwaiting());
        }

        public static Task ThrowExceptionSynchronouslyReturnTask()
        {
            if ("1".Equals(1.ToString())) throw new Exception();
            return Task.Delay(10);
        }

        [Test]
        public static void ThrowsExceptionWhenHappensToBeSynchronous()
        {
            Assert.Throws<Exception>(() => ThrowExceptionSynchronouslyReturnTask());
        }
    }
}
