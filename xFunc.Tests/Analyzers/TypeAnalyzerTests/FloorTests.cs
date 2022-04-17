namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class FloorTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestFloorUndefined()
    {
        var exp = new Floor(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestFloorNumber()
    {
        var exp = new Floor(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestFloorAngle()
    {
        var exp = new Floor(AngleValue.Degree(5.5).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestFloorPower()
    {
        var exp = new Floor(PowerValue.Watt(5.5).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestFloorTemperature()
    {
        var exp = new Floor(TemperatureValue.Celsius(5.5).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestFloorMass()
    {
        var exp = new Floor(MassValue.Gram(5.5).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestFloorLength()
    {
        var exp = new Floor(LengthValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestFloorTime()
    {
        var exp = new Floor(TimeValue.Second(5.5).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestFloorArea()
    {
        var exp = new Floor(AreaValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestFloorVolume()
    {
        var exp = new Floor(VolumeValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestFloorException()
    {
        var exp = new Floor(Bool.False);

        TestException(exp);
    }
}