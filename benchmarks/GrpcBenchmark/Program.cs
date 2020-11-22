using BenchmarkDotNet.Running;
using System;

namespace GrpcBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            BenchmarkRunner.Run<GrpcClient>();

            //var test = new GrpcClient();
            //test.Setup();
            //test.SayHello().GetAwaiter().GetResult();
        }
    }
}
