// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArcosineTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Arcosh(new Number(7));
        var result = exp.Execute();
        var expected = AngleValue.Radian(2.6339157938496336);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arcosh(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(1.9686379257930964, result.Real, 15);
        Assert.Equal(0.606137822387294, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Arcosh(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Arcosh(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}