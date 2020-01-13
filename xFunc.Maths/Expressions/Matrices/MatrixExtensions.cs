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
    public static class MatrixExtensions
    {

        /// <summary>
        /// Calculates the absolute value (norm) of vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>Return the absolute value of vector.</returns>
        public static double Abs(this Vector vector, ExpressionParameters parameters)
        {
            return Math.Sqrt(vector.Arguments.Sum(arg => Math.Pow((double)arg.Execute(parameters), 2)));
        }

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
            if (left.ParametersCount != right.ParametersCount)
                throw new ArgumentException(Resource.MatrixArgException);

            var exps = new IExpression[left.ParametersCount];

            Parallel.For(0, left.ParametersCount,
                i => exps[i] = new Number((double)left.Arguments[i].Execute(parameters) + (double)right.Arguments[i].Execute(parameters))
            );

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
            if (left.ParametersCount != right.ParametersCount)
                throw new ArgumentException(Resource.MatrixArgException);

            var exps = new IExpression[left.ParametersCount];

            Parallel.For(0, left.ParametersCount,
                i => exps[i] = new Number((double)left.Arguments[i].Execute(parameters) - (double)right.Arguments[i].Execute(parameters))
            );

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
            var n = (double)number.Execute(parameters);

            var numbers = (from num in vector.Arguments.AsParallel().AsOrdered()
                            select new Number((double)num.Execute(parameters) * n))
                        .ToArray();

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
            if (left.ParametersCount != right.ParametersCount || left.SizeOfVectors != right.SizeOfVectors)
                throw new ArgumentException(Resource.MatrixArgException);

            var vectors = new Vector[left.ParametersCount];

            Parallel.For(0, left.ParametersCount, i =>
            {
                var exps = new IExpression[left.SizeOfVectors];

                for (var j = 0; j < left.SizeOfVectors; j++)
                    exps[j] = new Number((double)left[i][j].Execute(parameters) + (double)right[i][j].Execute(parameters));

                vectors[i] = new Vector(exps);
            });

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
            if (left.ParametersCount != right.ParametersCount || left.SizeOfVectors != right.SizeOfVectors)
                throw new ArgumentException(Resource.MatrixArgException);

            var vectors = new Vector[left.ParametersCount];

            Parallel.For(0, left.ParametersCount, i =>
            {
                var exps = new IExpression[left.SizeOfVectors];

                for (var j = 0; j < left.SizeOfVectors; j++)
                    exps[j] = new Number((double)left[i][j].Execute(parameters) - (double)right[i][j].Execute(parameters));

                vectors[i] = new Vector(exps);
            });

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
            var n = (double)number.Execute(parameters);

            var result = from v in matrix.Arguments.AsParallel().AsOrdered()
                        select new Vector(
                            (from num in ((Vector)v).Arguments
                            select new Number((double)num.Execute(parameters) * n))
                            .ToArray()
                        );

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
            if (left.SizeOfVectors != right.ParametersCount)
                throw new ArgumentException(Resource.MatrixArgException);

            var result = Matrix.Create(left.ParametersCount, right.SizeOfVectors);

            Parallel.For(0, right.SizeOfVectors, i =>
            {
                for (var j = 0; j < left.ParametersCount; j++)
                {
                    double el = 0;
                    for (var k = 0; k < left.SizeOfVectors; k++)
                        el += (double)left[j][k].Execute(parameters) * (double)right[k][i].Execute(parameters);
                    result[j][i] = new Number(el);
                }
            });

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
        /// Calculates ths dot product of two vectors.
        /// </summary>
        /// <param name="left">The left vector.</param>
        /// <param name="right">The right vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>
        /// The dot product of vectors.
        /// </returns>
        /// <exception cref="System.ArgumentException">The size of vectors is invalid.</exception>
        public static double Mul(this Vector left, Vector right, ExpressionParameters parameters)
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

            Parallel.For(0, vectors.Length, i => vectors[i] = new Vector(new[] { vector[i] }));

            return new Matrix(vectors);
        }

        /// <summary>
        /// Transposes the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <returns>The transposed matrix.</returns>
        public static IExpression Transpose(this Matrix matrix)
        {
            var result = Matrix.Create(matrix.SizeOfVectors, matrix.ParametersCount);

            Parallel.For(0, matrix.ParametersCount, i =>
            {
                for (var j = 0; j < matrix.SizeOfVectors; j++)
                    result[j][i] = matrix[i][j];
            });

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

            var lu = LUPDecompositionInternal(matrix, out var permutation, out var toggle);

            if (lu == null)
                throw new MatrixIsInvalidException();

            double result = toggle;
            for (var i = 0; i < lu.Length; i++)
                result *= lu[i][i];

            return result;
        }

        /// <summary>
        /// Decomposes a matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <param name="permutation">An array of permutations.</param>
        /// <param name="toggle">Used for calculating a determinant.</param>
        /// <returns>Combined Lower and Upper matrices.</returns>
        public static Matrix LUPDecomposition(this Matrix matrix, ExpressionParameters parameters, out int[] permutation, out int toggle)
        {
            if (!matrix.IsSquare)
                throw new ArgumentException(Resource.MatrixArgException);

            var result = LUPDecompositionInternal(matrix.ToCalculatedArray(parameters), out permutation, out toggle);
            var m = Matrix.Create(result.Length, result.Length);
            for (var i = 0; i < result.Length; i++)
                for (var j = 0; j < result.Length; j++)
                    m[i][j] = new Number(result[i][j]);

            return m;
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
        public static Matrix Inverse(this Matrix matrix, ExpressionParameters parameters)
        {
            if (!matrix.IsSquare)
                throw new ArgumentException(Resource.MatrixArgException);

            var size = matrix.ParametersCount;
            var result = InverseInternal(matrix.ToCalculatedArray(parameters));
            var m = Matrix.Create(size, size);
            for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                    m[i][j] = new Number(result[i][j]);

            return m;
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

            var lu = LUPDecompositionInternal(matrix, out var permutation, out var toggle);
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
        /// <param name="left">The vector.</param>
        /// <param name="right">The vector.</param>
        /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
        /// <returns>The cross product.</returns>
        public static Vector Cross(this Vector left, Vector right, ExpressionParameters parameters)
        {
            if (left.ParametersCount != 3 || right.ParametersCount != 3)
                throw new ArgumentException(Resource.VectorCrossException);

            var vector1 = left.ToCalculatedArray(parameters);
            var vector2 = right.ToCalculatedArray(parameters);

            return new Vector(new IExpression[]
            {
                new Number(vector1[1] * vector2[2] - vector1[2] * vector2[1]),
                new Number(vector1[2] * vector2[0] - vector1[0] * vector2[2]),
                new Number(vector1[0] * vector2[1] - vector1[1] * vector2[0])
            });
        }

    }

}