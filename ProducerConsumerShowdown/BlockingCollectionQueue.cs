using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ProducerConsumerShowdown
{
    public class BlockingCollectionQueue : IJobQueue<Action>
    {
        private readonly BlockingCollection<Action> _jobs = new BlockingCollection<Action>();

        public BlockingCollectionQueue()
        {
            var thread = new Thread(new ThreadStart(OnStart));
            thread.IsBackground = true;
            thread.Start();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(Action job) => _jobs.Add(job);

        private void OnStart()
        {
            foreach (var job in _jobs.GetConsumingEnumerable(CancellationToken.None))
            {
                job.Invoke();
            }
        }

        public void Stop() => _jobs.CompleteAdding();
    }
}
