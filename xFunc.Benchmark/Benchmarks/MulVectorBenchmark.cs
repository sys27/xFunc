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

using System;
using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Benchmark.Benchmarks
{
    public class MulVectorBenchmark
    {
        private readonly Random random = new Random();

        private Mul mul;

        [Params(2, 10, 100)]
        public int Size;

        [GlobalSetup]
        public void Setup()
        {
            var left = CreateVector();
            var right = CreateVector();

            mul = new Mul(left, right);
        }

        private Vector CreateVector()
        {
            var vector = ImmutableArray.CreateBuilder<IExpression>(Size);
            for (var j = 0; j < Size; j++)
                vector.Add(new Number(random.Next()));

            return new Vector(vector.ToImmutableArray());
        }

        [Benchmark]
        public object MulVector() => mul.Execute();
    }
}