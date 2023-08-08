// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace xFunc.Maths.Expressions.Matrices;

/// <summary>
/// Represent the Matrix value.
/// </summary>
public readonly struct MatrixValue : IEquatable<MatrixValue>
{
    private readonly VectorValue[] vectors;

    private MatrixValue(VectorValue[] vectors)
    {
        if (vectors.Length == 0)
            throw new ArgumentNullException(nameof(vectors));

        this.vectors = vectors;
    }

    private MatrixValue(NumberValue[][] values)
        => vectors = Unsafe.As<NumberValue[][], VectorValue[]>(ref values);

    /// <summary>
    /// Creates a new instance of <see cref="MatrixValue"/>.
    /// </summary>
    /// <param name="value">The item of a new matrix.</param>
    /// <returns>The matrix.</returns>
    public static MatrixValue Create(NumberValue value)
        => new MatrixValue(new NumberValue[][]
        {
            new NumberValue[] { value },
        });

    /// <summary>
    /// Creates a new instance of <see cref="MatrixValue"/>.
    /// </summary>
    /// <param name="value">The item of a new matrix.</param>
    /// <returns>The matrix.</returns>
    public static MatrixValue Create(VectorValue value)
        => new MatrixValue(new[] { value });

    /// <summary>
    /// Creates a new instance of <see cref="MatrixValue"/>.
    /// </summary>
    /// <param name="vectors">The array.</param>
    /// <returns>The matrix.</returns>
    public static MatrixValue Create(VectorValue[] vectors)
    {
        var copy = new VectorValue[vectors.Length];
        vectors.CopyTo(copy, 0);

        return new MatrixValue(copy);
    }

    /// <summary>
    /// Creates a new instance of <see cref="MatrixValue"/>.
    /// </summary>
    /// <param name="values">The array.</param>
    /// <returns>The matrix.</returns>
    public static MatrixValue Create(NumberValue[][] values)
    {
        var copy = new VectorValue[values.Length];
        for (var i = 0; i < values.Length; i++)
            copy[i] = VectorValue.Create(values[i]);

        return new MatrixValue(copy);
    }

    /// <inheritdoc />
    public bool Equals(MatrixValue other)
        => (vectors, other.vectors) switch
        {
            (null, null) => true,
            (null, _) or (_, null) => false,
            _ => vectors.SequenceEqual(other.vectors),
        };

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is MatrixValue other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(vectors);

    /// <inheritdoc />
    public override string ToString()
    {
        if (vectors is null)
            return "{{}}";

        return new StringBuilder()
            .Append('{')
            .AppendJoin(", ", vectors)
            .Append('}')
            .ToString();
    }

    /// <summary>
    /// Gets the <see cref="VectorValue"/> at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="VectorValue"/>.
    /// </value>
    /// <param name="index">The index.</param>
    /// <returns>The element of matrix.</returns>
    public VectorValue this[int index]
        => vectors[index];

    /// <summary>
    /// Determines whether two specified instances of <see cref="MatrixValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(MatrixValue left, MatrixValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="MatrixValue"/> are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(MatrixValue left, MatrixValue right)
        => !left.Equals(right);

    /// <summary>
    /// Adds two objects of <see cref="MatrixValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static MatrixValue operator +(MatrixValue left, MatrixValue right)
        => Add(left, right);

    /// <summary>
    /// Subtracts two objects of <see cref="MatrixValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static MatrixValue operator -(MatrixValue left, MatrixValue right)
        => Sub(left, right);

    /// <summary>
    /// Multiplies two objects of <see cref="MatrixValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static MatrixValue operator *(MatrixValue left, MatrixValue right)
        => Mul(left, right);

    /// <summary>
    /// Multiplies <see cref="MatrixValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static MatrixValue operator *(MatrixValue left, NumberValue right)
        => Mul(left, right);

    /// <summary>
    /// Multiplies <see cref="MatrixValue"/> and <see cref="VectorValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static MatrixValue operator *(MatrixValue left, VectorValue right)
        => Mul(left, right);

    /// <summary>
    /// Multiplies <see cref="VectorValue"/> and <see cref="MatrixValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static MatrixValue operator *(VectorValue left, MatrixValue right)
        => Mul(left, right);

    /// <summary>
    /// Converts the current matrix to a two dimensional array.
    /// </summary>
    /// <returns>The array.</returns>
    public NumberValue[][] ToArray()
    {
        var copy = new NumberValue[Rows][];
        for (var i = 0; i < copy.Length; i++)
            copy[i] = this[i].ToArray();

        return copy;
    }

    /// <summary>
    /// Gets the count of rows.
    /// </summary>
    public int Rows
        => vectors.Length;

    /// <summary>
    /// Gets the count of columns.
    /// </summary>
    public int Columns
        => vectors[0].Size;

    /// <summary>
    /// Gets a value indicating whether matrix is square.
    /// </summary>
    public bool IsSquare
        => Rows == Columns;

    /// <summary>
    /// Adds the <paramref name="right"/> matrix to the <paramref name="left"/> matrix.
    /// </summary>
    /// <param name="left">The left matrix.</param>
    /// <param name="right">The right matrix.</param>
    /// <returns>The sum of matrices.</returns>
    /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
    public static MatrixValue Add(MatrixValue left, MatrixValue right)
    {
        if (left.Rows != right.Rows || left.Columns != right.Columns)
            throw new ArgumentException(Resource.MatrixArgException);

        var vectors = new VectorValue[left.Rows];

        for (var i = 0; i < vectors.Length; i++)
        {
            var exps = new NumberValue[left.Columns];

            for (var j = 0; j < exps.Length; j++)
                exps[j] = left[i][j] + right[i][j];

            vectors[i] = Unsafe.As<NumberValue[], VectorValue>(ref exps);
        }

        return new MatrixValue(vectors);
    }

    /// <summary>
    /// Subtracts the <paramref name="right"/> matrix from the <paramref name="left"/> matrix.
    /// </summary>
    /// <param name="left">The left matrix.</param>
    /// <param name="right">The right matrix.</param>
    /// <returns>The difference of matrices.</returns>
    /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
    public static MatrixValue Sub(MatrixValue left, MatrixValue right)
    {
        if (left.Rows != right.Rows || left.Columns != right.Columns)
            throw new ArgumentException(Resource.MatrixArgException);

        var vectors = new VectorValue[left.Rows];

        for (var i = 0; i < vectors.Length; i++)
        {
            var exps = new NumberValue[left.Columns];

            for (var j = 0; j < exps.Length; j++)
                exps[j] = left[i][j] - right[i][j];

            vectors[i] = Unsafe.As<NumberValue[], VectorValue>(ref exps);
        }

        return new MatrixValue(vectors);
    }

    /// <summary>
    /// Multiplies <paramref name="matrix"/> by <paramref name="number"/>.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <param name="number">The number.</param>
    /// <returns>The product of matrix and number.</returns>
    public static MatrixValue Mul(MatrixValue matrix, NumberValue number)
    {
        var vectors = new VectorValue[matrix.Rows];

        for (var i = 0; i < vectors.Length; i++)
        {
            var vector = new NumberValue[matrix.Columns];

            for (var j = 0; j < vector.Length; j++)
                vector[j] = matrix[i][j] * number.Number;

            vectors[i] = Unsafe.As<NumberValue[], VectorValue>(ref vector);
        }

        return new MatrixValue(vectors);
    }

    /// <summary>
    /// Multiplies the <paramref name="left" /> matrix by the <paramref name="right" /> matrix.
    /// </summary>
    /// <param name="left">The left matrix.</param>
    /// <param name="right">The right matrix.</param>
    /// <returns>The product of matrices.</returns>
    /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
    public static MatrixValue Mul(MatrixValue left, MatrixValue right)
    {
        if (left.Columns != right.Rows)
            throw new ArgumentException(Resource.MatrixArgException);

        var vectors = new VectorValue[left.Rows];

        if (vectors.Length <= 10)
        {
            for (var row = 0; row < vectors.Length; row++)
                vectors[row] = MulMatrixRow(left, right, row);
        }
        else
        {
            Parallel.For(0, vectors.Length, row => vectors[row] = MulMatrixRow(left, right, row));
        }

        return new MatrixValue(vectors);
    }

    private static VectorValue MulMatrixRow(MatrixValue left, MatrixValue right, int i)
    {
        var vector = new NumberValue[right.Columns];

        for (var j = 0; j < vector.Length; j++)
        {
            var element = NumberValue.Zero;
            for (var k = 0; k < left.Columns; k++)
                element += left[i][k] * right[k][j];

            vector[j] = element;
        }

        return Unsafe.As<NumberValue[], VectorValue>(ref vector);
    }

    /// <summary>
    /// Multiplies the <paramref name="left" /> vector by the <paramref name="right" /> matrix.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right matrix.</param>
    /// <returns>The product of matrices.</returns>
    /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
    public static MatrixValue Mul(VectorValue left, MatrixValue right)
    {
        var matrix = Create(left);

        return matrix * right;
    }

    /// <summary>
    /// Multiplies the <paramref name="left" /> matrix by the <paramref name="right" /> vector.
    /// </summary>
    /// <param name="left">The left matrix.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The product of matrices.</returns>
    /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
    public static MatrixValue Mul(MatrixValue left, VectorValue right)
    {
        var matrix = Create(right);

        return left * matrix;
    }

    /// <summary>
    /// Transposes the specified matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns>The transposed matrix.</returns>
    public static MatrixValue Transpose(MatrixValue matrix)
    {
        var vectors = new VectorValue[matrix.Columns];

        for (var i = 0; i < vectors.Length; i++)
        {
            var vector = new NumberValue[matrix.Rows];

            for (var j = 0; j < vector.Length; j++)
                vector[j] = matrix[j][i];

            vectors[i] = Unsafe.As<NumberValue[], VectorValue>(ref vector);
        }

        return new MatrixValue(vectors);
    }

    /// <summary>
    /// Calculates a determinant of specified matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns>The determinant of matrix.</returns>
    /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
    public static NumberValue Determinant(MatrixValue matrix)
    {
        if (!matrix.IsSquare)
            throw new ArgumentException(Resource.MatrixArgException);

        var size = matrix.Rows;

        if (size == 1)
            return matrix[0][0];
        if (size == 2)
            return matrix[0][0] * matrix[1][1] - matrix[1][0] * matrix[0][1];

        var (lu, _, toggle) = LUPDecomposition(matrix);

        if (lu is null)
            throw new InvalidMatrixException();

        var result = toggle;
        for (var i = 0; i < lu.Length; i++)
            result *= lu[i][i];

        return result;
    }

    private static (NumberValue[][] LU, int[] Permutations, NumberValue Sign) LUPDecomposition(MatrixValue matrix)
    {
        var size = matrix.Rows;
        var pivotSign = NumberValue.One;
        var permutations = new int[size];
        for (var i = 0; i < size; i++)
            permutations[i] = i;

        var result = matrix.ToArray();

        for (var j = 0; j < size - 1; j++)
        {
            var pivotValue = NumberValue.Abs(result[j][j]);
            var pivotRow = j;

            // select pivot item
            for (var row = j + 1; row < size; row++)
            {
                if (NumberValue.Abs(result[row][j]) > NumberValue.Abs(pivotValue))
                {
                    pivotValue = result[row][j];
                    pivotRow = row;
                }
            }

            // swap rows, if needed
            if (pivotRow != j)
            {
                (result[pivotRow], result[j]) = (result[j], result[pivotRow]);
                (permutations[pivotRow], permutations[j]) = (permutations[j], permutations[pivotRow]);

                pivotSign = -pivotSign;
            }

            if (result[j][j] == 0)
                throw new InvalidMatrixException();

            // calculate L and U
            for (var i = j + 1; i < size; i++)
            {
                result[i][j] /= result[j][j];
                for (var k = j + 1; k < size; k++)
                    result[i][k] -= result[i][j] * result[j][k];
            }
        }

        return (result, permutations, pivotSign);
    }

    /// <summary>
    /// Inverts a matrix.
    /// </summary>
    /// <param name="matrix">The matrix.</param>
    /// <returns>An inverse matrix.</returns>
    public static MatrixValue Inverse(MatrixValue matrix)
    {
        if (!matrix.IsSquare)
            throw new ArgumentException(Resource.MatrixArgException);

        var (lu, permutations, _) = LUPDecomposition(matrix);
        if (lu is null)
            throw new InvalidMatrixException();

        // create permutation matrix
        var rows = matrix.Rows;
        var result = new NumberValue[rows][];
        for (var i = 0; i < rows; i++)
        {
            var vector = new NumberValue[matrix.Columns];
            vector[permutations[i]] = NumberValue.One;

            result[i] = vector;
        }

        // calculate inverse matrix
        for (var k = 0; k < rows; k++)
        for (var i = k + 1; i < rows; i++)
        for (var j = 0; j < rows; j++)
            result[i][j] -= result[k][j] * lu[i][k];

        for (var k = rows - 1; k >= 0; k--)
        {
            for (var j = 0; j < rows; j++)
                result[k][j] /= lu[k][k];

            for (var i = 0; i < k; i++)
            for (var j = 0; j < rows; j++)
                result[i][j] -= result[k][j] * lu[i][k];
        }

        return new MatrixValue(result);
    }
}