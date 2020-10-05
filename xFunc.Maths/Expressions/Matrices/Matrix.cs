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

using System;
using System.Collections.Immutable;
using System.Linq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Matrices
{
    /// <summary>
    /// Represents a matrix.
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
                    throw new MatrixIsInvalidException();

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
        public override string ToString() => ToString(new CommonFormatter());

        /// <inheritdoc />
        public object Execute() => Execute(null);

        /// <inheritdoc />
        public object Execute(ExpressionParameters? parameters)
        {
            var args = ImmutableArray.CreateBuilder<Vector>(Rows);

            foreach (var vector in Vectors)
                args.Add((Vector)vector.Execute(parameters));

            return new Matrix(args.ToImmutableArray());
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
        /// Calculates current matrix and returns it as an two dimensional array.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The two dimensional array which represents current vector.</returns>
        internal NumberValue[][] ToCalculatedArray(ExpressionParameters? parameters)
        {
            var results = new NumberValue[Rows][];

            for (var i = 0; i < Rows; i++)
                results[i] = Vectors[i].ToCalculatedArray(parameters);

            return results;
        }

        /// <summary>
        /// Gets the vectors.
        /// </summary>
        public ImmutableArray<Vector> Vectors { get; }

        /// <summary>
        /// Gets the count of rows.
        /// </summary>
        public int Rows => Vectors.Length;

        /// <summary>
        /// Gets the count of columns.
        /// </summary>
        public int Columns => Vectors[0].ParametersCount;

        /// <summary>
        /// Gets a value indicating whether matrix is square.
        /// </summary>
        public bool IsSquare => Rows == Columns;
    }
}