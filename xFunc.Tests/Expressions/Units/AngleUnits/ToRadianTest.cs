// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.AngleUnits;

public class ToRadianTest
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new ToRadian(new Number(10));
        var actual = exp.Execute();
        var expected = AngleValue.Radian(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteAngleTest()
    {
        var exp = new ToRadian(AngleValue.Radian(10).AsExpression());
        var actual = exp.Execute();
        var expected = AngleValue.Radian(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteBoolTest()
    {
        Assert.Throws<ResultIsNotSupportedException>(() => new ToRadian(Bool.False).Execute());
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new ToRadian(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new ToRadian(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new ToRadian(new Number(10));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}