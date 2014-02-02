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
#if NET40_OR_GREATER
using System.Threading.Tasks;
#endif
using xFunc.Maths.Resources;

namespace xFunc.Maths.Expressions.Matrices
{

    /// <summary>
    /// Provides extention methods for matrices and vectors.
    /// </summary>
    public static class MatrixExtentions
    {

        /// <summary>
        /// Adds the <paramref name="right"/> vector to the <paramref name="left"/> vector.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>The sum of matrices.</returns>
        public static Vector Add(this Vector left, Vector right)
        {
            return Add(left, right, null);
        }

        /// <summary>
        /// Adds the <paramref name="right"/> vector to the <paramref name="left"/> vector.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The sum of matrices.</returns>
        /// <exception cref="System.ArgumentException">The size of matrices is invalid.</exception>
        public static Vector Add(this Vector left, Vector right, ExpressionParameters parameters)
        {
            if (left.CountOfParams != right.CountOfParams)
                throw new ArgumentException(Resource.MatrixArgException);

            var exps = new IExpression[left.CountOfParams];
#if NET40_OR_GREATER
            Parallel.For(0, left.CountOfParams,
                i => exps[i] = new Number((double)left.Arguments[i].Calculate(parameters) + (double)right.Arguments[i].Calculate(parameters))
            );
#else
            for (int i = 0; i < left.CountOfParams; i++)
                exps[i] = new Number((double)left.Arguments[i].Calculate(parameters) + (double)right.Arguments[i].Calculate(parameters));
#endif

            return new Vector(exps);
        }

        /// <summary>
        /// Subtracts the <paramref name="right"/> vector from the <paramref name="left"/> vector.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>The difference of matrices.</returns>
        public static Vector Sub(this Vector left, Vector right)
        {
            return Sub(left, right, null);
        }

        /// <summary>
        /// Subtracts the <paramref name="right"/> vector from the <paramref name="left"/> vector.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The difference of matrices.</returns>
        /// <exception cref="System.ArgumentException">The size of matrices is invalid.</exception>
        public static Vector Sub(this Vector left, Vector right, ExpressionParameters parameters)
        {
            if (left.CountOfParams != right.CountOfParams)
                throw new ArgumentException(Resource.MatrixArgException);

            var exps = new IExpression[left.CountOfParams];
#if NET40_OR_GREATER
            Parallel.For(0, left.CountOfParams,
                i => exps[i] = new Number((double)left.Arguments[i].Calculate(parameters) - (double)right.Arguments[i].Calculate(parameters))
            );
#else
            for (int i = 0; i < left.CountOfParams; i++)
                exps[i] = new Number((double)left.Arguments[i].Calculate(parameters) - (double)right.Arguments[i].Calculate(parameters));
#endif

            return new Vector(exps);
        }

        /// <summary>
        /// Multiplies <paramref name="vector"/> by <paramref name="number"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="number">The number.</param>
        /// <returns>The product of matrices.</returns>
        public static Vector Mul(this Vector vector, IExpression number)
        {
            return Mul(vector, number, null);
        }

        /// <summary>
        /// Multiplies <paramref name="vector"/> by <paramref name="number"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="number">The number.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The product of matrices.</returns>
        public static Vector Mul(this Vector vector, IExpression number, ExpressionParameters parameters)
        {
            var n = (double)number.Calculate(parameters);
#if NET40_OR_GREATER
            var numbers = (from num in vector.Arguments.AsParallel().AsOrdered()
                           select new Number((double)num.Calculate(parameters) * n))
                          .ToArray();
#else
            var numbers = (from num in vector.Arguments
                           select new Number((double)num.Calculate(parameters) * n))
                          .ToArray();
#endif

            return new Vector(numbers);
        }

        /// <summary>
        /// Adds the <paramref name="right"/> matrix to the <paramref name="left"/> matrix.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <returns>The sum of matrices.</returns>
        public static Matrix Add(this Matrix left, Matrix right)
        {
            return Add(left, right, null);
        }

        /// <summary>
        /// Adds the <paramref name="right"/> matrix to the <paramref name="left"/> matrix.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The sum of matrices.</returns>
        /// <exception cref="System.ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Add(this Matrix left, Matrix right, ExpressionParameters parameters)
        {
            if (left.CountOfParams != right.CountOfParams || left.SizeOfVectors != right.SizeOfVectors)
                throw new ArgumentException(Resource.MatrixArgException);

            var vectors = new Vector[left.CountOfParams];
#if NET40_OR_GREATER
            Parallel.For(0, left.CountOfParams, i =>
            {
                var exps = new IExpression[left.SizeOfVectors];

                for (int j = 0; j < left.SizeOfVectors; j++)
                    exps[j] = new Number((double)left[i][j].Calculate(parameters) + (double)right[i][j].Calculate(parameters));

                vectors[i] = new Vector(exps);
            });
#else
            for (int i = 0; i < left.CountOfParams; i++)
            {
                var exps = new IExpression[left.SizeOfVectors];

                for (int j = 0; j < left.SizeOfVectors; j++)
                    exps[j] = new Number((double)left[i][j].Calculate(parameters) + (double)right[i][j].Calculate(parameters));

                vectors[i] = new Vector(exps);
            }
#endif

            return new Matrix(vectors);
        }

        /// <summary>
        /// Subtracts the <paramref name="right"/> matrix from the <paramref name="left"/> matrix.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <returns>The difference of matrices.</returns>
        public static Matrix Sub(this Matrix left, Matrix right)
        {
            return Sub(left, right, null);
        }

        /// <summary>
        /// Subtracts the <paramref name="right"/> matrix from the <paramref name="left"/> matrix.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The difference of matrices.</returns>
        /// <exception cref="System.ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Sub(this Matrix left, Matrix right, ExpressionParameters parameters)
        {
            if (left.CountOfParams != right.CountOfParams || left.SizeOfVectors != right.SizeOfVectors)
                throw new ArgumentException(Resource.MatrixArgException);

            var vectors = new Vector[left.CountOfParams];
#if NET40_OR_GREATER
            Parallel.For(0, left.CountOfParams, i =>
            {
                var exps = new IExpression[left.SizeOfVectors];

                for (int j = 0; j < left.SizeOfVectors; j++)
                    exps[j] = new Number((double)left[i][j].Calculate(parameters) - (double)right[i][j].Calculate(parameters));

                vectors[i] = new Vector(exps);
            });
#else
            for (int i = 0; i < left.CountOfParams; i++)
            {
                var exps = new IExpression[left.SizeOfVectors];

                for (int j = 0; j < left.SizeOfVectors; j++)
                    exps[j] = new Number((double)left[i][j].Calculate(parameters) - (double)right[i][j].Calculate(parameters));

                vectors[i] = new Vector(exps);
            }
#endif

            return new Matrix(vectors);
        }

        /// <summary>
        /// Multiplies <paramref name="matrix"/> by <paramref name="number"/>.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="number">The number.</param>
        /// <returns>The product of matrix and number.</returns>
        public static Matrix Mul(this Matrix matrix, IExpression number)
        {
            return Mul(matrix, number, null);
        }

        /// <summary>
        /// Multiplies <paramref name="matrix"/> by <paramref name="number"/>.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="number">The number.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The product of matrix and number.</returns>
        public static Matrix Mul(this Matrix matrix, IExpression number, ExpressionParameters parameters)
        {
            var n = (double)number.Calculate(parameters);
#if NET40_OR_GREATER
            var result = from v in matrix.Arguments.AsParallel().AsOrdered()
                         select new Vector(
                             (from num in ((Vector)v).Arguments
                              select new Number((double)num.Calculate(parameters) * n))
                             .ToArray()
                         );
#else
            var result = from v in matrix.Arguments
                         select new Vector(
                             (from num in ((Vector)v).Arguments
                              select new Number((double)num.Calculate(parameters) * n))
                             .ToArray()
                         );
#endif

            return new Matrix(result.ToArray());
        }

        /// <summary>
        /// Multiplies the <paramref name="left" /> matrix by the <paramref name="right" /> matrix.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right matrix.</param>
        /// <returns>
        /// The product of matrices.
        /// </returns>
        public static Matrix Mul(this Matrix left, Matrix right)
        {
            return Mul(left, right, null);
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
        /// <exception cref="System.ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Mul(this Matrix left, Matrix right, ExpressionParameters parameters)
        {
            if (left.SizeOfVectors != right.CountOfParams)
                throw new ArgumentException(Resource.MatrixArgException);

            var result = new Matrix(left.CountOfParams, right.SizeOfVectors);
#if NET40_OR_GREATER
            Parallel.For(0, right.SizeOfVectors, i =>
            {
                for (int j = 0; j < left.CountOfParams; j++)
                {
                    double el = 0;
                    for (int k = 0; k < left.SizeOfVectors; k++)
                        el += (double)left[j][k].Calculate(parameters) * (double)right[k][i].Calculate(parameters);
                    result[j][i] = new Number(el);
                }
            });
#else
            for (int i = 0; i < right.SizeOfVectors; i++)
            {
                for (int j = 0; j < left.CountOfParams; j++)
                {
                    double el = 0;
                    for (int k = 0; k < left.SizeOfVectors; k++)
                        el += (double)left[j][k].Calculate(parameters) * (double)right[k][i].Calculate(parameters);
                    result[j][i] = new Number(el);
                }
            }
#endif

            return result;
        }

        /// <summary>
        /// Multiplies the <paramref name="left" /> vector by the <paramref name="right" /> matrix.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right matrix.</param>
        /// <returns>
        /// The product of matrices.
        /// </returns>
        public static Matrix Mul(this Vector left, Matrix right)
        {
            return Mul(left, right, null);
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
        /// <exception cref="System.ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Mul(this Vector left, Matrix right, ExpressionParameters parameters)
        {
            var matrix = new Matrix(new[] { left });

            return matrix.Mul(right, parameters);
        }

        /// <summary>
        /// Multiplies the <paramref name="left" /> matrix by the <paramref name="right" /> vector.
        /// </summary>
        /// <param name="left">The left matrix.</param>
        /// <param name="right">The right vector.</param>
        /// <returns>
        /// The product of matrices.
        /// </returns>
        public static Matrix Mul(this Matrix left, Vector right)
        {
            return Mul(left, right, null);
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
        /// <exception cref="System.ArgumentException">The size of matrices is invalid.</exception>
        public static Matrix Mul(this Matrix left, Vector right, ExpressionParameters parameters)
        {
            var matrix = new Matrix(new[] { right });

            return left.Mul(matrix, parameters);
        }

        /// <summary>
        /// Transposes the specified vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <returns>The transposed matrix.</returns>
        public static Matrix Transpose(this Vector vector)
        {
            var vectors = new Vector[vector.CountOfParams];
#if NET40_OR_GREATER
            Parallel.For(0, vectors.Length, i => vectors[i] = new Vector(new[] { vector[i] }));
#else
            for (int i = 0; i < vectors.Length; i++)
                vectors[i] = new Vector(new[] { vector[i] });
#endif

            return new Matrix(vectors);
        }

        /// <summary>
        /// Transposes the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The transposed matrix.</returns>
        public static IExpression Transpose(this Matrix matrix)
        {
            var result = new Matrix(matrix.SizeOfVectors, matrix.CountOfParams);

#if NET40_OR_GREATER
            Parallel.For(0, matrix.CountOfParams, i =>
            {
                for (int j = 0; j < matrix.SizeOfVectors; j++)
                    result[j][i] = matrix[i][j];
            });
#else
            for (int i = 0; i < matrix.CountOfParams; i++)
                for (int j = 0; j < matrix.SizeOfVectors; j++)
                    result[j][i] = matrix[i][j];
#endif

            return result;
        }

        /// <summary>
        /// Calculates a determinant of specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The determinant of matrix.</returns>
        public static double Determinant(this Matrix matrix)
        {
            return Determinant(matrix, null);
        }

        /// <summary>
        /// Calculates a determinant of specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The determinant of matrix.</returns>
        /// <exception cref="System.ArgumentException">The size of matrices is invalid.</exception>
        public static double Determinant(this Matrix matrix, ExpressionParameters parameters)
        {
            if (matrix.CountOfParams != matrix.SizeOfVectors)
                throw new ArgumentException(Resource.MatrixArgException);

            var array = matrix.ToCalculatedArray(parameters);

            return Determinant_(array, matrix.CountOfParams);
        }

        private static double Determinant_(double[][] matrix, int size)
        {
            if (size == 2)
                return matrix[0][0] * matrix[1][1] - matrix[1][0] * matrix[0][1];

            double det = 0;
            double[][] temp = new double[size - 1][];
            for (int i = 0; i < temp.Length; i++)
                temp[i] = new double[size - 1];

#if NET40_OR_GREATER
            Parallel.For(0, size, k =>
            {
                int n = 0;
                for (int i = 0; i < size; i++)
                {
                    int m = 0;
                    for (int j = 0; j < size; j++)
                    {
                        if (i != k && j != 0)
                        {
                            temp[n][m] = matrix[i][j];
                            m++;
                        }
                    }

                    if (i != k)
                        n++;
                }

                det += matrix[k][0] * Math.Pow(-1, k + 2) * Determinant_(temp, size - 1);
            });
#else
            for (int k = 0; k < size; k++)
            {
                int n = 0;
                for (int i = 0; i < size; i++)
                {
                    int m = 0;
                    for (int j = 0; j < size; j++)
                    {
                        if (i != k && j != 0)
                        {
                            temp[n][m] = matrix[i][j];
                            m++;
                        }
                    }

                    if (i != k)
                        n++;
                }

                det += matrix[k][0] * Math.Pow(-1, k + 2) * Determinant_(temp, size - 1);
            }
#endif

            return det;
        }

    }

}
