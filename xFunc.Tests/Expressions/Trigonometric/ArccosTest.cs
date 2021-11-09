// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArccosTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Arccos(Number.One);
        var expected = AngleValue.Radian(0);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arccos(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(0.60613782238729386, result.Real, 15);
        Assert.Equal(-1.9686379257930964, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Arccos(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Arccos(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}