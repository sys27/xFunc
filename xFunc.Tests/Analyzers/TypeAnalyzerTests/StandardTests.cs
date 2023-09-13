// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using Convert = xFunc.Maths.Expressions.Units.Convert;

namespace xFunc.Tests.Analyzers.TypeAnalyzerTests;

public class StandardTests : TypeAnalyzerBaseTests
{
    [Test]
    public void TestDefineUndefined()
    {
        var exp = new Assign(Variable.X, new Number(-2));

        Test(exp, ResultTypes.String);
    }

    [Test]
    public void TestDelVector()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var exp = new Del(diff, simp, Number.Two);

        Test(exp, ResultTypes.Function);
    }

    [Test]
    public void TestDerivExpression()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var exp = new Derivative(diff, simp, Variable.X);

        Test(exp, ResultTypes.Function);
    }

    [Test]
    public void TestDerivExpressionWithVar()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var exp = new Derivative(diff, simp, Variable.X, Variable.X);

        Test(exp, ResultTypes.Function);
    }

    [Test]
    public void TestDerivNumber()
    {
        var diff = new Differentiator();
        var simp = new Simplifier();
        var exp = new Derivative(diff, simp, Variable.X, Variable.X, Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestExpUndefined()
    {
        var exp = new Exp(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestExpNumber()
    {
        var exp = new Exp(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestExpComplexNumber()
    {
        var exp = new Exp(new ComplexNumber(10, 10));

        Test(exp, ResultTypes.ComplexNumber);
    }

    [Test]
    public void TestExpException()
    {
        var exp = new Exp(Bool.False);

        TestException(exp);
    }

    [Test]
    public void TestFactUndefined()
    {
        var exp = new Fact(Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestFactNumber()
    {
        var exp = new Fact(new Number(10));

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestFactException()
    {
        var exp = new Fact(Bool.False);

        TestException(exp);
    }

    [Test]
    public void TestGCDUndefined()
    {
        var exp = new GCD(new Number(10), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestGCDNumber()
    {
        var exp = new GCD(new IExpression[]
        {
            new Number(10), new Number(10), new Number(10)
        });

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestGCDException()
    {
        var exp = new GCD(new ComplexNumber(10), new Number(10));

        TestDiffParamException(exp);
    }

    [Test]
    public void TestLCMUndefined()
    {
        var exp = new LCM(new Number(10), Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestLCMNumber()
    {
        var exp = new LCM(new IExpression[]
        {
            new Number(10), new Number(10), new Number(10)
        });

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestLCMException()
    {
        var exp = new LCM(new ComplexNumber(10), new Number(10));

        TestDiffParamException(exp);
    }

    [Test]
    public void TestModUndefined()
    {
        var exp = new Mod(Variable.X, Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestModUndefinedAndNumber()
    {
        var exp = new Mod(Variable.X, Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestModNumberAndUndefined()
    {
        var exp = new Mod(Number.Two, Variable.X);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestModNumber()
    {
        var exp = new Mod(new Number(4), Number.Two);

        Test(exp, ResultTypes.Number);
    }

    [Test]
    public void TestModNumberAndBool()
    {
        var exp = new Mod(new Number(4), Bool.True);

        TestBinaryException(exp);
    }

    [Test]
    public void TestModBoolAndNumber()
    {
        var exp = new Mod(Bool.False, Number.Two);

        TestBinaryException(exp);
    }

    [Test]
    public void TestModUndefinedAndBool()
    {
        var exp = new Mod(Variable.X, Bool.False);

        TestBinaryException(exp);
    }

    [Test]
    public void TestModBoolAndUndefined()
    {
        var exp = new Mod(Bool.False, Variable.X);

        TestBinaryException(exp);
    }

    [Test]
    public void TestModException()
    {
        var exp = new Mod(Bool.False, Bool.False);

        TestException(exp);
    }

    [Test]
    public void TestNumber()
    {
        Test(Number.One, ResultTypes.Number);
    }

    [Test]
    public void TestSimplify()
    {
        var simp = new Simplifier();
        Test(new Simplify(simp, Variable.X), ResultTypes.Function);
    }

    [Test]
    public void TestSqrt()
    {
        Test(new Sqrt(Variable.X), ResultTypes.Undefined);
    }

    [Test]
    public void TestUndefine()
    {
        Test(new Unassign(Variable.X), ResultTypes.String);
    }

    [Test]
    public void TestCallExpression()
    {
        Test(new CallExpression(new Variable("f"), ImmutableArray<IExpression>.Empty), ResultTypes.Undefined);
    }

    [Test]
    public void TestLambdaExpression()
    {
        var exp = Number.One.ToLambdaExpression();

        Test(exp, ResultTypes.Function);
    }

    [Test]
    public void TestCurryWithLambdaExpression()
    {
        var exp = new Curry(Number.One.ToLambdaExpression());

        Test(exp, ResultTypes.Function);
    }

    [Test]
    public void TestCurryWithNonLambdaExpression()
    {
        var exp = new Curry(Number.One);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestCurryWithLambdaAndParametersExpression()
    {
        var exp = new Curry(
            Number.One.ToLambdaExpression(),
            ImmutableArray.Create<IExpression>(Number.One, Number.One));

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestVariable()
    {
        Test(new Sqrt(Variable.X), ResultTypes.Undefined);
    }

    [Test]
    public void TestDeletageExpression()
    {
        Test(new DelegateExpression(_ => null), ResultTypes.Undefined);
    }

    [Test]
    public void TestAngleNumber()
    {
        Test(AngleValue.Degree(10).AsExpression(), ResultTypes.AngleNumber);
    }

    [Test]
    public void TestToDegreeUndefined()
    {
        Test(new ToDegree(Variable.X), ResultTypes.AngleNumber);
    }

    [Test]
    public void TestToDegreeNumber()
    {
        Test(new ToDegree(new Number(10)), ResultTypes.AngleNumber);
    }

    [Test]
    public void TestToDegreeAngle()
    {
        Test(new ToDegree(AngleValue.Radian(10).AsExpression()), ResultTypes.AngleNumber);
    }

    [Test]
    public void TestToDegreeException()
    {
        TestException(new ToDegree(Bool.True));
    }

    [Test]
    public void TestToRadianNumber()
    {
        Test(new ToRadian(new Number(10)), ResultTypes.AngleNumber);
    }

    [Test]
    public void TestToRadianAngle()
    {
        Test(new ToRadian(AngleValue.Degree(10).AsExpression()), ResultTypes.AngleNumber);
    }

    [Test]
    public void TestToRadianException()
    {
        TestException(new ToRadian(Bool.True));
    }

    [Test]
    public void TestToGradianNumber()
    {
        Test(new ToGradian(new Number(10)), ResultTypes.AngleNumber);
    }

    [Test]
    public void TestToGradianAngle()
    {
        Test(new ToGradian(AngleValue.Radian(10).AsExpression()), ResultTypes.AngleNumber);
    }

    [Test]
    public void TestToGradianException()
    {
        TestException(new ToGradian(Bool.True));
    }

    [Test]
    public void TestStringExpression()
    {
        var exp = new StringExpression("hello");

        Test(exp, ResultTypes.String);
    }

    [Test]
    public void TestConvertVariable()
    {
        var exp = new Convert(
            new Converter(),
            new Variable("x"),
            new StringExpression("rad")
        );

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestConvert()
    {
        var exp = new Convert(
            new Converter(),
            Number.One,
            new StringExpression("rad")
        );

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestAngleConvert()
    {
        var exp = new Convert(
            new Converter(),
            AngleValue.Degree(10).AsExpression(),
            new StringExpression("rad")
        );

        Test(exp, ResultTypes.AngleNumber);
    }

    [Test]
    public void TestPowerConvert()
    {
        var exp = new Convert(
            new Converter(),
            PowerValue.Watt(10).AsExpression(),
            new StringExpression("kW")
        );

        Test(exp, ResultTypes.PowerNumber);
    }

    [Test]
    public void TestTemperatureConvert()
    {
        var exp = new Convert(
            new Converter(),
            TemperatureValue.Celsius(10).AsExpression(),
            new StringExpression("K")
        );

        Test(exp, ResultTypes.TemperatureNumber);
    }

    [Test]
    public void TestMassConvert()
    {
        var exp = new Convert(
            new Converter(),
            MassValue.Kilogram(10).AsExpression(),
            new StringExpression("g")
        );

        Test(exp, ResultTypes.MassNumber);
    }

    [Test]
    public void TestLengthConvert()
    {
        var exp = new Convert(
            new Converter(),
            LengthValue.Meter(10).AsExpression(),
            new StringExpression("m")
        );

        Test(exp, ResultTypes.LengthNumber);
    }

    [Test]
    public void TestTimeConvert()
    {
        var exp = new Convert(
            new Converter(),
            TimeValue.Second(10).AsExpression(),
            new StringExpression("s")
        );

        Test(exp, ResultTypes.TimeNumber);
    }

    [Test]
    public void TestAreaConvert()
    {
        var exp = new Convert(
            new Converter(),
            AreaValue.Meter(10).AsExpression(),
            new StringExpression("m^2")
        );

        Test(exp, ResultTypes.AreaNumber);
    }

    [Test]
    public void TestVolumeConvert()
    {
        var exp = new Convert(
            new Converter(),
            VolumeValue.Meter(10).AsExpression(),
            new StringExpression("m^3")
        );

        Test(exp, ResultTypes.VolumeNumber);
    }

    [Test]
    public void TestConvertException()
    {
        var exp = new Convert(
            new Converter(),
            Bool.True,
            new StringExpression("rad")
        );

        TestException(exp);
    }

    [Test]
    public void TestRational()
    {
        var exp = new Rational(Number.One, Number.Two);

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestRationalLeftUndefined()
    {
        var exp = new Rational(Variable.X, Number.Two);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestRationalRightUndefined()
    {
        var exp = new Rational(Number.One, Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestRationalException()
    {
        var exp = new Rational(Bool.True, Bool.False);

        TestException(exp);
    }

    [Test]
    public void TestRationalLeftException()
    {
        var exp = new Rational(Bool.True, Number.Two);

        TestBinaryException(exp);
    }

    [Test]
    public void TestRationalRightException()
    {
        var exp = new Rational(Number.One, Bool.False);

        TestBinaryException(exp);
    }

    [Test]
    public void TestToRational()
    {
        var exp = new ToRational(Number.Two);

        Test(exp, ResultTypes.RationalNumber);
    }

    [Test]
    public void TestToRationalUndefined()
    {
        var exp = new ToRational(Variable.X);

        Test(exp, ResultTypes.Undefined);
    }

    [Test]
    public void TestToRationalException()
    {
        var exp = new ToRational(Bool.False);

        TestException(exp);
    }

    [Test]
    [TestCaseSource(typeof(AllExpressionsData))]
    public void TestNullException(Type type) => TestNullExp(type);
}