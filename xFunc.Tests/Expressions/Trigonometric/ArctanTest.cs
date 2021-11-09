// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Trigonometric;

public class ArctanTest : BaseExpressionTests
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new Arctan(Number.One);
        var result = exp.Execute();
        var expected = AngleValue.Radian(0.7853981633974483);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ExecuteComplexNumberTest()
    {
        var complex = new Complex(3, 2);
        var exp = new Arctan(new ComplexNumber(complex));
        var result = (Complex)exp.Execute();

        Assert.Equal(1.3389725222944935, result.Real, 15);
        Assert.Equal(0.14694666622552977, result.Imaginary, 15);
    }

    [Fact]
    public void ExecuteTestException()
        => TestNotSupported(new Arctan(Bool.False));

    [Fact]
    public void CloneTest()
    {
        var exp = new Arctan(Number.One);
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}