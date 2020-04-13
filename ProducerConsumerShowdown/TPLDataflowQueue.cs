using System;
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

        public void Enqueue(Action job) => _jobs.Post(job);

        public void Stop() => _jobs.Complete();
    }
}
