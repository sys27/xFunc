// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;

namespace xFunc.Maths.Expressions.Matrices;

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
    public static object Abs(this Vector vector, ExpressionParameters? parameters)
    {
        var sum = new NumberValue(0);
        foreach (var argument in vector.Arguments)
        {
            var result = argument.Execute(parameters);
            if (result is NumberValue number)
            {
                var pow = NumberValue.Pow(number, new NumberValue(2));
                if (pow is NumberValue powNumber)
                {
                    sum += powNumber;
                    continue;
                }
            }

            throw new InvalidOperationException();
        }

        return NumberValue.Sqrt(sum);
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

        var exps = ImmutableArray.CreateBuilder<IExpression>(left.ParametersCount);

        for (var i = 0; i < left.ParametersCount; i++)
        {
            var leftNumber = (NumberValue)left[i].Execute(parameters);
            var rightNumber = (NumberValue)right[i].Execute(parameters);

            exps.Add(new Number(leftNumber + rightNumber));
        }

        return new Vector(exps.ToImmutableArray());
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

        var exps = ImmutableArray.CreateBuilder<IExpression>(left.ParametersCount);

        for (var i = 0; i < left.ParametersCount; i++)
        {
            var leftNumber = (NumberValue)left[i].Execute(parameters);
            var rightNumber = (NumberValue)right[i].Execute(parameters);

            exps.Add(new Number(leftNumber - rightNumber));
        }

        return new Vector(exps.ToImmutableArray());
    }

    /// <summary>
    /// Multiplies <paramref name="vector"/> by <paramref name="number"/>.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="number">The number.</param>
    /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
    /// <returns>The product of matrices.</returns>
    public static Vector Mul(this Vector vector, NumberValue number, ExpressionParameters? parameters)
    {
        var numbers = ImmutableArray.CreateBuilder<IExpression>(vector.ParametersCount);

        foreach (var argument in vector.Arguments)
            numbers.Add(new Number((NumberValue)argument.Execute(parameters) * number));

        return new Vector(numbers.ToImmutableArray());
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

        var vectors = ImmutableArray.CreateBuilder<Vector>(left.Rows);

        for (var i = 0; i < left.Rows; i++)
        {
            var exps = ImmutableArray.CreateBuilder<IExpression>(left.Columns);

            for (var j = 0; j < left.Columns; j++)
            {
                var leftNumber = (NumberValue)left[i][j].Execute(parameters);
                var rightNumber = (NumberValue)right[i][j].Execute(parameters);

                exps.Add(new Number(leftNumber + rightNumber));
            }

            vectors.Add(new Vector(exps.ToImmutableArray()));
        }

        return new Matrix(vectors.ToImmutableArray());
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

        var vectors = ImmutableArray.CreateBuilder<Vector>(left.Rows);

        for (var i = 0; i < left.Rows; i++)
        {
            var exps = ImmutableArray.CreateBuilder<IExpression>(left.Columns);

            for (var j = 0; j < left.Columns; j++)
            {
                var leftNumber = (NumberValue)left[i][j].Execute(parameters);
                var rightNumber = (NumberValue)right[i][j].Execute(parameters);

                exps.Add(new Number(leftNumber - rightNumber));
            }

            vectors.Add(new Vector(exps.ToImmutableArray()));
        }

        return new Matrix(vectors.ToImmutableArray());
    }

    /// <summary>
    /// Multiplies <paramref name="matrix"/> by <paramref name="number"/>.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="number">The number.</param>
    /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
    /// <returns>The product of matrix and number.</returns>
    public static Matrix Mul(this Matrix matrix, NumberValue number, ExpressionParameters? parameters)
    {
        var vectors = ImmutableArray.CreateBuilder<Vector>(matrix.Rows);

        for (var i = 0; i < matrix.Rows; i++)
        {
            var vector = ImmutableArray.CreateBuilder<IExpression>(matrix.Columns);

            for (var j = 0; j < matrix.Columns; j++)
                vector.Add(new Number((NumberValue)matrix[i][j].Execute(parameters) * number));

            vectors.Add(new Vector(vector.ToImmutableArray()));
        }

        return new Matrix(vectors.ToImmutableArray());
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

        if (rows <= 10)
        {
            var vectors = ImmutableArray.CreateBuilder<Vector>(rows);

            for (var row = 0; row < rows; row++)
                vectors.Add(MulMatrixRow(left, right, parameters, row));

            return new Matrix(vectors.ToImmutableArray());
        }
        else
        {
            var vectors = new Vector[rows];

            Parallel.For(0, rows, row => vectors[row] = MulMatrixRow(left, right, parameters, row));

            return new Matrix(vectors.ToImmutableArray());
        }
    }

    private static Vector MulMatrixRow(
        Matrix left,
        Matrix right,
        ExpressionParameters? parameters,
        int i)
    {
        var columns = right.Columns;
        var vector = ImmutableArray.CreateBuilder<IExpression>(columns);

        for (var j = 0; j < columns; j++)
        {
            var element = new NumberValue(0.0);
            for (var k = 0; k < left.Columns; k++)
            {
                var leftNumber = (NumberValue)left[i][k].Execute(parameters);
                var rightNumber = (NumberValue)right[k][j].Execute(parameters);

                element += leftNumber * rightNumber;
            }

            vector.Add(new Number(element));
        }

        return new Vector(vector.ToImmutableArray());
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
        var matrix = new Matrix(ImmutableArray.Create(left));

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
        var matrix = new Matrix(ImmutableArray.Create(right));

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
    public static NumberValue Mul(this Vector left, Vector right, ExpressionParameters? parameters)
    {
        if (left.ParametersCount != right.ParametersCount)
            throw new ArgumentException(Resource.MatrixArgException);

        var vector1 = left.ToCalculatedArray(parameters);
        var vector2 = right.ToCalculatedArray(parameters);

        var product = new NumberValue(0.0);
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
        var vectors = ImmutableArray.CreateBuilder<Vector>(vector.ParametersCount);

        foreach (var argument in vector.Arguments)
            vectors.Add(new Vector(ImmutableArray.Create(argument)));

        return new Matrix(vectors.ToImmutableArray());
    }

    /// <summary>
    /// Transposes the specified matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns>The transposed matrix.</returns>
    public static IExpression Transpose(this Matrix matrix)
    {
        var vectors = ImmutableArray.CreateBuilder<Vector>(matrix.Columns);

        for (var i = 0; i < matrix.Columns; i++)
        {
            var vector = ImmutableArray.CreateBuilder<IExpression>(matrix.Rows);

            for (var j = 0; j < matrix.Rows; j++)
                vector.Add(matrix[j][i]);

            vectors.Add(new Vector(vector.ToImmutableArray()));
        }

        return new Matrix(vectors.ToImmutableArray());
    }

    /// <summary>
    /// Calculates a determinant of specified matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="parameters">An object that contains all parameters and functions for expressions.</param>
    /// <returns>The determinant of matrix.</returns>
    /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
    public static NumberValue Determinant(this Matrix matrix, ExpressionParameters? parameters)
    {
        if (!matrix.IsSquare)
            throw new ArgumentException(Resource.MatrixArgException);

        var array = matrix.ToCalculatedArray(parameters);

        return DeterminantInternal(array);
    }

    private static NumberValue DeterminantInternal(NumberValue[][] matrix)
    {
        var size = matrix.Length;

        if (size == 1)
            return matrix[0][0];
        if (size == 2)
            return matrix[0][0] * matrix[1][1] - matrix[1][0] * matrix[0][1];

        var lu = LUPDecompositionInternal(matrix, out _, out var toggle);

        if (lu is null)
            throw new MatrixIsInvalidException();

        var result = toggle;
        for (var i = 0; i < lu.Length; i++)
            result *= lu[i][i];

        return result;
    }

    private static NumberValue[][] LUPDecompositionInternal(
        NumberValue[][] matrix,
        out int[] permutation,
        out NumberValue pivotSign)
    {
        var size = matrix.Length;

        permutation = new int[size];
        for (var i = 0; i < size; i++)
            permutation[i] = i;

        pivotSign = new NumberValue(1.0);

        for (var j = 0; j < size - 1; j++)
        {
            var pivotValue = NumberValue.Abs(matrix[j][j]);
            var pivotRow = j;

            for (var row = j + 1; row < size; row++)
            {
                if (matrix[row][j] > pivotValue)
                {
                    pivotValue = matrix[row][j];
                    pivotRow = row;
                }
            }

            if (pivotRow != j)
            {
                (matrix[pivotRow], matrix[j]) = (matrix[j], matrix[pivotRow]);
                (permutation[pivotRow], permutation[j]) = (permutation[j], permutation[pivotRow]);

                pivotSign = -pivotSign;
            }

            if (matrix[j][j] == 0)
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

    private static NumberValue[] HelperSolve(NumberValue[][] lu, NumberValue[] b)
    {
        var n = lu.Length;
        var x = new NumberValue[n];
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
        var vectors = ImmutableArray.CreateBuilder<Vector>(size);

        for (var i = 0; i < size; i++)
        {
            var vector = ImmutableArray.CreateBuilder<IExpression>(size);

            for (var j = 0; j < size; j++)
                vector.Add(new Number(result[i][j]));

            vectors.Add(new Vector(vector.ToImmutableArray()));
        }

        return new Matrix(vectors.ToImmutableArray());
    }

    private static NumberValue[][] InverseInternal(NumberValue[][] matrix)
    {
        var n = matrix.Length;
        var result = new NumberValue[n][];

        for (var i = 0; i < n; i++)
        {
            result[i] = new NumberValue[n];
            for (var j = 0; j < n; j++)
                result[i][j] = matrix[i][j];
        }

        var lu = LUPDecompositionInternal(matrix, out var permutation, out _);
        if (lu is null)
            throw new MatrixIsInvalidException();

        var b = new NumberValue[n];
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < n; j++)
            {
                if (i == permutation[j])
                    b[j] = new NumberValue(1.0);
                else
                    b[j] = new NumberValue(0.0);
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

        return new Vector(ImmutableArray.Create<IExpression>(
            new Number(vector1[1] * vector2[2] - vector1[2] * vector2[1]),
            new Number(vector1[2] * vector2[0] - vector1[0] * vector2[2]),
            new Number(vector1[0] * vector2[1] - vector1[1] * vector2[0])));
    }
}