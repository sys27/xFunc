// Copyright 2012-2021 Dmytro Kyshchenko
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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;

namespace xFunc.Benchmark.Benchmarks
{
    public class SimplifierBenchmark
    {
        private Simplifier simplifier;

        private IExpression exp;

        [GlobalSetup]
        public void Setup()
        {
            simplifier = new Simplifier();

            var processor = new Processor();

            exp = processor.Parse("0 + x + x + 0 + 1 + 2 + 3 + x + (2 * x) + (3 * x) + (x * 4) - 0 - x - 0 - 1 - 2 - 3 - (2 * x) - (x * 3) + (x * 0) - (0 * x) + (1 * x) - (x * 1) * (x * x) * (2 * x) * (x * 3) + (x ^ 0) + (x ^ 0) + (e ^ ln(1)) + cos(arccos(0)) + (x * 0) + tan(arctan(0)) + sin(arcsin(x)) - (0 * x)");
        }

        [Benchmark]
        public IExpression Simplify()
            => exp.Analyze(simplifier);
    }
}