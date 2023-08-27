namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class FracTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestFracUndefined()
    {
        var exp = new Frac(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestFracNumber()
    {
        var exp = new Frac(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestFracAngle()
    {
        var exp = new Frac(AngleValue.Degree(5.5).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestFracPower()
    {
        var exp = new Frac(PowerValue.Watt(5.5).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestFracTemperature()
    {
        var exp = new Frac(TemperatureValue.Celsius(5.5).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestFracMass()
    {
        var exp = new Frac(MassValue.Gram(5.5).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestFracLength()
    {
        var exp = new Frac(LengthValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestFracTime()
    {
        var exp = new Frac(TimeValue.Second(5.5).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestFracArea()
    {
        var exp = new Frac(AreaValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestFracVolume()
    {
        var exp = new Frac(VolumeValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestFracException()
    {
        var exp = new Frac(Bool.False);

        TestException(exp);
    }
}