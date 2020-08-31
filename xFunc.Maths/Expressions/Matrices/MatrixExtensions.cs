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
using System.Linq;
using System.Threading.Tasks;
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Matrices
{
    /// <summary>
    /// Provides extension methods for matrices and vectors.
    /// </summary>
    internal static class MatrixExtensions
    {
        /// <summary>
        /// Calculates the absolute value (norm) of vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>Return the absolute value of vector.</returns>
        public static double Abs(this Vector vector, ExpressionParameters? parameters)
        {
            return Math.Sqrt(vector.Arguments.Sum(arg => Math.Pow((double)arg.Execute(parameters), 2)));
        }

        /// <summary>
        /// Adds the <paramref name="right"/> vector to the <paramref name="left"/> vector.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The sum of matrices.</returns>
        /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
        public static Vector Add(this Vector left, Vector right, ExpressionParameters? parameters)
        {
            if (left.ParametersCount != right.ParametersCount)
                throw new ArgumentException(Resource.MatrixArgException);

            var exps = new IExpression[left.ParametersCount];

            for (var i = 0; i < exps.Length; i++)
            {
                var leftNumber = (double)left[i].Execute(parameters);
                var rightNumber = (double)right[i].Execute(parameters);

                exps[i] = new Number(leftNumber + rightNumber);
            }

            return new Vector(exps);
        }

        /// <summary>
        /// Subtracts the <paramref name="right"/> vector from the <paramref name="left"/> vector.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The difference of matrices.</returns>
        /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
        public static Vector Sub(this Vector left, Vector right, ExpressionParameters? parameters)
        {
            if (left.ParametersCount != right.ParametersCount)
                throw new ArgumentException(Resource.MatrixArgException);

            var exps = new IExpression[left.ParametersCount];

            for (var i = 0; i < exps.Length; i++)
            {
                var leftNumber = (double)left[i].Execute(parameters);
                var rightNumber = (double)right[i].Execute(parameters);

                exps[i] = new Number(leftNumber - rightNumber);
            }

            return new Vector(exps);
        }

        /// <summary>
        /// Multiplies <paramref name="vector"/> by <paramref name="number"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="number">The number.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The product of matrices.</returns>
        public static Vector Mul(this Vector vector, Number number, ExpressionParameters? parameters)
        {
            var size = vector.ParametersCount;
            var numbers = new IExpression[size];

            for (var index = 0; index < size; index++)
            {
                var argNumber = (double)vector[index].Execute(parameters);
                numbers[index] = new Number(argNumber * number.Value);
            }

            return new Vector(numbers);
        }

        /// <summary>
        /// Adds the <paramref name="right"/> matrix to the <paramref name="left"/> matrix.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The sum of matrices.</returns>
        /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Add(this Matrix left, Matrix right, ExpressionParameters? parameters)
        {
            if (left.Rows != right.Rows ||
                left.Columns != right.Columns)
                throw new ArgumentException(Resource.MatrixArgException);

            var vectors = new Vector[left.Rows];

            for (var i = 0; i < vectors.Length; i++)
            {
                var exps = new IExpression[left.Columns];

                for (var j = 0; j < exps.Length; j++)
                {
                    var leftNumber = (double)left[i][j].Execute(parameters);
                    var rightNumber = (double)right[i][j].Execute(parameters);

                    exps[j] = new Number(leftNumber + rightNumber);
                }

                vectors[i] = new Vector(exps);
            }

            return new Matrix(vectors);
        }

        /// <summary>
        /// Subtracts the <paramref name="right"/> matrix from the <paramref name="left"/> matrix.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The difference of matrices.</returns>
        /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Sub(this Matrix left, Matrix right, ExpressionParameters? parameters)
        {
            if (left.Rows != right.Rows ||
                left.Columns != right.Columns)
                throw new ArgumentException(Resource.MatrixArgException);

            var vectors = new Vector[left.Rows];

            for (var i = 0; i < vectors.Length; i++)
            {
                var exps = new IExpression[left.Columns];

                for (var j = 0; j < exps.Length; j++)
                {
                    var leftNumber = (double)left[i][j].Execute(parameters);
                    var rightNumber = (double)right[i][j].Execute(parameters);

                    exps[j] = new Number(leftNumber - rightNumber);
                }

                vectors[i] = new Vector(exps);
            }

            return new Matrix(vectors);
        }

        /// <summary>
        /// Multiplies <paramref name="matrix"/> by <paramref name="number"/>.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="number">The number.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The product of matrix and number.</returns>
        public static Matrix Mul(this Matrix matrix, Number number, ExpressionParameters? parameters)
        {
            var vectors = new Vector[matrix.Rows];

            for (var i = 0; i < vectors.Length; i++)
            {
                var vector = new IExpression[matrix.Columns];

                for (var j = 0; j < vector.Length; j++)
                    vector[j] = new Number((double)matrix[i][j].Execute(parameters) * number.Value);

                vectors[i] = new Vector(vector);
            }

            return new Matrix(vectors);
        }

        /// <summary>
        /// Multiplies the <paramref name="left" /> matrix by the <paramref name="right" /> matrix.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// The product of matrices.
        /// </returns>
        /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Mul(this Matrix left, Matrix right, ExpressionParameters? parameters)
        {
            if (left.Columns != right.Rows)
                throw new ArgumentException(Resource.MatrixArgException);

            var rows = left.Rows;
            var vectors = new Vector[rows];

            if (rows <= 10)
            {
                for (var row = 0; row < rows; row++)
                    vectors[row] = MulMatrixRow(left, right, parameters, row);
            }
            else
            {
                Parallel.For(0, rows, row => vectors[row] = MulMatrixRow(left, right, parameters, row));
            }

            return new Matrix(vectors);
        }

        private static Vector MulMatrixRow(
            Matrix left,
            Matrix right,
            ExpressionParameters? parameters,
            int i)
        {
            var columns = right.Columns;
            var vector = new IExpression[columns];

            for (var j = 0; j < columns; j++)
            {
                var element = 0.0;
                for (var k = 0; k < left.Columns; k++)
                {
                    var leftNumber = (double)left[i][k].Execute(parameters);
                    var rightNumber = (double)right[k][j].Execute(parameters);

                    element += leftNumber * rightNumber;
                }

                vector[j] = new Number(element);
            }

            return new Vector(vector);
        }

        /// <summary>
        /// Multiplies the <paramref name="left" /> vector by the <paramref name="right" /> matrix.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// The product of matrices.
        /// </returns>
        /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Mul(this Vector left, Matrix right, ExpressionParameters? parameters)
        {
            var matrix = new Matrix(new[] { left });

            return matrix.Mul(right, parameters);
        }

        /// <summary>
        /// Multiplies the <paramref name="left" /> matrix by the <paramref name="right" /> vector.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// The product of matrices.
        /// </returns>
        /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Mul(this Matrix left, Vector right, ExpressionParameters? parameters)
        {
            var matrix = new Matrix(new[] { right });

            return left.Mul(matrix, parameters);
        }

        /// <summary>
        /// Calculates ths dot product of two vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// The dot product of vectors.
        /// </returns>
        /// <exception cref="ArgumentException">The size of vectors is invalid.</exception>
        public static double Mul(this Vector left, Vector right, ExpressionParameters? parameters)
        {
            if (left.ParametersCount != right.ParametersCount)
                throw new ArgumentException(Resource.MatrixArgException);

            var vector1 = left.ToCalculatedArray(parameters);
            var vector2 = right.ToCalculatedArray(parameters);

            var product = 0.0;
            for (var i = 0; i < vector1.Length; i++)
                product += vector1[i] * vector2[i];

            return product;
        }

        /// <summary>
        /// Transposes the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The transposed matrix.</returns>
        public static Matrix Transpose(this Vector vector)
        {
            var vectors = new Vector[vector.ParametersCount];

            for (var i = 0; i < vector.ParametersCount; i++)
                vectors[i] = new Vector(new[] { vector[i] });

            return new Matrix(vectors);
        }

        /// <summary>
        /// Transposes the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The transposed matrix.</returns>
        public static IExpression Transpose(this Matrix matrix)
        {
            var vectors = new Vector[matrix.Columns];

            for (var i = 0; i < vectors.Length; i++)
            {
                var vector = new IExpression[matrix.Rows];

                for (var j = 0; j < vector.Length; j++)
                    vector[j] = matrix[j][i];

                vectors[i] = new Vector(vector);
            }

            return new Matrix(vectors);
        }

        /// <summary>
        /// Calculates a determinant of specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The determinant of matrix.</returns>
        /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
        public static double Determinant(this Matrix matrix, ExpressionParameters? parameters)
        {
            if (!matrix.IsSquare)
                throw new ArgumentException(Resource.MatrixArgException);

            var array = matrix.ToCalculatedArray(parameters);

            return Determinant_(array);
        }

        private static double Determinant_(double[][] matrix)
        {
            if (matrix.Length == 1)
                return matrix[0][0];
            if (matrix.Length == 2)
                return matrix[0][0] * matrix[1][1] - matrix[1][0] * matrix[0][1];

            var lu = LUPDecompositionInternal(matrix, out _, out var toggle);

            if (lu == null)
                throw new MatrixIsInvalidException();

            double result = toggle;
            for (var i = 0; i < lu.Length; i++)
                result *= lu[i][i];

            return result;
        }

        private static double[][] LUPDecompositionInternal(double[][] matrix, out int[] permutation, out int toggle)
        {
            var size = matrix.Length;

            permutation = new int[size];
            for (var i = 0; i < size; i++)
                permutation[i] = i;

            toggle = 1;

            for (var j = 0; j < size - 1; j++)
            {
                var colMax = Math.Abs(matrix[j][j]);
                var pRow = j;

                for (var i = j + 1; i < size; i++)
                {
                    if (matrix[i][j] > colMax)
                    {
                        colMax = matrix[i][j];
                        pRow = i;
                    }
                }

                if (pRow != j)
                {
                    var rowPtr = matrix[pRow];
                    matrix[pRow] = matrix[j];
                    matrix[j] = rowPtr;
                    var tmp = permutation[pRow];
                    permutation[pRow] = permutation[j];
                    permutation[j] = tmp;
                    toggle = -toggle;
                }

                if (matrix[j][j].Equals(0) || Math.Abs(matrix[j][j]) < 1E-14)
                    throw new MatrixIsInvalidException();

                for (var i = j + 1; i < size; i++)
                {
                    matrix[i][j] /= matrix[j][j];
                    for (var k = j + 1; k < size; k++)
                        matrix[i][k] -= matrix[i][j] * matrix[j][k];
                }
            }

            return matrix;
        }

        private static double[] HelperSolve(double[][] lu, double[] b)
        {
            var n = lu.Length;
            var x = new double[n];
            b.CopyTo(x, 0);

            for (var i = 1; i < n; ++i)
            {
                var sum = x[i];
                for (var j = 0; j < i; ++j)
                    sum -= lu[i][j] * x[j];
                x[i] = sum;
            }

            x[n - 1] /= lu[n - 1][n - 1];

            for (var i = n - 2; i >= 0; --i)
            {
                var sum = x[i];
                for (var j = i + 1; j < n; ++j)
                    sum -= lu[i][j] * x[j];
                x[i] = sum / lu[i][i];
            }

            return x;
        }

        /// <summary>
        /// Inverts a matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>An inverse matrix.</returns>
        public static Matrix Inverse(this Matrix matrix, ExpressionParameters? parameters)
        {
            if (!matrix.IsSquare)
                throw new ArgumentException(Resource.MatrixArgException);

            var size = matrix.Rows;
            var result = InverseInternal(matrix.ToCalculatedArray(parameters));
            var vectors = new Vector[size];

            for (var i = 0; i < size; i++)
            {
                var vector = new IExpression[size];

                for (var j = 0; j < size; j++)
                    vector[j] = new Number(result[i][j]);

                vectors[i] = new Vector(vector);
            }

            return new Matrix(vectors);
        }

        private static double[][] InverseInternal(double[][] matrix)
        {
            var n = matrix.Length;
            var result = new double[n][];

            for (var i = 0; i < n; i++)
            {
                result[i] = new double[n];
                for (var j = 0; j < n; j++)
                    result[i][j] = matrix[i][j];
            }

            var lu = LUPDecompositionInternal(matrix, out var permutation, out _);
            if (lu == null)
                throw new MatrixIsInvalidException();

            var b = new double[n];
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (i == permutation[j])
                        b[j] = 1.0;
                    else
                        b[j] = 0.0;
                }

                var x = HelperSolve(lu, b);
                for (var j = 0; j < n; j++)
                    result[j][i] = x[j];
            }

            return result;
        }

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="left">The multiplier vector.</param>
        /// <param name="right">The multiplicand vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The cross product.</returns>
        public static Vector Cross(this Vector left, Vector right, ExpressionParameters? parameters)
        {
            if (left.ParametersCount != 3 || right.ParametersCount != 3)
                throw new ArgumentException(Resource.VectorCrossException);

            var vector1 = left.ToCalculatedArray(parameters);
            var vector2 = right.ToCalculatedArray(parameters);

            return new Vector(new IExpression[]
            {
                new Number(vector1[1] * vector2[2] - vector1[2] * vector2[1]),
                new Number(vector1[2] * vector2[0] - vector1[0] * vector2[2]),
                new Number(vector1[0] * vector2[1] - vector1[1] * vector2[0]),
            });
        }
    }
}