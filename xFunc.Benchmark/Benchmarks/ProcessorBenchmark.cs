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
using xFunc.Maths.Results;

namespace xFunc.Benchmark.Benchmarks
{
    public class ProcessorBenchmark
    {
        private Processor processor;

        [GlobalSetup]
        public void Setup()
        {
            processor = new Processor();
        }

        [Benchmark]
        public IExpression Parse()
            => processor.Parse("(100.1 + 2(3sin(4cos(5tan(6ctg(10x)))) * 3) / (func(a, b, c) ^ 2)) - (cos(y) - 111.3) & (true | false -> true <-> false eq true) + (det({{1, 2}, {3, 4}}) * 10log(2, 3)) + re(3 + 2i) - im(2 - 9i) + (9 + 2i)");

        [Benchmark]
        public NumberResult Solve()
            => processor.Solve<NumberResult>("count(1, 2, 3, 4, 5, 6, 7, 8, 9, 10) + (2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5) * 10 ^ 6 + 10!");
    }
}