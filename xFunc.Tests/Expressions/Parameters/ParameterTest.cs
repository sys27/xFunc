// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Parameters;

public class ParameterTest
{
    [Fact]
    public void DoubleCtor()
    {
        var value = 1.0;
        var x = new Parameter("x", value);
        var expected = new NumberValue(value);

        Assert.Equal(expected, x.Value);
    }

    [Fact]
    public void NumberCtor()
    {
        var value = new NumberValue(1.0);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void AngleValueCtor()
    {
        var value = AngleValue.Degree(1.0);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void PowerValueCtor()
    {
        var value = PowerValue.Watt(1.0);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void TemperatureValueCtor()
    {
        var value = TemperatureValue.Celsius(10);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void MassValueCtor()
    {
        var value = MassValue.Gram(10);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void LengthValueCtor()
    {
        var value = LengthValue.Meter(10);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void TimeValueCtor()
    {
        var value = TimeValue.Second(10);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void AreaValueCtor()
    {
        var value = AreaValue.Meter(10);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void VolumeValueCtor()
    {
        var value = VolumeValue.Meter(10);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void ComplexCtor()
    {
        var value = new Complex(1, 2);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void BoolCtor()
    {
        var value = true;
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void VectorCtor()
    {
        var value = VectorValue.Create(NumberValue.One);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void MatrixCtor()
    {
        var value = MatrixValue.Create(NumberValue.One);
        var x = new Parameter("x", value);

        Assert.Equal(value, x.Value);
    }

    [Fact]
    public void StringCtor()
    {
        var str = "hello";
        var x = new Parameter("x", str);

        Assert.Equal(str, x.Value);
    }

    [Fact]
    public void NullEqual()
    {
        var parameter = new Parameter("x", 1);
        var isEqual = parameter.Equals(null);

        Assert.False(isEqual);
    }

    [Fact]
    public void RefEqualEqual()
    {
        var parameter = new Parameter("x", 1);
        var isEqual = parameter.Equals(parameter);

        Assert.True(isEqual);
    }

    [Fact]
    public void EqualObjectEqual()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("x", 1);
        var isEqual = parameter1.Equals((object)parameter2);

        Assert.True(isEqual);
    }

    [Fact]
    public void OtherType()
    {
        var parameter = new Parameter("x", 1);
        var obj = new object();
        var isEqual = parameter.Equals(obj);

        Assert.False(isEqual);
    }

    [Fact]
    public void EqualParameters()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("x", 1);
        var isEqual = parameter1.Equals(parameter2);

        Assert.True(isEqual);
    }

    [Fact]
    public void NotEqualKey()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("y", 1);
        var isEqual = parameter1.Equals(parameter2);

        Assert.False(isEqual);
    }

    [Fact]
    public void NotEqualValue()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("x", 2);
        var isEqual = parameter1.Equals(parameter2);

        Assert.False(isEqual);
    }

    [Fact]
    public void EqualOperatorTest()
    {
        var x = new Parameter("x", 1);
        var y = new Parameter("x", 1);

        Assert.True(x == y);
    }

    [Fact]
    public void NotEqualOperatorTest()
    {
        var x = new Parameter("x", 1);
        var y = new Parameter("x", 2);

        Assert.True(x != y);
    }

    [Fact]
    public void GreaterTest()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("a", 2);

        Assert.True(parameter1 > parameter2);
    }

    [Fact]
    public void GreaterLeftNullTest()
    {
        var parameter2 = new Parameter("a", 2);

        Assert.False(null > parameter2);
    }

    [Fact]
    public void GreaterRightNullTest()
    {
        var parameter1 = new Parameter("x", 1);

        Assert.False(parameter1 > null);
    }

    [Fact]
    public void LessTest()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("y", 2);

        Assert.True(parameter1 < parameter2);
    }

    [Fact]
    public void LessLeftNullTest()
    {
        var parameter2 = new Parameter("y", 2);

        Assert.False(null < parameter2);
    }

    [Fact]
    public void LessRightNullTest()
    {
        var parameter1 = new Parameter("x", 1);

        Assert.False(parameter1 < null);
    }

    [Fact]
    public void GreaterOrEqualTest()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("a", 2);

        Assert.True(parameter1 >= parameter2);
    }

    [Fact]
    public void LessOrEqualTest()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("y", 2);

        Assert.True(parameter1 <= parameter2);
    }

    [Fact]
    public void CompareToNullTest()
    {
        var parameter = new Parameter("x", 1);
        var result = parameter.CompareTo(null);

        Assert.Equal(1, result);
    }

    [Fact]
    public void EmptyKeyTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Parameter(string.Empty, 1.0));
    }

    [Fact]
    public void SetNullValueTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Parameter("x", (string)null));
    }

    [Fact]
    public void EditConstantParameterTest()
    {
        var parameter = new Parameter("x", 1.0, ParameterType.Constant);

        Assert.Throws<ParameterIsReadOnlyException>(() => parameter.Value = 3.0);
    }

    [Fact]
    public void EditReadonlyParameterTest()
    {
        var parameter = new Parameter("x", 1.0, ParameterType.ReadOnly);

        Assert.Throws<ParameterIsReadOnlyException>(() => parameter.Value = 3.0);
    }

    [Fact]
    public void ToStringTest()
    {
        var parameter = new Parameter("x", 1, ParameterType.Constant);
        var str = parameter.ToString();
        var expected = "x: 1 (Constant)";

        Assert.Equal(expected, str);
    }
}