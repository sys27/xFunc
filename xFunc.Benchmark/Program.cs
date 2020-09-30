// Copyright 2012-2020 Dmytro Kyshchenko
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;

namespace xFunc.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
                args = new[] { "--filter", "*" };

            BenchmarkSwitcher
                .FromAssembly(typeof(Program).Assembly)
                .Run(args,
                    ManualConfig.Create(DefaultConfig.Instance)
                        .AddJob(Job.MediumRun
                            .WithToolchain(CsProjCoreToolchain.NetCoreApp31))
                        .AddDiagnoser(MemoryDiagnoser.Default)
                        .StopOnFirstError());
        }
    }
}