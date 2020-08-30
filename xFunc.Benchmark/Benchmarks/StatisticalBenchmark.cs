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

using BenchmarkDotNet.Attributes;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Benchmark.Benchmarks
{
    public class StatisticalBenchmark
    {
        private IExpression stdev;
        private IExpression stdevp;

        private IExpression var;
        private IExpression varp;

        [GlobalSetup]
        public void Setup()
        {
            var processor = new Processor();

            stdev = processor.Parse("stdev(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
            stdevp = processor.Parse("stdevp(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");

            var = processor.Parse("var(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
            varp = processor.Parse("varp(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
        }

        [Benchmark]
        public object StdevExecute() => stdev.Execute();

        [Benchmark]
        public object StdevpExecute() => stdevp.Execute();

        [Benchmark]
        public object VarExecute() => var.Execute();

        [Benchmark]
        public object VarpExecute() => varp.Execute();
    }
}