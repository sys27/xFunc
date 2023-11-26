// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace xFunc.Tests.Expressions.Parameters;

public class ParameterTest
{
    [Test]
    public void DoubleCtor()
    {
        var value = 1.0;
        var x = new Parameter("x", value);
        var expected = new NumberValue(value);

        Assert.That(x.Value.Value, Is.EqualTo(expected));
    }

    [Test]
    public void NumberCtor()
    {
        var value = new NumberValue(1.0);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void AngleValueCtor()
    {
        var value = AngleValue.Degree(1.0);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void PowerValueCtor()
    {
        var value = PowerValue.Watt(1.0);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void TemperatureValueCtor()
    {
        var value = TemperatureValue.Celsius(10);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void MassValueCtor()
    {
        var value = MassValue.Gram(10);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void LengthValueCtor()
    {
        var value = LengthValue.Meter(10);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void TimeValueCtor()
    {
        var value = TimeValue.Second(10);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void AreaValueCtor()
    {
        var value = AreaValue.Meter(10);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void VolumeValueCtor()
    {
        var value = VolumeValue.Meter(10);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void ComplexCtor()
    {
        var value = new Complex(1, 2);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void BoolCtor()
    {
        var value = true;
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void VectorCtor()
    {
        var value = VectorValue.Create(NumberValue.One);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void MatrixCtor()
    {
        var value = MatrixValue.Create(NumberValue.One);
        var x = new Parameter("x", value);

        Assert.That(x.Value.Value, Is.EqualTo(value));
    }

    [Test]
    public void StringCtor()
    {
        var str = "hello";
        var x = new Parameter("x", str);

        Assert.That(x.Value.Value, Is.EqualTo(str));
    }

    [Test]
    public void RationalCtor()
    {
        var rational = new RationalValue(1, 3);
        var x = new Parameter("x", rational);

        Assert.That(x.Value.Value, Is.EqualTo(rational));
    }

    [Test]
    public void NullEqual()
    {
        var parameter = new Parameter("x", 1);
        var isEqual = parameter.Equals(null);

        Assert.That(isEqual, Is.False);
    }

    [Test]
    public void RefEqualEqual()
    {
        var parameter = new Parameter("x", 1);
        var isEqual = parameter.Equals(parameter);

        Assert.That(isEqual);
    }

    [Test]
    public void EqualObjectEqual()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("x", 1);
        var isEqual = parameter1.Equals((object)parameter2);

        Assert.That(isEqual);
    }

    [Test]
    public void OtherType()
    {
        var parameter = new Parameter("x", 1);
        var obj = new object();
        var isEqual = parameter.Equals(obj);

        Assert.That(isEqual, Is.False);
    }

    [Test]
    public void EqualParameters()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("x", 1);
        var isEqual = parameter1.Equals(parameter2);

        Assert.That(isEqual);
    }

    [Test]
    public void NotEqualKey()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("y", 1);
        var isEqual = parameter1.Equals(parameter2);

        Assert.That(isEqual, Is.False);
    }

    [Test]
    public void NotEqualValue()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("x", 2);
        var isEqual = parameter1.Equals(parameter2);

        Assert.That(isEqual, Is.False);
    }

    [Test]
    public void EqualOperatorTest()
    {
        var x = new Parameter("x", 1);
        var y = new Parameter("x", 1);

        Assert.That(x == y);
    }

    [Test]
    public void NotEqualOperatorTest()
    {
        var x = new Parameter("x", 1);
        var y = new Parameter("x", 2);

        Assert.That(x != y);
    }

    [Test]
    public void GreaterTest()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("a", 2);

        Assert.That(parameter1 > parameter2);
    }

    [Test]
    public void GreaterLeftNullTest()
    {
        var parameter2 = new Parameter("a", 2);

        Assert.That(null > parameter2, Is.False);
    }

    [Test]
    public void GreaterRightNullTest()
    {
        var parameter1 = new Parameter("x", 1);

        Assert.That(parameter1 > null, Is.False);
    }

    [Test]
    public void LessTest()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("y", 2);

        Assert.That(parameter1 < parameter2);
    }

    [Test]
    public void LessLeftNullTest()
    {
        var parameter2 = new Parameter("y", 2);

        Assert.That(null < parameter2, Is.False);
    }

    [Test]
    public void LessRightNullTest()
    {
        var parameter1 = new Parameter("x", 1);

        Assert.That(parameter1 < null, Is.False);
    }

    [Test]
    public void GreaterOrEqualTest()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("a", 2);

        Assert.That(parameter1 >= parameter2);
    }

    [Test]
    public void LessOrEqualTest()
    {
        var parameter1 = new Parameter("x", 1);
        var parameter2 = new Parameter("y", 2);

        Assert.That(parameter1 <= parameter2);
    }

    [Test]
    public void CompareToNullTest()
    {
        var parameter = new Parameter("x", 1);
        var result = parameter.CompareTo(null);

        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void EmptyKeyTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Parameter(string.Empty, 1.0));
    }

    [Test]
    public void SetNullValueTest()
    {
        Assert.Throws<ArgumentNullException>(() => new Parameter("x", (string)null));
    }

    [Test]
    public void EditConstantParameterTest()
    {
        var parameter = new Parameter("x", 1.0, ParameterType.Constant);

        Assert.Throws<ParameterIsReadOnlyException>(() => parameter.Value = 3.0);
    }

    [Test]
    public void EditReadonlyParameterTest()
    {
        var parameter = new Parameter("x", 1.0, ParameterType.ReadOnly);

        Assert.Throws<ParameterIsReadOnlyException>(() => parameter.Value = 3.0);
    }

    [Test]
    public void ToStringTest()
    {
        var parameter = new Parameter("x", 1, ParameterType.Constant);
        var str = parameter.ToString();
        var expected = "x: 1 (Constant)";

        Assert.That(str, Is.EqualTo(expected));
    }
}