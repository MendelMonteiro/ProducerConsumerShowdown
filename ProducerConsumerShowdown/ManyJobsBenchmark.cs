using BenchmarkDotNet.Attributes;
using System;
using System.Threading;

namespace ProducerConsumerShowdown
{
    [MemoryDiagnoser]
    public class ManyJobsBenchmark
    {
        private readonly AutoResetEvent _autoResetEvent;
        private DisruptorQueue _disruptorQueue;
        private BlockingCollectionQueue _blockingCollectionQueue;
        private NoDedicatedThreadQueue _noThreadQueue;
        private RxQueue _rxQueue;
        private ChannelsQueue _channelsQueue;
        private TPLDataflowQueue _dataFlowQueue;

        public ManyJobsBenchmark() => _autoResetEvent = new AutoResetEvent(false);

        [GlobalSetup(Target = nameof(BlockingCollectionQueue))]
        public void Setup() => _blockingCollectionQueue = new BlockingCollectionQueue();

        [GlobalCleanup(Target = nameof(BlockingCollectionQueue))]
        public void Cleanup() => _blockingCollectionQueue.Stop();

        [GlobalSetup(Target = nameof(NoDedicatedThreadQueue))]
        public void SetupNoThread() => _noThreadQueue = new NoDedicatedThreadQueue();

        [GlobalCleanup(Target = nameof(NoDedicatedThreadQueue))]
        public void CleanupNoThread() => _noThreadQueue.Stop();

        [GlobalSetup(Target = nameof(RxQueue))]
        public void SetupRx() => _rxQueue = new RxQueue();

        [GlobalCleanup(Target = nameof(RxQueue))]
        public void CleanupRx() => _rxQueue.Stop();

        [GlobalSetup(Target = nameof(ChannelsQueue))]
        public void SetupChannels() => _channelsQueue = new ChannelsQueue();

        [GlobalCleanup(Target = nameof(ChannelsQueue))]
        public void CleanupChannels() => _channelsQueue.Stop();

        [GlobalSetup(Target = nameof(TPLDataflowQueue))]
        public void SetupDataFlow() => _dataFlowQueue = new TPLDataflowQueue();

        [GlobalCleanup(Target = nameof(TPLDataflowQueue))]
        public void CleanupDataFlow() => _dataFlowQueue.Stop();

        [Benchmark]
        public void BlockingCollectionQueue() => DoManyJobs(_blockingCollectionQueue);
        [Benchmark]
        public void NoDedicatedThreadQueue() => DoManyJobs(_noThreadQueue);
        [Benchmark]
        public void RxQueue() => DoManyJobs(_rxQueue);
        [Benchmark]
        public void ChannelsQueue() => DoManyJobs(_channelsQueue);
        [Benchmark]
        public void TPLDataflowQueue() => DoManyJobs(_dataFlowQueue);

        [GlobalSetup(Target = nameof(DisruptorQueue))]
        public void DisruptorSetup()
        {
            _disruptorQueue = new DisruptorQueue();
            Console.WriteLine("Setting up disruptor");
        }

        [GlobalCleanup(Target = nameof(DisruptorQueue))]
        public void DisruptorCleanup()
        {
            Console.WriteLine("Stopping up disruptor");
            _disruptorQueue?.Stop();
        }

        [Benchmark]
        public void DisruptorQueue() => DoManyJobs(_disruptorQueue);

        private void DoManyJobs(IJobQueue<Action> jobQueue)
        {
            Action p = () => { };
            int jobs = 100_000;
            for (int i = 0; i < jobs - 1; i++)
            {
                jobQueue.Enqueue(p);
            }
            jobQueue.Enqueue(() => _autoResetEvent.Set());
            _autoResetEvent.WaitOne();
        }
    }
}
