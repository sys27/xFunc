// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArcsinTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Arcsin(Number.One);
        var result = exp.Execute();
        var expected = AngleValue.Radian(1.5707963267948966);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arcsin(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(0.96465850440760248, result.Real, 14);
        Assert.Equal(1.9686379257930975, result.Imaginary, 14);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Arcsin(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Arcsin(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}