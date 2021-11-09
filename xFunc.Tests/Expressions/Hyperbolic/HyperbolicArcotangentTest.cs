// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Hyperbolic;

public class HyperbolicArcotangentTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Arcoth(new Number(7));
        var result = exp.Execute();
        var expected = AngleValue.Radian(0.14384103622589042);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arcoth(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(0.2290726829685388, result.Real, 15);
        Assert.Equal(-0.16087527719832109, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Arcoth(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Arcoth(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}