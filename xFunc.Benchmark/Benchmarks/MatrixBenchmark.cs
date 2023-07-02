// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Benchmark.Benchmarks;

public class MatrixBenchmark
{
    private Matrix matrix1;
    private Matrix matrix2;

    [Params(2, 10, 100)]
    public int Size;

    [GlobalSetup]
    public void Setup()
    {
        matrix1 = CreateMatrix();
        matrix2 = CreateMatrix();
    }

    private Matrix CreateMatrix()
    {
        var vectors = ImmutableArray.CreateBuilder<Vector>(Size);
        for (var i = 0; i < Size; i++)
        {
            var vector = ImmutableArray.CreateBuilder<IExpression>(Size);
            for (var j = 0; j < Size; j++)
                vector.Add(new Number(Random.Shared.Next()));

            vectors.Add(new Vector(vector.ToImmutableArray()));
        }

        return new Matrix(vectors.ToImmutableArray());
    }

    [Benchmark]
    public object AddMatrix()
        => new Add(matrix1, matrix2).Execute();

    [Benchmark]
    public object SubMatrix()
        => new Sub(matrix1, matrix2).Execute();

    [Benchmark]
    public object MulMatrix()
        => new Mul(matrix1, matrix2).Execute();

    [Benchmark]
    public object MulMatrixByNumber()
        => new Mul(matrix1, Number.Two).Execute();
}