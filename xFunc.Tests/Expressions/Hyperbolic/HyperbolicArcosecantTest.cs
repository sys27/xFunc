// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArcosecantTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Arcsch(new Number(0.5));
        var result = exp.Execute();
        var expected = AngleValue.Radian(1.2279471772995156);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arcsch(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(0.23133469857397318, result.Real, 15);
        Assert.Equal(-0.15038560432786197, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Arcsch(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Arcsch(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}