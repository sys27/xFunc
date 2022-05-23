// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Benchmark.Benchmarks;

public class MulMatrixBenchmark
{
    private readonly Random random = new Random();

    private Mul mul;

    [Params(2, 10, 100)]
    public int Size;

    [GlobalSetup]
    public void Setup()
    {
        var left = CreateMatrix();
        var right = CreateMatrix();

        mul = new Mul(left, right);
    }

    private Matrix CreateMatrix()
    {
        var vectors = ImmutableArray.CreateBuilder<Vector>(Size);
        for (var i = 0; i < Size; i++)
        {
            var vector = ImmutableArray.CreateBuilder<IExpression>(Size);
            for (var j = 0; j < Size; j++)
                vector.Add(new Number(random.Next()));

            vectors.Add(new Vector(vector.ToImmutableArray()));
        }

        return new Matrix(vectors.ToImmutableArray());
    }

    [Benchmark]
    public object MulMatrix() => mul.Execute();
}