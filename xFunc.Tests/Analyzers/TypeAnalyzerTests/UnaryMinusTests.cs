namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class UnaryMinusTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestUnaryMinusUndefined()
    {
        var exp = new UnaryMinus(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestUnaryMinusNumber()
    {
        var exp = new UnaryMinus(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestUnaryMinusAngleNumber()
    {
        var exp = new UnaryMinus(AngleValue.Degree(10).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestUnaryMinusPower()
    {
        var exp = new UnaryMinus(PowerValue.Watt(10).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestUnaryMinusTemperature()
    {
        var exp = new UnaryMinus(TemperatureValue.Celsius(10).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestUnaryMinusMass()
    {
        var exp = new UnaryMinus(MassValue.Gram(10).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestUnaryMinusLength()
    {
        var exp = new UnaryMinus(LengthValue.Meter(10).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestUnaryMinusTime()
    {
        var exp = new UnaryMinus(TimeValue.Second(10).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestUnaryMinusArea()
    {
        var exp = new UnaryMinus(AreaValue.Meter(10).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestUnaryMinusVolume()
    {
        var exp = new UnaryMinus(VolumeValue.Meter(10).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestUnaryMinusComplexNumber()
    {
        var exp = new UnaryMinus(new ComplexNumber(10, 10));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestUnaryMinusException()
    {
        var exp = new UnaryMinus(Bool.False);

        TestException(exp);
    }
}