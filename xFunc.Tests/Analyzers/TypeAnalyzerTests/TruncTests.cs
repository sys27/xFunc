namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class TruncTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestTruncUndefined()
    {
        var exp = new Trunc(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestTruncNumber()
    {
        var exp = new Trunc(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestTruncAngle()
    {
        var exp = new Trunc(AngleValue.Degree(5.5).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestTruncPower()
    {
        var exp = new Trunc(PowerValue.Watt(5.5).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestTruncTemperature()
    {
        var exp = new Trunc(TemperatureValue.Celsius(5.5).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestTruncMass()
    {
        var exp = new Trunc(MassValue.Gram(5.5).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestTruncLength()
    {
        var exp = new Trunc(LengthValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestTimeLength()
    {
        var exp = new Trunc(TimeValue.Second(5.5).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestAreaLength()
    {
        var exp = new Trunc(AreaValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestVolumeLength()
    {
        var exp = new Trunc(VolumeValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestTruncException()
    {
        var exp = new Trunc(Bool.False);

        TestException(exp);
    }
}