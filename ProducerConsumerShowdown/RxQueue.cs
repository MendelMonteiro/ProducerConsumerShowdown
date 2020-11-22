using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;

namespace ProducerConsumerShowdown
{
    public class RxQueue : IJobQueue<Action>
    {
        readonly Subject<Action> _jobs = new Subject<Action>();

        public RxQueue()
        {
            _jobs.ObserveOn(Scheduler.Default)
                .Subscribe(job => { job.Invoke(); });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enqueue(Action job) => _jobs.OnNext(job);

        public void Stop() => _jobs.Dispose();
    }
}
