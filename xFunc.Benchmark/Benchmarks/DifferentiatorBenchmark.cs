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
    public class DifferentiatorBenchmark
    {
        private Differentiator differentiator;
        private DifferentiatorContext context;

        private IExpression complexExp;

        [GlobalSetup]
        public void Setup()
        {
            differentiator = new Differentiator();
            context = DifferentiatorContext.Default();

            var processor = new Processor();

            complexExp = processor.Parse("(2 * abs(3 * sin(4 * cos(5 * tan(6 * ctg(x ^ 2))))) - ln(x ^ 2)) + arcsin(arccos(arctan(arcctg(x ^ 10))))");
        }

        [Benchmark]
        public IExpression ComplexExpression()
            => complexExp.Analyze(differentiator, context);
    }
}