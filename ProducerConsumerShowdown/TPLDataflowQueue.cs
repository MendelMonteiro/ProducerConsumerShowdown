using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

namespace ProducerConsumerShowdown
{
    public class TPLDataflowQueue : IJobQueue<Action>
    {
        private readonly ActionBlock<Action> _jobs;

        public TPLDataflowQueue()
        {
            _jobs = new ActionBlock<Action>((job) =>
            {
                job.Invoke();
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(Action job) => _jobs.Post(job);

        public void Stop() => _jobs.Complete();
    }
}
