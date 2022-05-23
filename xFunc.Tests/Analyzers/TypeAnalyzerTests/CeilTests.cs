namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class CeilTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestCeilNumber()
    {
        var exp = new Ceil(new Number(-2));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestCeilAngle()
    {
        var exp = new Ceil(AngleValue.Degree(5.5).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestCeilPower()
    {
        var exp = new Ceil(PowerValue.Watt(5.5).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestCeilTemperature()
    {
        var exp = new Ceil(TemperatureValue.Celsius(5.5).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestCeilMass()
    {
        var exp = new Ceil(MassValue.Gram(5.5).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestCeilLength()
    {
        var exp = new Ceil(LengthValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestCeilTime()
    {
        var exp = new Ceil(TimeValue.Second(5.5).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestCeilArea()
    {
        var exp = new Ceil(AreaValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestCeilVolume()
    {
        var exp = new Ceil(VolumeValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestCeilVariable()
    {
        var exp = new Ceil(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestCeilException()
    {
        var exp = new Ceil(Bool.False);

        TestException(exp);
    }
}