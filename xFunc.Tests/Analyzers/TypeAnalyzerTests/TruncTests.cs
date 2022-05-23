namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class TruncTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestTruncUndefined()
    {
        var exp = new Trunc(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestTruncNumber()
    {
        var exp = new Trunc(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestTruncAngle()
    {
        var exp = new Trunc(AngleValue.Degree(5.5).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestTruncPower()
    {
        var exp = new Trunc(PowerValue.Watt(5.5).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestTruncTemperature()
    {
        var exp = new Trunc(TemperatureValue.Celsius(5.5).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestTruncMass()
    {
        var exp = new Trunc(MassValue.Gram(5.5).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestTruncLength()
    {
        var exp = new Trunc(LengthValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestTimeLength()
    {
        var exp = new Trunc(TimeValue.Second(5.5).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestAreaLength()
    {
        var exp = new Trunc(AreaValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestVolumeLength()
    {
        var exp = new Trunc(VolumeValue.Meter(5.5).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestTruncException()
    {
        var exp = new Trunc(Bool.False);

        TestException(exp);
    }
}