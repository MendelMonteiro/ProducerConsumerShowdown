using System;

namespace ProducerConsumerShowdown
{
    public interface IJobQueue<T>
    {
        void Enqueue(Action p);
        void Stop();
    }
}
