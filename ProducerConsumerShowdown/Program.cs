using BenchmarkDotNet.Running;
using System;

namespace ProducerConsumerShowdown
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ManyJobsBenchmark>();
            Console.WriteLine(summary);
        }
    }
}
