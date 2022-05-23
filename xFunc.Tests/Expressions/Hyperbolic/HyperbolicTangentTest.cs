// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicTangentTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Tanh(Number.One);
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.017451520543541533);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteRadianTest()
    {
        var exp = new Tanh(AngleValue.Radian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.7615941559557649);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteDegreeTest()
    {
        var exp = new Tanh(AngleValue.Degree(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.017451520543541533);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteGradianTest()
    {
        var exp = new Tanh(AngleValue.Gradian(1).AsExpression());
        var result = (NumberValue)exp.Execute();
        var expected = new NumberValue(0.015706671467249425);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Tanh(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(1.0032386273536098, result.Real, 15);
        Assert.Equal(-0.0037640256415041864, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Tanh(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Tanh(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}