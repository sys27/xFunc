// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Matrices;

namespace xFunc.Benchmark.Benchmarks
{
    public class TransposeBenchmark
    {
        private readonly Random random = new Random();

        private Transpose transpose;

        [ParamsSource(nameof(GetSizes))]
        public (int rows, int columns) Size;

        public IEnumerable<(int rows, int columns)> GetSizes()
        {
            yield return (2, 3);
            yield return (8, 10);
        }

        [GlobalSetup]
        public void Setup()
        {
            var matrix = CreateMatrix();

            transpose = new Transpose(matrix);
        }

        private Matrix CreateMatrix()
        {
            var vectors = ImmutableArray.CreateBuilder<Vector>(Size.rows);
            for (var i = 0; i < Size.rows; i++)
            {
                var vector = ImmutableArray.CreateBuilder<IExpression>(Size.columns);
                for (var j = 0; j < Size.columns; j++)
                    vector.Add(new Number(random.Next()));

                vectors.Add(new Vector(vector.ToImmutableArray()));
            }

            return new Matrix(vectors.ToImmutableArray());
        }

        [Benchmark]
        public object TransposeMatrix() => transpose.Execute();
    }
}