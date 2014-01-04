using System;
using System.Linq;

namespace xFunc.Maths.Expressions.Matrices
{

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

        public static Matrix Transpose(this Vector vector)
        {
            var vectors = new Vector[vector.CountOfParams];
            for (int i = 0; i < vectors.Length; i++)
                vectors[i] = new Vector(new[] { vector[i] });

            return new Matrix(vectors);
        }

        public static IExpression Transpose(this Matrix matrix)
        {
            var result = new Matrix(matrix.SizeOfVectors, matrix.CountOfParams);

            for (int i = 0; i < matrix.CountOfParams; i++)
                for (int j = 0; j < matrix.SizeOfVectors; j++)
                    result[j][i] = matrix[i][j];

            return result;
        }

    }

}
