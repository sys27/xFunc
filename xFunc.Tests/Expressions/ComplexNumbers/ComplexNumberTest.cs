// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.ComplexNumbers;

public class ComplexNumberTest
{
    [Fact]
    public void ExecuteTest()
    {
        var complex = new Complex(5, 2);
        var exp = new ComplexNumber(complex);

        Assert.Equal(complex, exp.Execute());
    }

    [Fact]
    public void ExecuteWithParamsTest()
    {
        var complex = new Complex(5, 2);
        var exp = new ComplexNumber(complex);

        Assert.Equal(complex, exp.Execute(null));
    }

    [Fact]
    public void ValueTest()
    {
        var complex = new Complex(5, 2);
        var exp = new ComplexNumber(complex);

        Assert.Equal(complex, exp.Value);
    }

    [Fact]
    public void CastToComplexTest()
    {
        var complex = new Complex(5, 2);
        var exp = new ComplexNumber(complex);
        var result = (Complex)exp;

        Assert.Equal(complex, result);
    }

    [Fact]
    public void CastToComplexNumberTest()
    {
        var complex = new Complex(5, 2);
        var exp = (ComplexNumber)complex;
        var result = new ComplexNumber(complex);

        Assert.Equal(exp, result);
    }

    [Fact]
    public void EqualsTest()
    {
        var exp1 = new ComplexNumber(new Complex(5, 2));
        var exp2 = new ComplexNumber(new Complex(5, 2));

        Assert.True(exp1.Equals(exp2));
    }

    [Fact]
    public void NotEqualsTest()
    {
        var exp1 = new ComplexNumber(new Complex(5, 2));
        var exp2 = new ComplexNumber(new Complex(3, 2));

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void NotEqualsDiffTypesTest()
    {
        var exp1 = new ComplexNumber(new Complex(5, 2));
        var exp2 = Number.Two;

        Assert.False(exp1.Equals(exp2));
    }

    [Fact]
    public void ComplexNumberAsVariableTest()
    {
        var exp = new Add(
            new Number(3),
            new Mul(
                Number.Two,
                new Variable("i")
            )
        );
        var parameters = new ExpressionParameters();

        var actual = exp.Execute(parameters);
        var expected = new Complex(3, 2);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ComplexExponentialFormTest1()
    {
        const int r = 2;
        const int phi = 3;

        var exp = new Mul(
            new Number(r),
            new Exp(
                new Mul(
                    new Number(phi),
                    new ComplexNumber(Complex.ImaginaryOne)
                )
            )
        );
        var actual = exp.Execute();
        var expected = Complex.FromPolarCoordinates(r, phi);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ComplexExponentialFormTest2()
    {
        const int r = 2;
        const int phi = 3;

        var exp = new Mul(
            new Number(r),
            new Pow(
                new Number(Math.E),
                new Mul(
                    new Number(phi),
                    new ComplexNumber(Complex.ImaginaryOne)
                )
            )
        );
        var actual = exp.Execute();
        var expected = Complex.FromPolarCoordinates(r, phi);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ComplexNumberAnalyzeNull1()
    {
        var exp = new ComplexNumber(3, 2);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void ComplexNumberAnalyzeNull2()
    {
        var exp = new ComplexNumber(3, 2);

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void ConvertToComplexTest()
    {
        ComplexNumber exp = null;
        Assert.Throws<ArgumentNullException>(() => (Complex)exp);
    }
}