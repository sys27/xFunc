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
using xFunc.Maths.Analyzers.TypeAnalyzers;
using xFunc.Maths.Expressions;

namespace xFunc.Benchmark.Benchmarks
{
    public class TypeAnalyzerBenchmark
    {
        private TypeAnalyzer analyzer;

        private IExpression gcd;
        private IExpression gcdUndefined;
        private IExpression matrix;
        private IExpression count;
        private IExpression exp;

        [GlobalSetup]
        public void Setup()
        {
            var processor = new Processor();
            analyzer = new TypeAnalyzer();

            gcd = processor.Parse("gcd(2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8, 2, 4, 6, 8)");
            gcdUndefined = processor.Parse("gcd(2, 4, 6, 8, 2, 4, 6, 8, x, 4, 6, 8, 2, 4, 6, 8)");
            matrix = processor.Parse("{{2, 2, 2}, {2, 2, 2}, {2, 2, 2}}");
            count = processor.Parse("count(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2)");
            exp = processor.Parse("count(1, 2, 3, 4, 5, 6, 7, 8, 9, 10) + (2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5 + 2 * sin(4 * cos(6 * tan(8 * cot(pi) ^ 2) ^ 3) ^ 4) ^ 5) * 10 ^ 6");
        }

        [Benchmark]
        public ResultTypes GCD()
            => gcd.Analyze(analyzer);

        [Benchmark]
        public ResultTypes GCDUndefined()
            => gcdUndefined.Analyze(analyzer);

        [Benchmark]
        public ResultTypes Matrix()
            => matrix.Analyze(analyzer);

        [Benchmark]
        public ResultTypes Count()
            => count.Analyze(analyzer);

        [Benchmark]
        public ResultTypes Exp()
            => exp.Analyze(analyzer);
    }
}