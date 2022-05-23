// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicCotangentTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Coth(Number.One);
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(57.30159715911299);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteRadianTest()
    {
        var exp = new Coth(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(1.3130352854993312);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteDegreeTest()
    {
        var exp = new Coth(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(57.30159715911299);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteGradianTest()
    {
        var exp = new Coth(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(63.66721313838742);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Coth(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(0.99675779656935837, result.Real, 15);
        Assert.Equal(0.0037397103763368955, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Coth(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Coth(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}