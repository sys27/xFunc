// Copyright 2012-2018 Dmitry Kischenko
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using xFunc.Maths.Analyzers;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Matrices
{

    /// <summary>
    /// Represents a matrix.
    /// </summary>
    public class Matrix : DifferentParametersExpression
    {

        [ExcludeFromCodeCoverage]
        internal Matrix() : base(null, -1) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="args"/> is null.</exception>
        public Matrix(Vector[] args) : base(args, args.Length)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            if (args.Length == 0)
                throw new ArgumentException(Resource.MatrixArgException, nameof(args));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="matrixSize">The size of the matrix.</param>
        /// <param name="vectorSize">The size of the vector.</param>
        public Matrix(int matrixSize, int vectorSize)
            : base(null, matrixSize)
        {
            var vectors = new Vector[matrixSize];
            for (var i = 0; i < vectors.Length; i++)
                vectors[i] = new Vector(vectorSize);

            Arguments = vectors;
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
            get
            {
                return (Vector)m_arguments[index];
            }
            set
            {
                m_arguments[index] = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="IExpression"/> with the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="IExpression"/>.
        /// </value>
        /// <param name="row">The row.</param>
        /// <param name="col">The column.</param>
        /// <returns>The element of matrix.</returns>
        public IExpression this[int row, int col]
        {
            get
            {
                return this[row][col];
            }
            set
            {
                this[row][col] = value;
            }
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode(743, 3863);
        }

        private Vector[] CalculateMatrix(ExpressionParameters parameters)
        {
            var args = new Vector[this.ParametersCount];

            for (var i = 0; i < this.ParametersCount; i++)
                args[i] = (Vector)m_arguments[i].Execute(parameters);

            return args;
        }

        /// <summary>
        /// Executes this expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the execution.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override object Execute(ExpressionParameters parameters)
        {
            return new Matrix(CalculateMatrix(parameters));
        }

        /// <summary>
        /// Analyzes the current expression.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="analyzer">The analyzer.</param>
        /// <returns>
        /// The analysis result.
        /// </returns>
        public override TResult Analyze<TResult>(IAnalyzer<TResult> analyzer)
        {
            return analyzer.Analyze(this);
        }

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Matrix(Array.ConvertAll(CloneArguments(), x => (Vector)x));
        }

        internal double[][] ToCalculatedArray(ExpressionParameters parameters)
        {
            return (from Vector vector in m_arguments.AsParallel().AsOrdered()
                    select vector.ToCalculatedArray(parameters)).ToArray();
        }

        /// <summary>
        /// Swaps the rows of matrix.
        /// </summary>
        /// <param name="firstIndex">The index of first row.</param>
        /// <param name="secondIndex">The index of second row.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="firstIndex"/> or <paramref name="secondIndex"/> is out of range.</exception>
        public void SwapRows(int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || firstIndex >= ParametersCount)
                throw new ArgumentException();
            if (secondIndex < 0 || secondIndex >= ParametersCount)
                throw new ArgumentException();

            var temp = m_arguments[firstIndex];
            m_arguments[firstIndex] = m_arguments[secondIndex];
            m_arguments[secondIndex] = temp;
        }

        /// <summary>
        /// Swaps the columns of matrix.
        /// </summary>
        /// <param name="firstIndex">The index of first column.</param>
        /// <param name="secondIndex">The index of second column.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"><paramref name="firstIndex"/> or <paramref name="secondIndex"/> is out of range.</exception>
        public void SwapColumns(int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || firstIndex >= ParametersCount)
                throw new ArgumentOutOfRangeException();
            if (secondIndex < 0 || secondIndex >= ParametersCount)
                throw new ArgumentOutOfRangeException();

            foreach (Vector item in m_arguments)
            {
                var temp = item[firstIndex];
                item[firstIndex] = item[secondIndex];
                item[secondIndex] = temp;
            }
        }

        /// <summary>
        /// Creates an identity matrix.
        /// </summary>
        /// <param name="sizeOfMatrix">The size of matrix.</param>
        /// <returns>An identity matrix.</returns>
        public static Matrix CreateIdentity(int sizeOfMatrix)
        {
            var matrix = new Matrix(sizeOfMatrix, sizeOfMatrix);

            for (var i = 0; i < sizeOfMatrix; i++)
            {
                for (var j = 0; j < sizeOfMatrix; j++)
                    matrix[i][j] = new Number(0);

                matrix[i][i] = new Number(1);
            }

            return matrix;
        }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public override IExpression[] Arguments
        {
            get
            {
                return m_arguments;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    var size = value[0].ParametersCount;

                    if (value.Any(exp => exp.ParametersCount != size))
                        throw new MatrixIsInvalidException();
                }

                base.Arguments = value;
            }
        }

        /// <summary>
        /// Gets the size of vectors.
        /// </summary>
        /// <value>
        /// The size of vectors.
        /// </value>
        public int SizeOfVectors => m_arguments[0].ParametersCount;

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinParameters => 1;

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int MaxParameters => -1;

        /// <summary>
        /// Gets a value indicating whether matrix is square.
        /// </summary>
        /// <value>
        ///   <c>true</c> if matrix is square; otherwise, <c>false</c>.
        /// </value>
        public bool IsSquare => this.ParametersCount == this.SizeOfVectors;

    }

}
