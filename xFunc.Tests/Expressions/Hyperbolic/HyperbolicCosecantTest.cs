// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicCosecantTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Csch(Number.One);
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(57.29287073437031);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteRadianTest()
    {
        var exp = new Csch(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.8509181282393216);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteDegreeTest()
    {
        var exp = new Csch(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(57.29287073437031);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteGradianTest()
    {
        var exp = new Csch(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(63.65935931824048);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Csch(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(-0.041200986288574125, result.Real, 15);
        Assert.Equal(-0.090473209753207426, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Csch(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Csch(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}