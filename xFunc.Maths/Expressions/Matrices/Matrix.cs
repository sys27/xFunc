// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace xFunc.Maths.Expressions.Matrices;

/// <summary>
/// Represents a matrix expression.
/// </summary>
public class Matrix : IExpression
{
    private const int MinParametersCount = 1;

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class.
    /// </summary>
    /// <param name="vectors">The vectors.</param>
    /// <exception cref="ArgumentNullException"><paramref name="vectors"/> is null.</exception>
    public Matrix(Vector[] vectors)
        : this(vectors.ToImmutableArray())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Matrix"/> class.
    /// </summary>
    /// <param name="vectors">The vectors.</param>
    /// <exception cref="ArgumentNullException"><paramref name="vectors"/> is null.</exception>
    public Matrix(ImmutableArray<Vector> vectors)
    {
        if (vectors == null)
            throw new ArgumentNullException(nameof(vectors));

        if (vectors.Length < MinParametersCount)
            throw new ArgumentException(Resource.LessParams, nameof(vectors));

        var size = vectors[0].ParametersCount;
        foreach (var vector in vectors)
            if (vector.ParametersCount != size)
                throw new InvalidMatrixException();

        Vectors = vectors;
    }

    /// <summary>
    /// Gets or sets the <see cref="Vector"/> at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="Vector"/>.
    /// </value>
    /// <param name="index">The index.</param>
    /// <returns>The element of matrix.</returns>
    public Vector this[int index] => Vectors[index];

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj is null || GetType() != obj.GetType())
            return false;

        var matrix = (Matrix)obj;

        if (Vectors.Length != matrix.Vectors.Length)
            return false;

        return Vectors.SequenceEqual(matrix.Vectors);
    }

    /// <inheritdoc />
    public string ToString(IFormatter formatter) => Analyze(formatter);

    /// <inheritdoc />
    public override string ToString() => ToString(CommonFormatter.Instance);

    /// <inheritdoc />
    public object Execute() => Execute(null);

    /// <inheritdoc />
    public object Execute(ExpressionParameters? parameters)
    {
        var vectors = new VectorValue[Vectors.Length];

        for (var i = 0; i < Vectors.Length; i++)
        {
            var result = Vectors[i].Execute(parameters);
            if (result is not VectorValue vectorValue)
                throw new ResultIsNotSupportedException(this, result);

            vectors[i] = vectorValue;
        }

        return Unsafe.As<VectorValue[], MatrixValue>(ref vectors);
    }

    /// <inheritdoc />
    public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this);
    }

    /// <inheritdoc />
    public TResult Analyze<TResult, TContext>(
        IAnalyzer<TResult, TContext> analyzer,
        TContext context)
    {
        if (analyzer is null)
            throw new ArgumentNullException(nameof(analyzer));

        return analyzer.Analyze(this, context);
    }

    /// <summary>
    /// Clones this instance of the <see cref="IExpression" />.
    /// </summary>
    /// <param name="vectors">The list of arguments.</param>
    /// <returns>
    /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
    /// </returns>
    public IExpression Clone(ImmutableArray<Vector>? vectors = null)
        => new Matrix(vectors ?? Vectors);

    /// <summary>
    /// Gets the vectors.
    /// </summary>
    public ImmutableArray<Vector> Vectors { get; }
}