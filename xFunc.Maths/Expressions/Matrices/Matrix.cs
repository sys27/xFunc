// Copyright 2012-2014 Dmitry Kischenko
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
#if NET35_OR_GREATER || PORTABLE
using System.Linq;
#endif
using System.Text;

namespace xFunc.Maths.Expressions.Matrices
{

    /// <summary>
    /// Represents a matrix.
    /// </summary>
    public class Matrix : DifferentParametersExpression
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        internal Matrix()
            : base(null, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="System.ArgumentNullException"><paramref name="args"/> is null.</exception>
        /// <exception cref="System.ArgumentException"></exception>
        public Matrix(Vector[] args)
            : base(args, args.Length)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            if (args.Length < 1)
                throw new ArgumentException();
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
            for (int i = 0; i < vectors.Length; i++)
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
                return (Vector)arguments[index];
            }
            set
            {
                arguments[index] = value;
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
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;

            if (obj == null || this.GetType() != obj.GetType())
                return false;

            var matrix = (Matrix)obj;

            return this.countOfParams == matrix.countOfParams && this.arguments.SequenceEqual(matrix.arguments);
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

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append('{');
            foreach (var item in arguments)
                sb.Append(item).Append(", ");
            sb.Remove(sb.Length - 2, 2).Append('}');

            return sb.ToString();
        }

        private void CheckMatrix(IExpression[] args)
        {
            var size = args[0].CountOfParams;

            if (args.Any(exp => !(exp is Vector) || exp.CountOfParams != size))
                throw new MatrixIsInvalidException();
        }

        /// <summary>
        /// Calculates this mathemarical expression.
        /// </summary>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// A result of the calculation.
        /// </returns>
        /// <seealso cref="ExpressionParameters" />
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override object Calculate(ExpressionParameters parameters)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <returns>
        /// Returns a derivative of the expression.
        /// </returns>
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override IExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Calculates a derivative of the expression.
        /// </summary>
        /// <param name="variable">The variable of differentiation.</param>
        /// <returns>
        /// Returns a derivative of the expression of several variables.
        /// </returns>
        /// <seealso cref="Variable" />
        /// <exception cref="System.NotSupportedException">Always.</exception>
        public override IExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Clones this instance of the <see cref="IExpression" />.
        /// </summary>
        /// <returns>
        /// Returns the new instance of <see cref="IExpression" /> that is a clone of this instance.
        /// </returns>
        public override IExpression Clone()
        {
            return new Matrix((Vector[])CloneArguments());
        }

        internal double[][] ToCalculatedArray(ExpressionParameters parameters)
        {
#if NET40_OR_GREATER
            return (from vector in arguments.AsParallel().AsOrdered()
                    select ((Vector)vector).ToCalculatedArray(parameters)).ToArray();
#else
            return (from vector in arguments
                    select ((Vector)vector).ToCalculatedArray(parameters)).ToArray();
#endif
        }

        public void SwapRows(int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || firstIndex >= countOfParams)
                throw new ArgumentException();
            if (secondIndex < 0 || secondIndex >= countOfParams)
                throw new ArgumentException();

            var temp = arguments[firstIndex];
            arguments[firstIndex] = arguments[secondIndex];
            arguments[secondIndex] = temp;
        }

        public void SwapColumns(int firstIndex, int secondIndex)
        {
            if (firstIndex < 0 || firstIndex >= countOfParams)
                throw new ArgumentException();
            if (secondIndex < 0 || secondIndex >= countOfParams)
                throw new ArgumentException();

            foreach (Vector item in arguments)
            {
                var temp = item[firstIndex];
                item[firstIndex] = item[secondIndex];
                item[secondIndex] = temp;
            }
        }

        public static Matrix CreateIdentity(int sizeOfMatrix)
        {
            var matrix = new Matrix(sizeOfMatrix, sizeOfMatrix);

            for (int i = 0; i < sizeOfMatrix; i++)
            {
                for (int j = 0; j < sizeOfMatrix; j++)
                    matrix[i][j] = new Number(0);

                matrix[i][i] = new Number(1);
            }

            return matrix;
        }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <value>
        /// The arguments.
        /// </value>
        public override IExpression[] Arguments
        {
            get
            {
                return base.Arguments;
            }
            set
            {
                if (value != null)
                {
                    if (value.Length == 0)
                        throw new ArgumentException();

                    CheckMatrix(value);
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
        public int SizeOfVectors
        {
            get
            {
                return arguments[0].CountOfParams;
            }
        }

        /// <summary>
        /// Gets the minimum count of parameters.
        /// </summary>
        /// <value>
        /// The minimum count of parameters.
        /// </value>
        public override int MinCountOfParams
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the maximum count of parameters. -1 - Infinity.
        /// </summary>
        /// <value>
        /// The maximum count of parameters.
        /// </value>
        public override int MaxCountOfParams
        {
            get
            {
                return -1;
            }
        }

        public bool IsSquare
        {
            get
            {
                return this.countOfParams == this.SizeOfVectors;
            }
        }

    }

}
