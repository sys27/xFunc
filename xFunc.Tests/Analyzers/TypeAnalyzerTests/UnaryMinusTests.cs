namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class UnaryMinusTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestUnaryMinusUndefined()
    {
        var exp = new UnaryMinus(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestUnaryMinusNumber()
    {
        var exp = new UnaryMinus(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestUnaryMinusAngleNumber()
    {
        var exp = new UnaryMinus(AngleValue.Degree(10).AsExpression());

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestUnaryMinusPower()
    {
        var exp = new UnaryMinus(PowerValue.Watt(10).AsExpression());

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestUnaryMinusTemperature()
    {
        var exp = new UnaryMinus(TemperatureValue.Celsius(10).AsExpression());

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestUnaryMinusMass()
    {
        var exp = new UnaryMinus(MassValue.Gram(10).AsExpression());

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestUnaryMinusLength()
    {
        var exp = new UnaryMinus(LengthValue.Meter(10).AsExpression());

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestUnaryMinusTime()
    {
        var exp = new UnaryMinus(TimeValue.Second(10).AsExpression());

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestUnaryMinusArea()
    {
        var exp = new UnaryMinus(AreaValue.Meter(10).AsExpression());

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestUnaryMinusVolume()
    {
        var exp = new UnaryMinus(VolumeValue.Meter(10).AsExpression());

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestUnaryMinusComplexNumber()
    {
        var exp = new UnaryMinus(new ComplexNumber(10, 10));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestUnaryMinusRationalNumber()
    {
        var exp = new UnaryMinus(new Rational(Number.One, Number.Two));

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestUnaryMinusException()
    {
        var exp = new UnaryMinus(Bool.False);

        TestException(exp);
    }
}