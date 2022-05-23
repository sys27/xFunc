namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class SignTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestSignUndefined()
    {
        Test(new Sign(Variable.X), ResultTypes.Undefined);
    }

    [Fact]
    public void TestSignNumber()
    {
        Test(new Sign(new Number(-5)), ResultTypes.Number);
    }

    [Fact]
    public void TestSignAngle()
    {
        Test(new Sign(AngleValue.Degree(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestSignPower()
    {
        Test(new Sign(PowerValue.Watt(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestSignTemperature()
    {
        Test(new Sign(TemperatureValue.Celsius(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestSignMass()
    {
        Test(new Sign(MassValue.Gram(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestSignLength()
    {
        Test(new Sign(LengthValue.Meter(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestSignTime()
    {
        Test(new Sign(TimeValue.Second(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestSignArea()
    {
        Test(new Sign(AreaValue.Meter(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestSignVolume()
    {
        Test(new Sign(VolumeValue.Meter(10).AsExpression()), ResultTypes.Number);
    }

    [Fact]
    public void TestSignException()
    {
        TestException(new Sign(Bool.False));
    }
}