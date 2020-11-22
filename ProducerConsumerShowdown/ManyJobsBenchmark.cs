using BenchmarkDotNet.Attributes;
using System;
using System.Threading;

namespace ProducerConsumerShowdown
{
    [MemoryDiagnoser]
    public class ManyJobsBenchmark
    {
        private const int _jobSize = 1_000_000;
        private readonly AutoResetEvent _autoResetEvent;

        private DisruptorQueue _disruptorQueue;
        private DisruptorQueueNoDelegate _disruptorQueueNoDelegate;
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
        [GlobalSetup(Target = nameof(DisruptorQueueNoDelegate))]
        public void DisruptorSetupNoDelegate() => _disruptorQueueNoDelegate = new DisruptorQueueNoDelegate();

        [GlobalCleanup(Target = nameof(DisruptorQueueNoDelegate))]
        public void DisruptorCleanupNoDelegate() => _disruptorQueueNoDelegate.Stop();

        [GlobalSetup(Target = nameof(DisruptorQueueNoDelegateBatched))]
        public void DisruptorSetupNoDelegateBatched() => _disruptorQueueNoDelegate = new DisruptorQueueNoDelegate();

        [GlobalCleanup(Target = nameof(DisruptorQueueNoDelegateBatched))]
        public void DisruptorCleanupNoDelegateBatched() => _disruptorQueueNoDelegate.Stop();


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
        public void DisruptorQueueNoDelegate()
        {
            var jobQueue = _disruptorQueueNoDelegate;
            for (int i = 0; i < _jobSize - 1; i++)
            {
                jobQueue.Enqueue(null);
            }
            jobQueue.Enqueue(_autoResetEvent);
            _autoResetEvent.WaitOne();
        }

        [Benchmark]
        public void DisruptorQueueNoDelegateBatched()
        {
            var jobQueue = _disruptorQueueNoDelegate;
            var batchSize = 10;
            for (int i = 0; i < (_jobSize / batchSize) - 1; i++)
            {
                jobQueue.EnqueueBatch(null, batchSize);
            }
            jobQueue.Enqueue(_autoResetEvent);
            _autoResetEvent.WaitOne();
        }

        private void DoManyJobs(IJobQueue<Action> jobQueue)
        {
            for (int i = 0; i < _jobSize - 1; i++)
            {
                jobQueue.Enqueue(() => { });
            }
            jobQueue.Enqueue(() => _autoResetEvent.Set());
            _autoResetEvent.WaitOne();
        }
    }
}
