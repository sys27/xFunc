namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class CeilTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestCeilNumber()
    {
        var exp = new Ceil(new Number(-2));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestCeilAngle()
    {
        var exp = new Ceil(AngleValue.Degree(5.5).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestCeilPower()
    {
        var exp = new Ceil(PowerValue.Watt(5.5).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestCeilTemperature()
    {
        var exp = new Ceil(TemperatureValue.Celsius(5.5).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestCeilMass()
    {
        var exp = new Ceil(MassValue.Gram(5.5).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestCeilLength()
    {
        var exp = new Ceil(LengthValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestCeilTime()
    {
        var exp = new Ceil(TimeValue.Second(5.5).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestCeilArea()
    {
        var exp = new Ceil(AreaValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestCeilVolume()
    {
        var exp = new Ceil(VolumeValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestCeilVariable()
    {
        var exp = new Ceil(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestCeilException()
    {
        var exp = new Ceil(Bool.False);

        TestException(exp);
    }
}