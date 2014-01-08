// Copyright 2012-2013 Dmitry Kischenko
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

namespace xFunc.Maths.Expressions.Matrices
{

    /// <summary>
    /// Provides extention methods for matrices and vectors.
    /// </summary>
    public static class MatrixExtentions
    {

        public static Vector Add(this Vector left, Vector right)
        {
            return left.Add(right, null);
        }

        public static Vector Add(this Vector left, Vector right, ExpressionParameters parameters)
        {
            if (left.CountOfParams != right.CountOfParams)
                // todo: message
                throw new VectorIsInvalidException();

            var exps = new IExpression[left.CountOfParams];
            for (int i = 0; i < left.CountOfParams; i++)
                exps[i] = new Number((double)left.Arguments[i].Calculate(parameters) + (double)right.Arguments[i].Calculate(parameters));

            return new Vector(exps);
        }

        public static Vector Sub(this Vector left, Vector right)
        {
            return left.Sub(right, null);
        }

        public static Vector Sub(this Vector left, Vector right, ExpressionParameters parameters)
        {
            if (left.CountOfParams != right.CountOfParams)
                // todo: message
                throw new VectorIsInvalidException();

            var exps = new IExpression[left.CountOfParams];
            for (int i = 0; i < left.CountOfParams; i++)
                exps[i] = new Number((double)left.Arguments[i].Calculate(parameters) - (double)right.Arguments[i].Calculate(parameters));

            return new Vector(exps);
        }

        public static Vector Mul(this Vector vector, IExpression number)
        {
            return vector.Mul(number, null);
        }

        public static Vector Mul(this Vector vector, IExpression number, ExpressionParameters parameters)
        {
            var numbers = (from num in vector.Arguments
                           select new Number((double)num.Calculate(parameters) * (double)number.Calculate(parameters)))
                          .ToArray();

            return new Vector(numbers);
        }

        public static Matrix Add(this Matrix left, Matrix right)
        {
            return left.Add(right, null);
        }

        public static Matrix Add(this Matrix left, Matrix right, ExpressionParameters parameters)
        {
            if (left.CountOfParams != right.CountOfParams || left.SizeOfVectors != right.SizeOfVectors)
                // todo: message
                throw new MatrixIsInvalidException();

            var vectors = new Vector[left.CountOfParams];
            for (int i = 0; i < left.CountOfParams; i++)
            {
                var exps = new IExpression[left.SizeOfVectors];

                for (int j = 0; j < left.SizeOfVectors; j++)
                    exps[j] = new Number((double)left[i][j].Calculate(parameters) + (double)right[i][j].Calculate(parameters));

                vectors[i] = new Vector(exps);
            }

            return new Matrix(vectors);
        }

        public static Matrix Sub(this Matrix left, Matrix right)
        {
            return left.Sub(right, null);
        }

        public static Matrix Sub(this Matrix left, Matrix right, ExpressionParameters parameters)
        {
            if (left.CountOfParams != right.CountOfParams || left.SizeOfVectors != right.SizeOfVectors)
                // todo: message
                throw new MatrixIsInvalidException();

            var vectors = new Vector[left.CountOfParams];
            for (int i = 0; i < left.CountOfParams; i++)
            {
                var exps = new IExpression[left.SizeOfVectors];

                for (int j = 0; j < left.SizeOfVectors; j++)
                    exps[j] = new Number((double)left[i][j].Calculate(parameters) - (double)right[i][j].Calculate(parameters));

                vectors[i] = new Vector(exps);
            }

            return new Matrix(vectors);
        }

        public static Matrix Mul(this Matrix vector, IExpression number)
        {
            return vector.Mul(number, null);
        }

        public static Matrix Mul(this Matrix vector, IExpression number, ExpressionParameters parameters)
        {
            var matrix = vector.Arguments
                               .Select(v =>
                               {
                                   var vect = ((Vector)v).Arguments
                                                         .Select(num => new Number((double)num.Calculate(parameters) * (double)number.Calculate(parameters)))
                                                         .ToArray();

                                   return new Vector(vect);
                               })
                               .ToArray();

            return new Matrix(matrix);
        }

        public static Matrix Mul(this Matrix left, Matrix right)
        {
            return left.Mul(right, null);
        }

        public static Matrix Mul(this Matrix left, Matrix right, ExpressionParameters parameters)
        {
            if (left.SizeOfVectors != right.CountOfParams)
                // todo: message...
                throw new ArgumentException();

            var result = new Matrix(left.CountOfParams, right.SizeOfVectors);
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

            return result;
        }

        public static Matrix Mul(this Vector left, Matrix right)
        {
            return left.Mul(right, null);
        }

        public static Matrix Mul(this Vector left, Matrix right, ExpressionParameters parameters)
        {
            var matrix = new Matrix(new[] { left });

            return matrix.Mul(right, parameters);
        }

        public static Matrix Mul(this Matrix left, Vector right)
        {
            return left.Mul(right, null);
        }

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
            for (int i = 0; i < vectors.Length; i++)
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
            var result = new Matrix(matrix.SizeOfVectors, matrix.CountOfParams);

            for (int i = 0; i < matrix.CountOfParams; i++)
                for (int j = 0; j < matrix.SizeOfVectors; j++)
                    result[j][i] = matrix[i][j];

            return result;
        }

        public static double Determinant(this Matrix matrix)
        {
            return Determinant(matrix, null);
        }

        public static double Determinant(this Matrix matrix, ExpressionParameters parameters)
        {
            if (matrix.CountOfParams != matrix.SizeOfVectors)
                // todo: message...
                throw new ArgumentException();

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

            return det;
        }

    }

}
