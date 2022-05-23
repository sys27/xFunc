// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.AngleUnits;

public class ToDegreeTest
{
    [Fact]
    public void ExecuteNumberTest()
    {
        var exp = new ToDegree(new Number(10));
        var actual = exp.Execute();
        var expected = AngleValue.Degree(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteAngleTest()
    {
        var exp = new ToDegree(AngleValue.Degree(10).AsExpression());
        var actual = exp.Execute();
        var expected = AngleValue.Degree(10);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteBoolTest()
    {
        Assert.Throws<ResultIsNotSupportedException>(() => new ToDegree(Bool.False).Execute());
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new ToDegree(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new ToDegree(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new ToDegree(new Number(10));
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}