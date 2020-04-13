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
        private DisruptorQueue2 _disruptorQueue2;
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
        [GlobalSetup(Target = nameof(DisruptorQueue))]
        public void DisruptorSetup() => _disruptorQueue = new DisruptorQueue();

        [GlobalCleanup(Target = nameof(DisruptorQueue))]
        public void DisruptorCleanup() => _disruptorQueue.Stop();
        [GlobalSetup(Target = nameof(DisruptorQueue2))]
        public void DisruptorSetup2() => _disruptorQueue2 = new DisruptorQueue2();

        [GlobalCleanup(Target = nameof(DisruptorQueue2))]
        public void DisruptorCleanup2() => _disruptorQueue2.Stop();


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
        [Benchmark]
        public void DisruptorQueue() => DoManyJobs(_disruptorQueue);
        [Benchmark]
        public void DisruptorQueue2()
        {
            int jobs = 100_000; 
            var jobQueue = _disruptorQueue2;
            for (int i = 0; i < jobs - 1; i++)
            {
                jobQueue.Enqueue(null);
            }
            jobQueue.Enqueue(_autoResetEvent);
            _autoResetEvent.WaitOne();
        }

        private void DoManyJobs(IJobQueue<Action> jobQueue)
        {
            int jobs = 100_000;
            for (int i = 0; i < jobs - 1; i++)
            {
                jobQueue.Enqueue(() => { });
            }
            jobQueue.Enqueue(() => _autoResetEvent.Set());
            _autoResetEvent.WaitOne();
        }
    }
}
