// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions;

public class PowTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteTest1()
    {
        var exp = new Pow(Number.Two, new Number(10));
        var expected = new NumberValue(1024.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void NegativeExecuteTest1()
    {
        var exp = new Pow(new Number(-8), new Number(1 / 3.0));
        var expected = new NumberValue(-2.0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void NegativeNumberExecuteTest1()
    {
        var exp = new Pow(new Number(-25), new Number(1 / 2.0));
        var result = (Complex)exp.Execute();
        var expected = new Complex(0, 5);

        Assert.Equal(expected.Real, result.Real, 14);
        Assert.Equal(expected.Imaginary, result.Imaginary, 14);
    }

    [Fact]
    public void NegativeNumberExecuteTest2()
    {
        var exp = new Pow(new Number(-25), new Number(-1 / 2.0));

        Assert.Equal(new Complex(0, -0.2), exp.Execute());
    }

    [Fact]
    public void NegativeNumberExecuteTest3()
    {
        var exp = new Pow(new Number(-5), Number.Two);

        Assert.Equal(new NumberValue(25.0), exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var complex1 = new Complex(3, 2);
        var complex2 = new Complex(4, 5);
        var exp = new Pow(new ComplexNumber(complex1), new ComplexNumber(complex2));

        Assert.Equal(Complex.Pow(complex1, complex2), exp.Execute());
    }

    [Fact]
    public void ExecuteTest3()
    {
        var complex = new Complex(3, 2);
        var exp = new Pow(new ComplexNumber(complex), new Number(10));

        Assert.Equal(Complex.Pow(complex, 10), exp.Execute());
    }

    [Fact]
    public void ExecuteTest4()
    {
        var complex1 = new Complex(-3, 2);
        var complex2 = new Complex(-4, 5);
        var exp = new Pow(new ComplexNumber(complex1), new ComplexNumber(complex2));

        Assert.Equal(Complex.Pow(complex1, complex2), exp.Execute());
    }

    [Fact]
    public void ExecuteTest5()
    {
        var complex = new Complex(-3, 2);
        var exp = new Pow(new ComplexNumber(complex), new Number(10));

        Assert.Equal(Complex.Pow(complex, 10), exp.Execute());
    }

    [Fact]
    public void ExecuteNumberAndComplexTest()
    {
        const int @base = 2;
        var complex = new Complex(3, 2);
        var exp = new Pow(new Number(@base), new ComplexNumber(complex));
        var expected = Complex.Pow(@base, complex);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteRationalAndNumberTest()
    {
        var exp = new Pow(
            new Rational(new Number(2), new Number(3)),
            new Number(3));
        var expected = new RationalValue(8, 27);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteWrongArgumentTypeTest()
        => TestNotSupported(new Pow(Bool.True, Bool.True));

    [Fact]
    public void CloneTest()
    {
        var exp = new Pow(Variable.X, Number.Zero);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}