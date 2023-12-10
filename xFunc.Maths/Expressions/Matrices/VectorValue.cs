// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using Simd = System.Numerics;

namespace xFunc.Maths.Expressions.Matrices;

/// <summary>
/// Represent the Vector value.
/// </summary>
public readonly struct VectorValue : IEquatable<VectorValue>, IEnumerable<NumberValue>
{
    private readonly NumberValue[] array;

    private VectorValue(NumberValue[] array)
    {
        if (array.Length == 0)
            throw new ArgumentNullException(nameof(array));

        this.array = array;
    }

    /// <summary>
    /// Creates a new instance of <see cref="VectorValue"/>.
    /// </summary>
    /// <param name="value">The item of a new vector.</param>
    /// <returns>The vector.</returns>
    public static VectorValue Create(NumberValue value)
        => new VectorValue(new[] { value });

    /// <summary>
    /// Creates a new instance of <see cref="VectorValue"/>.
    /// </summary>
    /// <param name="array">The array.</param>
    /// <returns>The vector.</returns>
    public static VectorValue Create(params NumberValue[] array)
    {
        var copy = new NumberValue[array.Length];
        array.CopyTo(copy, 0);

        return new VectorValue(copy);
    }

    /// <inheritdoc />
    public bool Equals(VectorValue other)
        => (array, other.array) switch
        {
            (null, null) => true,
            (null, _) or (_, null) => false,
            _ => array.SequenceEqual(other.array),
        };

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is VectorValue other && Equals(other);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
        => HashCode.Combine(array);

    /// <inheritdoc />
    public override string ToString()
    {
        if (array is null)
            return "{}";

        return new StringBuilder()
            .Append('{')
            .AppendJoin(", ", array)
            .Append('}')
            .ToString();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <inheritdoc />
    public IEnumerator<NumberValue> GetEnumerator()
        => array.AsEnumerable().GetEnumerator();

    /// <summary>
    /// Gets the <see cref="double"/> at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="double"/>.
    /// </value>
    /// <param name="index">The index.</param>
    /// <returns>The argument.</returns>
    public NumberValue this[int index]
        => array[index];

    /// <summary>
    /// Determines whether two specified instances of <see cref="VectorValue"/> are equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator ==(VectorValue left, VectorValue right)
        => left.Equals(right);

    /// <summary>
    /// Determines whether two specified instances of <see cref="VectorValue"/> are not equal.
    /// </summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>; otherwise, <c>false</c>.</returns>
    public static bool operator !=(VectorValue left, VectorValue right)
        => !left.Equals(right);

    /// <summary>
    /// Adds two objects of <see cref="VectorValue"/>.
    /// </summary>
    /// <param name="left">The first object to add.</param>
    /// <param name="right">The second object to add.</param>
    /// <returns>An object that is the sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static VectorValue operator +(VectorValue left, VectorValue right)
        => Add(left, right);

    /// <summary>
    /// Subtracts two objects of <see cref="VectorValue"/>.
    /// </summary>
    /// <param name="left">The first object to sub.</param>
    /// <param name="right">The second object to sub.</param>
    /// <returns>An object that is the difference of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static VectorValue operator -(VectorValue left, VectorValue right)
        => Sub(left, right);

    /// <summary>
    /// Multiplies two objects of <see cref="VectorValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static NumberValue operator *(VectorValue left, VectorValue right)
        => Mul(left, right);

    /// <summary>
    /// Multiplies <see cref="VectorValue"/> and <see cref="NumberValue"/>.
    /// </summary>
    /// <param name="left">The first object to multiply.</param>
    /// <param name="right">The second object to multiply.</param>
    /// <returns>An object that is the product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static VectorValue operator *(VectorValue left, NumberValue right)
        => Mul(left, right);

    /// <summary>
    /// Converts the current vector to an array.
    /// </summary>
    /// <returns>The array.</returns>
    public NumberValue[] ToArray()
    {
        var copy = new NumberValue[array.Length];
        array.CopyTo(copy, 0);

        return copy;
    }

    /// <summary>
    /// Computes the average of a sequence of numeric values.
    /// </summary>
    /// <returns>The average value.</returns>
    public NumberValue Average()
        => Sum() / Size;

    /// <summary>
    /// Computes the sum of a sequence of numeric values.
    /// </summary>
    /// <returns>The sum of the values in the vector.</returns>
    public NumberValue Sum()
    {
        var sum = 0.0;

        for (var i = 0; i < array.Length; i++)
            sum += array[i].Number;

        return new NumberValue(sum);
    }

    /// <summary>
    /// Gets the size of the current vector.
    /// </summary>
    public int Size
        => array.Length;

    /// <summary>
    /// Calculates the absolute value (norm) of vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>Return the absolute value of vector.</returns>
    public static object Abs(VectorValue vector)
    {
        var size = Simd.Vector<double>.Count;
        var i = 0;
        var sum = 0.0;

        if (Simd.Vector.IsHardwareAccelerated && vector.Size >= size)
        {
            var span = Unsafe.As<double[]>(vector.array);

            var v = Simd.Vector<double>.Zero;

            for (; i <= span.Length - size; i += size)
            {
                var chunkVector = new Simd.Vector<double>(span, i);
                v += chunkVector * chunkVector;
            }

            sum = Simd.Vector.Sum(v);
        }

        for (; i < vector.Size; i++)
            sum += vector[i].Number * vector[i].Number;

        return NumberValue.Sqrt(new NumberValue(sum));
    }

    /// <summary>
    /// Adds the <paramref name="right"/> vector to the <paramref name="left"/> vector.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The sum of matrices.</returns>
    /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
    public static VectorValue Add(VectorValue left, VectorValue right)
    {
        if (left.Size != right.Size)
            throw new ArgumentException(Resource.MatrixArgException);

        var exps = new NumberValue[left.Size];
        for (var i = 0; i < exps.Length; i++)
            exps[i] = left[i] + right[i];

        return new VectorValue(exps);
    }

    /// <summary>
    /// Subtracts the <paramref name="right"/> vector from the <paramref name="left"/> vector.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The difference of matrices.</returns>
    /// <exception cref="ArgumentException">The size of matrices is invalid.</exception>
    public static VectorValue Sub(VectorValue left, VectorValue right)
    {
        if (left.Size != right.Size)
            throw new ArgumentException(Resource.MatrixArgException);

        var exps = new NumberValue[left.Size];
        for (var i = 0; i < exps.Length; i++)
            exps[i] = left[i] - right[i];

        return new VectorValue(exps);
    }

    /// <summary>
    /// Multiplies <paramref name="vector"/> by <paramref name="number"/>.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <param name="number">The number.</param>
    /// <returns>The product of matrices.</returns>
    public static VectorValue Mul(VectorValue vector, NumberValue number)
    {
        var numbers = new NumberValue[vector.Size];
        for (var i = 0; i < vector.Size; i++)
            numbers[i] = vector[i] * number.Number;

        return new VectorValue(numbers);
    }

    /// <summary>
    /// Calculates ths dot product of two vectors.
    /// </summary>
    /// <param name="left">The left vector.</param>
    /// <param name="right">The right vector.</param>
    /// <returns>The dot product of vectors.</returns>
    /// <exception cref="ArgumentException">The size of vectors is invalid.</exception>
    public static NumberValue Mul(VectorValue left, VectorValue right)
    {
        if (left.Size != right.Size)
            throw new ArgumentException(Resource.MatrixArgException);

        var size = Simd.Vector<double>.Count;
        var i = 0;
        var product = 0.0;

        if (Simd.Vector.IsHardwareAccelerated && left.Size >= size)
        {
            var leftSpan = Unsafe.As<double[]>(left.array).AsSpan();
            var rightSpan = Unsafe.As<double[]>(right.array).AsSpan();

            var v = Simd.Vector<double>.Zero;

            for (; i <= leftSpan.Length - size; i += size)
            {
                var leftV = new Simd.Vector<double>(leftSpan[i..]);
                var rightV = new Simd.Vector<double>(rightSpan[i..]);

                v += leftV * rightV;
            }

            product = Simd.Vector.Sum(v);
        }

        for (; i < left.Size; i++)
            product += left[i].Number * right[i].Number;

        return new NumberValue(product);
    }

    /// <summary>
    /// Transposes the specified vector.
    /// </summary>
    /// <param name="vector">The vector.</param>
    /// <returns>The transposed matrix.</returns>
    public static MatrixValue Transpose(VectorValue vector)
    {
        var vectors = new VectorValue[vector.Size];

        for (var i = 0; i < vectors.Length; i++)
            vectors[i] = Create(vector[i]);

        return Unsafe.As<VectorValue[], MatrixValue>(ref vectors);
    }

    /// <summary>
    /// Computes the cross product of two vectors.
    /// </summary>
    /// <param name="left">The multiplier vector.</param>
    /// <param name="right">The multiplicand vector.</param>
    /// <returns>The cross product.</returns>
    public static VectorValue Cross(VectorValue left, VectorValue right)
    {
        if (left.Size != 3 || right.Size != 3)
            throw new ArgumentException(Resource.VectorCrossException);

        return new VectorValue(new[]
        {
            left[1] * right[2] - left[2] * right[1],
            left[2] * right[0] - left[0] * right[2],
            left[0] * right[1] - left[1] * right[0],
        });
    }
}