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
using System.Collections.Generic;
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

        private readonly IList<Vector> vectors;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="vectors">The vectors.</param>
        /// <exception cref="ArgumentNullException"><paramref name="vectors"/> is null.</exception>
        public Matrix(IList<Vector> vectors)
        {
            if (vectors == null)
                throw new ArgumentNullException(nameof(vectors));

            if (vectors.Count < MinParametersCount)
                throw new ArgumentException(Resource.LessParams, nameof(vectors));

            var size = vectors[0].ParametersCount;
            foreach (var vector in vectors)
                if (vector.ParametersCount != size)
                    throw new MatrixIsInvalidException();

            this.vectors = vectors;
        }

        /// <summary>
        /// Gets or sets the <see cref="Vector"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Vector"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>The element of matrix.</returns>
        public Vector this[int index]
        {
            get => vectors[index];
            set => vectors[index] = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (obj == null || GetType() != obj.GetType())
                return false;

            var matrix = (Matrix)obj;

            if (vectors.Count != matrix.vectors.Count)
                return false;

            return vectors.SequenceEqual(matrix.vectors);
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <param name="formatter">The formatter.</param>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public string ToString(IFormatter formatter) => Analyze(formatter);

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString() => ToString(new CommonFormatter());

        /// <summary>
        /// Executes this expression. Don't use this method if your expression has variables or user-functions.
        /// </summary>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        public object Execute() => Execute(null);

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        public object Execute(ExpressionParameters parameters)
        {
            var args = new Vector[Rows];

            for (var i = 0; i < Rows; i++)
                args[i] = (Vector)vectors[i].Execute(parameters);

            return new Matrix(args);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TContext">The type of additional parameter for analyzer.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="context">The context.</param>
        /// <returns>The analysis result.</returns>
        public TResult Analyze<TResult, TContext>(
            IAnalyzer<TResult, TContext> analyzer,
            TContext context)
        {
            if (analyzer == null)
                throw new ArgumentNullException(nameof(analyzer));

            return analyzer.Analyze(this, context);
        }

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public IExpression Clone()
        {
            var args = new Vector[Rows];

            for (var i = 0; i < Rows; i++)
                args[i] = (Vector)vectors[i].Clone();

            return new Matrix(args);
        }

        /// <summary>
        /// Calculates current matrix and returns it as an two dimensional array.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The two dimensional array which represents current vector.</returns>
        internal double[][] ToCalculatedArray(ExpressionParameters parameters)
        {
            var results = new double[Rows][];

            for (var i = 0; i < Rows; i++)
                results[i] = vectors[i].ToCalculatedArray(parameters);

            return results;
        }

        /// <summary>
        /// Gets the vectors.
        /// </summary>
        /// <value>The vectors.</value>
        public IEnumerable<Vector> Vectors => vectors;

        /// <summary>
        /// Gets the count of rows.
        /// </summary>
        /// <value>
        /// The count of rows.
        /// </value>
        public int Rows => vectors.Count;

        /// <summary>
        /// Gets the count of columns.
        /// </summary>
        /// <value>
        /// The count of columns.
        /// </value>
        public int Columns => vectors[0].ParametersCount;

        /// <summary>
        /// Gets a value indicating whether matrix is square.
        /// </summary>
        /// <value>
        ///   <c>true</c> if matrix is square; otherwise, <c>false</c>.
        /// </value>
        public bool IsSquare => Rows == Columns;
    }
}