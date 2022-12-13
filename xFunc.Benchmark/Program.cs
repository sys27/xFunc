// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;

namespace xFunc.Benchmark;

public class Program
{
    public static void Main(string[] args)
    {
        if (args is null || args.Length == 0)
            args = new[] { "--filter", "*" };

        BenchmarkSwitcher
            .FromAssembly(typeof(Program).Assembly)
            .Run(args,
                ManualConfig.Create(DefaultConfig.Instance)
                    .AddJob(Job.MediumRun
                        .WithToolchain(CsProjCoreToolchain.NetCoreApp70))
                    .AddDiagnoser(MemoryDiagnoser.Default)
                    .StopOnFirstError());
    }
}