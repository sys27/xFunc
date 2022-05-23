// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArtangentTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Artanh(new Number(0.6));
        var result = exp.Execute();
        var expected = AngleValue.Radian(0.6931471805599453);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Artanh(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(0.2290726829685388, result.Real, 15);
        Assert.Equal(1.4099210495965755, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Artanh(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Artanh(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}