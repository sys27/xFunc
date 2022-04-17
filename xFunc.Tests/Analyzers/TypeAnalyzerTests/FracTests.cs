namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class FracTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestFracUndefined()
    {
        var exp = new Frac(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestFracNumber()
    {
        var exp = new Frac(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestFracAngle()
    {
        var exp = new Frac(AngleValue.Degree(5.5).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestFracPower()
    {
        var exp = new Frac(PowerValue.Watt(5.5).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestFracTemperature()
    {
        var exp = new Frac(TemperatureValue.Celsius(5.5).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestFracMass()
    {
        var exp = new Frac(MassValue.Gram(5.5).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestFracLength()
    {
        var exp = new Frac(LengthValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestFracTime()
    {
        var exp = new Frac(TimeValue.Second(5.5).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestFracArea()
    {
        var exp = new Frac(AreaValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestFracVolume()
    {
        var exp = new Frac(VolumeValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestFracException()
    {
        var exp = new Frac(Bool.False);

        TestException(exp);
    }
}