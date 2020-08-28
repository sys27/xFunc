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
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Statistical;

namespace xFunc.Benchmark.Benchmarks
{
    public class TypeAnalyzerBenchmark
    {
        private Processor processor;
        private TypeAnalyzer analyzer;

        private GCD gcd;
        private GCD gcdUndefined;

        private Matrix matrix;

        private Count count;

        private IExpression exp;

        [GlobalSetup]
        public void Setup()
        {
            processor = new Processor();
            analyzer = new TypeAnalyzer();

            gcd = new GCD(new IExpression[]
            {
                new Number(2), new Number(4), new Number(6), new Number(8),
                new Number(2), new Number(4), new Number(6), new Number(8),
                new Number(2), new Number(4), new Number(6), new Number(8),
                new Number(2), new Number(4), new Number(6), new Number(8),
            });
            gcdUndefined = new GCD(new IExpression[]
            {
                new Number(2), new Number(4), new Number(6), new Number(8),
                new Number(2), new Number(4), new Number(6), new Number(8),
                Variable.X, new Number(4), new Number(6), new Number(8),
                new Number(2), new Number(4), new Number(6), new Number(8),
            });

            matrix = new Matrix(new[]
            {
                new Vector(new IExpression[] { new Number(2), new Number(2), new Number(2), }),
                new Vector(new IExpression[] { new Number(2), new Number(2), new Number(2), }),
                new Vector(new IExpression[] { new Number(2), new Number(2), new Number(2), }),
            });

            count = new Count(new IExpression[]
            {
                new Number(2), new Number(2), new Number(2), new Number(2),
                new Number(2), new Number(2), new Number(2), new Number(2),
                new Number(2), new Number(2), new Number(2), new Number(2),
                new Number(2), new Number(2), new Number(2), new Number(2),
            });

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