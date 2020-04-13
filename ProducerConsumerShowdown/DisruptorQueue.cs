using Disruptor;
using Disruptor.Dsl;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerShowdown
{
    class DisruptorQueue : IJobQueue<Action>
    {
        private readonly Disruptor<Event> _disruptor;

        public DisruptorQueue()
        {
            _disruptor = new Disruptor<Event>(() => new Event(), 256, TaskScheduler.Default, ProducerType.Single, new BusySpinWaitStrategy());
            _disruptor.HandleEventsWith(new EntryHandler());
            _disruptor.Start();
        }

        public void Enqueue(Action action)
        {
            var seq = _disruptor.RingBuffer.Next();

            var entry = _disruptor.RingBuffer[seq];
            entry.Action = action;

            _disruptor.RingBuffer.Publish(seq);
        }

        public void Stop() => _disruptor.Shutdown();

        internal class Event
        {
            internal Action Action;
        }

        class EntryHandler : IEventHandler<Event>
        {
            public void OnEvent(Event data, long sequence, bool endOfBatch) => data.Action.Invoke();
        }
    }

    class DisruptorQueue2 
    {
        private readonly Disruptor<Event> _disruptor;

        public DisruptorQueue2()
        {
            _disruptor = new Disruptor<Event>(() => new Event(), 256, TaskScheduler.Default, ProducerType.Single, new BusySpinWaitStrategy());
            _disruptor.HandleEventsWith(new EntryHandler());
            _disruptor.Start();
        }

        public void Enqueue(AutoResetEvent @event)
        {
            var seq = _disruptor.RingBuffer.Next();

            var entry = _disruptor.RingBuffer[seq];
            entry.AutoResetEvent = @event;

            _disruptor.RingBuffer.Publish(seq);
        }

        public void Stop() => _disruptor.Shutdown();

        internal class Event
        {
            internal AutoResetEvent AutoResetEvent;
        }

        class EntryHandler : IEventHandler<Event>
        {
            public void OnEvent(Event data, long sequence, bool endOfBatch) => data.AutoResetEvent?.Set();
        }
    }
}
