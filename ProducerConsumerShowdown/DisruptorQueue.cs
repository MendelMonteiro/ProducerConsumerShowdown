using Disruptor;
using Disruptor.Dsl;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace ProducerConsumerShowdown
{
    class DisruptorQueue : IJobQueue<Action>
    {
        private readonly Disruptor<Entry> _disruptor;

        public DisruptorQueue()
        {
            _disruptor = new Disruptor<Entry>(() => new Entry(), 256, TaskScheduler.Default, ProducerType.Multi, new BusySpinWaitStrategy());
            _disruptor.HandleEventsWith(new EntryHandler());
            _disruptor.Start();
        }

        public void Enqueue(Action p)
        {
            var seq = _disruptor.RingBuffer.Next();

            var entry = _disruptor.RingBuffer[seq];
            entry.Action = p;

            _disruptor.RingBuffer.Publish(seq);
        }

        public void Stop() => _disruptor.Shutdown();

        internal class Entry
        {
            internal Action Action;
        }

        class EntryHandler : IEventHandler<Entry>
        {
            public void OnEvent(Entry data, long sequence, bool endOfBatch) => data.Action.Invoke();
        }
    }
}
