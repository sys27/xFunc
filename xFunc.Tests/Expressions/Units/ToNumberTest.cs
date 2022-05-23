// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace xFunc.Tests.Expressions.Units;

public class ToNumberTest
{
    [Fact]
    public void ExecuteAngleTest()
    {
        var exp = new ToNumber(AngleValue.Degree(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecutePowerTest()
    {
        var exp = new ToNumber(PowerValue.Watt(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTemperatureTest()
    {
        var exp = new ToNumber(TemperatureValue.Celsius(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteMassTest()
    {
        var exp = new ToNumber(MassValue.Gram(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteLengthTest()
    {
        var exp = new ToNumber(LengthValue.Meter(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteTimeTest()
    {
        var exp = new ToNumber(TimeValue.Second(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteAreaTest()
    {
        var exp = new ToNumber(AreaValue.Meter(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteVolumeTest()
    {
        var exp = new ToNumber(VolumeValue.Meter(10).AsExpression());
        var actual = exp.Execute();
        var expected = new NumberValue(10.0);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ExecuteBoolTest()
    {
        Assert.Throws<ResultIsNotSupportedException>(() => new ToNumber(Bool.False).Execute());
    }

    [Fact]
    public void NullAnalyzerTest1()
    {
        var exp = new ToNumber(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string>(null));
    }

    [Fact]
    public void NullAnalyzerTest2()
    {
        var exp = new ToNumber(new Number(10));

        Assert.Throws<ArgumentNullException>(() => exp.Analyze<string, object>(null, null));
    }

    [Fact]
    public void CloneTest()
    {
        var exp = new ToNumber(AngleValue.Degree(10).AsExpression());
        var clone = exp.Clone();

        Assert.Equal(exp, clone);
    }
}