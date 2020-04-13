using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ProducerConsumerShowdown
{
    public class ChannelsQueue : IJobQueue<Action>
    {
        private readonly ChannelWriter<Action> _writer;

        public ChannelsQueue()
        {
            var channel = Channel.CreateUnbounded<Action>(new UnboundedChannelOptions() { SingleReader = true });
            var reader = channel.Reader;
            _writer = channel.Writer;

            Task.Run(async () =>
            {
                while (await reader.WaitToReadAsync())
                {
                    // Fast loop around available jobs
                    while (reader.TryRead(out var job))
                    {
                        job.Invoke();
                    }
                }
            });
        }

        public void Enqueue(Action job) => _writer.TryWrite(job);

        public void Stop() => _writer.Complete();
    }
}
