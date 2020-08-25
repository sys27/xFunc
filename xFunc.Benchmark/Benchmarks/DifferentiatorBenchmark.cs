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
using xFunc.Maths.Analyzers;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;

namespace xFunc.Benchmark.Benchmarks
{
    public class DifferentiatorBenchmark
    {
        private Differentiator differentiator;

        private IExpression complexExp;

        [GlobalSetup]
        public void Setup()
        {
            differentiator = new Differentiator();

            // (abs(sin(cos(tan(ctg(x ^ 2))))) - ln(x ^ 2)) + arcsin(arccos(arctan(arcctg(x ^ 10))))
            complexExp = new Add(
                new Sub(
                    new Abs(
                        new Sin(
                            new Cos(
                                new Tan(
                                    new Cot(
                                        new Pow(Variable.X, new Number(2))
                                    )
                                )
                            )
                        )
                    ),
                    new Ln(
                        new Pow(Variable.X, new Number(2)
                        )
                    )
                ),
                new Arcsin(
                    new Arccos(
                        new Arctan(
                            new Arccot(
                                new Pow(Variable.X, new Number(10))
                            )
                        )
                    )
                )
            );
        }

        [Benchmark]
        public IExpression ComplexExpression()
            => complexExp.Analyze(differentiator, DifferentiatorContext.Default());
    }
}