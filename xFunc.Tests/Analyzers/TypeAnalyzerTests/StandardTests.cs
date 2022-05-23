// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Convert = xFunc.Maths.Expressions.Units.Convert;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class StandardTests : TypeAnalyzerBaseTests
{
    [Fact]
    public void TestDefineUndefined()
    {
        var exp = new Define(Variable.X, new Number(-2));

        Test(exp, ResultTypes.String);
    }

    [Fact]
    public void TestDelVector()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var exp = new Del(diff, simp, Number.Two);

        Test(exp, ResultTypes.Vector);
    }

    [Fact]
    public void TestDerivExpression()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var exp = new Derivative(diff, simp, Variable.X);

        Test(exp, ResultTypes.Expression);
    }

    [Fact]
    public void TestDerivExpressionWithVar()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var exp = new Derivative(diff, simp, Variable.X, Variable.X);

        Test(exp, ResultTypes.Expression);
    }

    [Fact]
    public void TestDerivNumber()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var exp = new Derivative(diff, simp, Variable.X, Variable.X, Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestExpUndefined()
    {
        var exp = new Exp(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestExpNumber()
    {
        var exp = new Exp(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestExpComplexNumber()
    {
        var exp = new Exp(new ComplexNumber(10, 10));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestExpException()
    {
        var exp = new Exp(Bool.False);

        TestException(exp);
    }

    [Fact]
    public void TestFactUndefined()
    {
        var exp = new Fact(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestFactNumber()
    {
        var exp = new Fact(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestFactException()
    {
        var exp = new Fact(Bool.False);

        TestException(exp);
    }

    [Fact]
    public void TestGCDUndefined()
    {
        var exp = new GCD(new Number(10), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestGCDNumber()
    {
        var exp = new GCD(new IExpression[]
        {
            new Number(10), new Number(10), new Number(10)
        });

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestGCDException()
    {
        var exp = new GCD(new ComplexNumber(10), new Number(10));

        TestDiffParamException(exp);
    }

    [Fact]
    public void TestLbUndefined()
    {
        var exp = new Lb(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestLbNumber()
    {
        var exp = new Lb(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestLbException()
    {
        var exp = new Lb(Bool.False);

        TestException(exp);
    }

    [Fact]
    public void TestLCMUndefined()
    {
        var exp = new LCM(new Number(10), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestLCMNumber()
    {
        var exp = new LCM(new IExpression[]
        {
            new Number(10), new Number(10), new Number(10)
        });

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestLCMException()
    {
        var exp = new LCM(new ComplexNumber(10), new Number(10));

        TestDiffParamException(exp);
    }

    [Fact]
    public void TestLgUndefined()
    {
        var exp = new Lg(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestLgNumber()
    {
        var exp = new Lg(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestLgComplexNumber()
    {
        var exp = new Lg(new ComplexNumber(10, 10));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestLgException()
    {
        var exp = new Lg(Bool.False);

        TestException(exp);
    }

    [Fact]
    public void TestLnUndefined()
    {
        var exp = new Ln(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestLnNumber()
    {
        var exp = new Ln(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestLnComplexNumber()
    {
        var exp = new Ln(new ComplexNumber(10, 10));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestLnException()
    {
        var exp = new Ln(Bool.False);

        TestException(exp);
    }

    [Fact]
    public void TestLogNumberAndUndefined()
    {
        var exp = new Log(Number.Two, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestLogUndefinedAndNumber()
    {
        var exp = new Log(Variable.X, Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestLogNumber()
    {
        var exp = new Log(Number.Two, new Number(4));

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestLogComplexNumber()
    {
        var exp = new Log(Number.Two, new ComplexNumber(8, 3));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Fact]
    public void TestLogException()
    {
        var exp = new Log(Number.Two, Bool.False);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestLogBaseIsNotNumber()
    {
        var exp = new Log(Bool.False, Number.Two);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestModUndefined()
    {
        var exp = new Mod(Variable.X, Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestModUndefinedAndNumber()
    {
        var exp = new Mod(Variable.X, Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestModNumberAndUndefined()
    {
        var exp = new Mod(Number.Two, Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestModNumber()
    {
        var exp = new Mod(new Number(4), Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Fact]
    public void TestModNumberAndBool()
    {
        var exp = new Mod(new Number(4), Bool.True);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestModBoolAndNumber()
    {
        var exp = new Mod(Bool.False, Number.Two);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestModUndefinedAndBool()
    {
        var exp = new Mod(Variable.X, Bool.False);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestModBoolAndUndefined()
    {
        var exp = new Mod(Bool.False, Variable.X);

        TestBinaryException(exp);
    }

    [Fact]
    public void TestModException()
    {
        var exp = new Mod(Bool.False, Bool.False);

        TestException(exp);
    }

    [Fact]
    public void TestNumber()
    {
        Test(Number.One, ResultTypes.Number);
    }

    [Fact]
    public void TestSimplify()
    {
        var simp = new Simplifier();
        Test(new Simplify(simp, Variable.X), ResultTypes.Undefined);
    }

    [Fact]
    public void TestSqrt()
    {
        Test(new Sqrt(Variable.X), ResultTypes.Undefined);
    }

    [Fact]
    public void TestUndefine()
    {
        Test(new Undefine(Variable.X), ResultTypes.String);
    }

    [Fact]
    public void TestUserFunction()
    {
        Test(new UserFunction("f", new IExpression[0]), ResultTypes.Undefined);
    }

    [Fact]
    public void TestVariable()
    {
        Test(new Sqrt(Variable.X), ResultTypes.Undefined);
    }

    [Fact]
    public void TestDeletageExpression()
    {
        Test(new DelegateExpression(_ => null), ResultTypes.Undefined);
    }

    [Fact]
    public void TestAngleNumber()
    {
        Test(AngleValue.Degree(10).AsExpression(), ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestToDegreeUndefined()
    {
        Test(new ToDegree(Variable.X), ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestToDegreeNumber()
    {
        Test(new ToDegree(new Number(10)), ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestToDegreeAngle()
    {
        Test(new ToDegree(AngleValue.Radian(10).AsExpression()), ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestToDegreeException()
    {
        TestException(new ToDegree(Bool.True));
    }

    [Fact]
    public void TestToRadianNumber()
    {
        Test(new ToRadian(new Number(10)), ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestToRadianAngle()
    {
        Test(new ToRadian(AngleValue.Degree(10).AsExpression()), ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestToRadianException()
    {
        TestException(new ToRadian(Bool.True));
    }

    [Fact]
    public void TestToGradianNumber()
    {
        Test(new ToGradian(new Number(10)), ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestToGradianAngle()
    {
        Test(new ToGradian(AngleValue.Radian(10).AsExpression()), ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestToGradianException()
    {
        TestException(new ToGradian(Bool.True));
    }

    [Fact]
    public void TestStringExpression()
    {
        var exp = new StringExpression("hello");

        Test(exp, ResultTypes.String);
    }

    [Fact]
    public void TestConvertVariable()
    {
        var exp = new Convert(
            new Converter(),
            new Variable("x"),
            new StringExpression("rad")
        );

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestConvert()
    {
        var exp = new Convert(
            new Converter(),
            Number.One,
            new StringExpression("rad")
        );

        Test(exp, ResultTypes.Undefined);
    }

    [Fact]
    public void TestAngleConvert()
    {
        var exp = new Convert(
            new Converter(),
            AngleValue.Degree(10).AsExpression(),
            new StringExpression("rad")
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Fact]
    public void TestPowerConvert()
    {
        var exp = new Convert(
            new Converter(),
            PowerValue.Watt(10).AsExpression(),
            new StringExpression("kW")
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Fact]
    public void TestTemperatureConvert()
    {
        var exp = new Convert(
            new Converter(),
            TemperatureValue.Celsius(10).AsExpression(),
            new StringExpression("K")
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Fact]
    public void TestMassConvert()
    {
        var exp = new Convert(
            new Converter(),
            MassValue.Kilogram(10).AsExpression(),
            new StringExpression("g")
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Fact]
    public void TestLengthConvert()
    {
        var exp = new Convert(
            new Converter(),
            LengthValue.Meter(10).AsExpression(),
            new StringExpression("m")
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Fact]
    public void TestTimeConvert()
    {
        var exp = new Convert(
            new Converter(),
            TimeValue.Second(10).AsExpression(),
            new StringExpression("s")
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Fact]
    public void TestAreaConvert()
    {
        var exp = new Convert(
            new Converter(),
            AreaValue.Meter(10).AsExpression(),
            new StringExpression("m^2")
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Fact]
    public void TestVolumeConvert()
    {
        var exp = new Convert(
            new Converter(),
            VolumeValue.Meter(10).AsExpression(),
            new StringExpression("m^3")
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Fact]
    public void TestConvertException()
    {
        var exp = new Convert(
            new Converter(),
            Bool.True,
            new StringExpression("rad")
        );

        TestException(exp);
    }

    [Theory]
    [ClassData(typeof(AllExpressionsData))]
    public void TestNullException(Type type) => TestNullExp(type);
}