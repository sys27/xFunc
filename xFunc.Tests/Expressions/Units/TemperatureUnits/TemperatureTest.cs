// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units.TemperatureUnits;

public class TemperatureTest
{
    [Fact]
    public void EqualNullTest()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();

        Assert.False(exp.Equals(null));
    }

    [Fact]
    public void EqualNullObjectTest()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();

        Assert.False(exp.Equals((object)null));
    }

    [Fact]
    public void EqualSameTest()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();

        Assert.True(exp.Equals(exp));
    }

    [Fact]
    public void EqualSameObjectTest()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();

        Assert.True(exp.Equals((object)exp));
    }

    [Fact]
    public void EqualDiffTypeTest()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();
        var number = Number.One;

        Assert.False(exp.Equals(number));
    }

    [Fact]
    public void ExecuteTest()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();
        var expected = TemperatureValue.Celsius(10);

        Assert.Equal(expected, exp.Execute());
    }

    [Fact]
    public void ExecuteTest2()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();
        var expected = TemperatureValue.Celsius(10);

        Assert.Equal(expected, exp.Execute(null));
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }
}