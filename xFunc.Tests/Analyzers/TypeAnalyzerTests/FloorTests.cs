namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class FloorTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestFloorUndefined()
    {
        var exp = new Floor(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestFloorNumber()
    {
        var exp = new Floor(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestFloorAngle()
    {
        var exp = new Floor(AngleValue.Degree(5.5).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestFloorPower()
    {
        var exp = new Floor(PowerValue.Watt(5.5).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestFloorTemperature()
    {
        var exp = new Floor(TemperatureValue.Celsius(5.5).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestFloorMass()
    {
        var exp = new Floor(MassValue.Gram(5.5).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestFloorLength()
    {
        var exp = new Floor(LengthValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestFloorTime()
    {
        var exp = new Floor(TimeValue.Second(5.5).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestFloorArea()
    {
        var exp = new Floor(AreaValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestFloorVolume()
    {
        var exp = new Floor(VolumeValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestFloorException()
    {
        var exp = new Floor(Bool.False);

        TestException(exp);
    }
}