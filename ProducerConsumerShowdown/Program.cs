using BenchmarkDotNet.Running;
using Disruptor;
using System;
using System.Diagnostics;
using System.Threading;

namespace ProducerConsumerShowdown
{
    class Program
    {
        private const int _jobSize = 1_000_000;

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ManyJobsBenchmark>();
            Console.WriteLine(summary);

            //RunDisruptorTest();
        }

        private static void RunDisruptorTest()
        {
            var resetEvent = new AutoResetEvent(false);

            var jobQueue = new DisruptorQueueNoDelegate();

            RunTest(resetEvent, jobQueue);

            var watch = Stopwatch.StartNew();

            var backPressureCount = RunTest(resetEvent, jobQueue);

            watch.Stop();

            Console.WriteLine($"Could not enqueue {backPressureCount:N0} items.  Took {watch.ElapsedMilliseconds:N0}ms - {watch.ElapsedTicks / (double)_jobSize * 100:N0}ns per item");
        }

        private static int RunTest(AutoResetEvent resetEvent, DisruptorQueueNoDelegate jobQueue)
        {
            int backPressureCount = 0;
            int i = 0;
            while (i < _jobSize - 1)
            {
                //if (!jobQueue.TryEnqueue(null))
                //{
                //    backPressureCount++;
                //    continue;
                //}
                jobQueue.Enqueue(null);
                i++;
            }

            jobQueue.Enqueue(resetEvent);
            resetEvent.WaitOne();

            //WaitForAllEntriesToBeProcssed(jobQueue);
            return backPressureCount;
        }

        private static void WaitForAllEntriesToBeProcssed(DisruptorQueueNoDelegate jobQueue)
        {
            var spinWait = new SpinWait();
            while (true)
            {
                if (jobQueue.ProcessedCount < _jobSize)
                    spinWait.SpinOnce();
                else
                    break;
            }
        }
    }
}
