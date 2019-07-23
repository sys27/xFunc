using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;

namespace xFunc.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner
                .Run(typeof(Program).Assembly,
                     ManualConfig.Create(DefaultConfig.Instance)
                                 .With(Job.MediumRun
                                          .WithLaunchCount(1)
                                          .With(CsProjCoreToolchain.NetCoreApp22))
                                 .With(MemoryDiagnoser.Default)
                                 .StopOnFirstError()
                );
        }
    }
}
