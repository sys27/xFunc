// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Immutable;
using System.Numerics;
using Matrices = xFunc.Maths.Expressions.Matrices;

namespace xFunc.Tests.Analyzers.Formatters;

public class CommonFormatterTest
{
    #region Common

    [Test]
    public void AbsToStringTest()
    {
        var exp = new Abs(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("abs(5)"));
    }

    [Test]
    public void AddToStringTest()
    {
        var exp = new Add(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("5 + 0"));
    }

    [Test]
    public void AddToStringBinTest()
    {
        var exp = new Mul(Variable.X, new Add(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x * (5 + 0)"));
    }

    [Test]
    public void CeilToStringTest()
    {
        var ceil = new Ceil(new Number(5.55555555));

        Assert.That(ceil.ToString(), Is.EqualTo("ceil(5.55555555)"));
    }

    [Test]
    public void DefineToStringTest()
    {
        var exp = new Assign(Variable.X, Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("x := 0"));
    }

    [Test]
    public void DelToStringTest()
    {
        var exp = new Del(
            new Differentiator(),
            new Simplifier(),
            new Add(
                new Add(
                    new Mul(Number.Two, new Variable("x1")),
                    new Pow(new Variable("x2"), Number.Two)
                ),
                new Pow(new Variable("x3"), new Number(3))
            )
        );

        Assert.That(exp.ToString(), Is.EqualTo("del(((2 * x1) + (x2 ^ 2)) + (x3 ^ 3))"));
    }

    [Test]
    public void DerivativeToStringExpTest()
    {
        var deriv = new Derivative(new Differentiator(), new Simplifier(), new Sin(Variable.X));

        Assert.That(deriv.ToString(), Is.EqualTo("deriv(sin(x))"));
    }

    [Test]
    public void DerivativeToStringVarTest()
    {
        var deriv = new Derivative(new Differentiator(), new Simplifier(), new Sin(Variable.X), Variable.X);

        Assert.That(deriv.ToString(), Is.EqualTo("deriv(sin(x), x)"));
    }

    [Test]
    public void DerivativeToStringPointTest()
    {
        var deriv = new Derivative(new Differentiator(), new Simplifier(), new Sin(Variable.X), Variable.X, Number.One);

        Assert.That(deriv.ToString(), Is.EqualTo("deriv(sin(x), x, 1)"));
    }

    [Test]
    public void DivToStringTest()
    {
        var exp = new Div(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("5 / 0"));
    }

    [Test]
    public void DivToStringBinTest()
    {
        var exp = new Mul(Variable.X, new Div(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x * (5 / 0)"));
    }

    [Test]
    public void ExpToStringTest()
    {
        var exp = new Exp(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("exp(5)"));
    }

    [Test]
    public void FactToStringTest()
    {
        var exp = new Fact(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("5!"));
    }

    [Test]
    public void FloorToStringTest()
    {
        var exp = new Floor(new Number(5.55555555));

        Assert.That(exp.ToString(), Is.EqualTo("floor(5.55555555)"));
    }

    [Test]
    public void TruncToStringTest()
    {
        var exp = new Trunc(new Number(5.55555555));

        Assert.That(exp.ToString(), Is.EqualTo("trunc(5.55555555)"));
    }

    [Test]
    public void FracToStringTest()
    {
        var exp = new Frac(new Number(5.55555555));

        Assert.That(exp.ToString(), Is.EqualTo("frac(5.55555555)"));
    }

    [Test]
    public void GCDToStringTest()
    {
        var exp = new GCD(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("gcd(5, 0)"));
    }

    [Test]
    public void LCMToStringTest()
    {
        var exp = new LCM(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("lcm(5, 0)"));
    }

    [Test]
    public void LbToStringTest()
    {
        var exp = new Lb(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("lb(5)"));
    }

    [Test]
    public void LgToStringTest()
    {
        var exp = new Lg(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("lg(5)"));
    }

    [Test]
    public void LnToStringTest()
    {
        var exp = new Ln(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("ln(5)"));
    }

    [Test]
    public void LogToStringTest()
    {
        var exp = new Log(Number.Zero, new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("log(0, 5)"));
    }

    [Test]
    public void ModToStringTest()
    {
        var exp = new Mod(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("5 % 0"));
    }

    [Test]
    public void ModToStringBinTest()
    {
        var exp = new Mul(Variable.X, new Mod(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x * (5 % 0)"));
    }

    [Test]
    public void MulToStringTest()
    {
        var exp = new Mul(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("5 * 0"));
    }

    [Test]
    public void MulToStringAddTest()
    {
        var exp = new Add(Variable.X, new Mul(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x + (5 * 0)"));
    }

    [Test]
    public void MulToStringSubTest()
    {
        var exp = new Sub(Variable.X, new Mul(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x - (5 * 0)"));
    }

    [Test]
    public void MulToStringMulTest()
    {
        var exp = new Mul(Variable.X, new Mul(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x * (5 * 0)"));
    }

    [Test]
    public void MulToStringDivTest()
    {
        var exp = new Div(Variable.X, new Mul(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x / (5 * 0)"));
    }

    [Test]
    public void NumberTest()
    {
        var exp = new Number(3.3);

        Assert.That(exp.ToString(), Is.EqualTo("3.3"));
    }

    [Test]
    public void AngleNumberTest()
    {
        var exp = AngleValue.Degree(10).AsExpression();

        Assert.That(exp.ToString(), Is.EqualTo("10 'degree'"));
    }

    [Test]
    public void PowerNumberTest()
    {
        var exp = PowerValue.Watt(10).AsExpression();

        Assert.That(exp.ToString(), Is.EqualTo("10 'W'"));
    }

    [Test]
    public void TemperatureNumberTest()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();

        Assert.That(exp.ToString(), Is.EqualTo("10 '°C'"));
    }

    [Test]
    public void MassNumberTest()
    {
        var exp = MassValue.Gram(10).AsExpression();

        Assert.That(exp.ToString(), Is.EqualTo("10 'g'"));
    }

    [Test]
    public void LengthNumberTest()
    {
        var exp = LengthValue.Meter(10).AsExpression();

        Assert.That(exp.ToString(), Is.EqualTo("10 'm'"));
    }

    [Test]
    public void TimeNumberTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.That(exp.ToString(), Is.EqualTo("10 's'"));
    }

    [Test]
    public void AreaNumberTest()
    {
        var exp = AreaValue.Meter(10).AsExpression();

        Assert.That(exp.ToString(), Is.EqualTo("10 'm^2'"));
    }

    [Test]
    public void VolumeNumberTest()
    {
        var exp = VolumeValue.Meter(10).AsExpression();

        Assert.That(exp.ToString(), Is.EqualTo("10 'm^3'"));
    }

    [Test]
    public void ToDegreeTest()
    {
        var exp = new ToDegree(new Number(10));

        Assert.That(exp.ToString(), Is.EqualTo("todegree(10)"));
    }

    [Test]
    public void ToRadianTest()
    {
        var exp = new ToRadian(new Number(10));

        Assert.That(exp.ToString(), Is.EqualTo("toradian(10)"));
    }

    [Test]
    public void ToGradianTest()
    {
        var exp = new ToGradian(new Number(10));

        Assert.That(exp.ToString(), Is.EqualTo("togradian(10)"));
    }

    [Test]
    public void ToNumberTest()
    {
        var exp = new ToNumber(AngleValue.Degree(10).AsExpression());

        Assert.That(exp.ToString(), Is.EqualTo("tonumber(10 'degree')"));
    }

    [Test]
    public void NumberSubTest()
    {
        var exp = new Sub(Number.One, new Number(-3.3));

        Assert.That(exp.ToString(), Is.EqualTo("1 - -3.3"));
    }

    [Test]
    public void PowToStringTest()
    {
        var exp = new Pow(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("5 ^ 0"));
    }

    [Test]
    public void PowToStringAddTest()
    {
        var exp = new Add(Variable.X, new Pow(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x + (5 ^ 0)"));
    }

    [Test]
    public void PowToStringSubTest()
    {
        var exp = new Sub(Variable.X, new Pow(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x - (5 ^ 0)"));
    }

    [Test]
    public void PowToStringMulTest()
    {
        var exp = new Mul(Variable.X, new Pow(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x * (5 ^ 0)"));
    }

    [Test]
    public void PowToStringDivTest()
    {
        var exp = new Div(Variable.X, new Pow(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x / (5 ^ 0)"));
    }

    [Test]
    public void RootToStringTest()
    {
        var exp = new Root(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("root(5, 0)"));
    }

    [Test]
    public void RoundToStringTest()
    {
        var exp = new Round(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("round(5, 0)"));
    }

    [Test]
    public void SimplifyToStringTest()
    {
        var exp = new Simplify(new Simplifier(), new Sin(Variable.X));

        Assert.That(exp.ToString(), Is.EqualTo("simplify(sin(x))"));
    }

    [Test]
    public void SqrtToStringTest()
    {
        var exp = new Sqrt(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("sqrt(5)"));
    }

    [Test]
    public void SubToStringTest()
    {
        var exp = new Sub(new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("5 - 0"));
    }

    [Test]
    public void SubToStringSubTest()
    {
        var exp = new Sub(Variable.X, new Sub(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x - (5 - 0)"));
    }

    [Test]
    public void SubToStringDivTest()
    {
        var exp = new Div(Variable.X, new Sub(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("x / (5 - 0)"));
    }

    [Test]
    public void UnaryMinusToStringTest()
    {
        var exp = new UnaryMinus(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("-5"));
    }

    [Test]
    public void UnaryMinusToStringBinTest()
    {
        var exp = new UnaryMinus(new Add(new Number(5), Number.Zero));

        Assert.That(exp.ToString(), Is.EqualTo("-(5 + 0)"));
    }

    [Test]
    public void UnaryMinusToStringSubTest()
    {
        var exp = new Sub(Number.Zero, new UnaryMinus(new Number(5)));

        Assert.That(exp.ToString(), Is.EqualTo("0 - -5"));
    }

    [Test]
    public void UndefineToStringTest()
    {
        var exp = new Unassign(Variable.X);

        Assert.That(exp.ToString(), Is.EqualTo("undef(x)"));
    }

    [Test]
    public void LambdaExpressionToStringArgTest()
    {
        var exp = new Add(Variable.X, Variable.Y)
            .ToLambdaExpression(Variable.X, Variable.Y);

        Assert.That(exp.ToString(), Is.EqualTo("(x, y) => x + y"));
    }

    [Test]
    public void CallExpressionToStringArgTest()
    {
        var exp = new CallExpression(
            new Variable("f"),
            new IExpression[] { new Number(5), Number.Two }.ToImmutableArray());

        Assert.That(exp.ToString(), Is.EqualTo("f(5, 2)"));
    }

    [Test]
    public void InlineCallExpressionToStringArgTest()
    {
        var exp = new CallExpression(
            Variable.X.ToLambdaExpression(Variable.X.Name),
            new IExpression[] { new Number(5) }.ToImmutableArray());

        Assert.That(exp.ToString(), Is.EqualTo("((x) => x)(5)"));
    }

    [Test]
    public void CurryTest()
    {
        var exp = new Curry(new Lambda(Number.One).AsExpression());

        Assert.That(exp.ToString(), Is.EqualTo("curry(() => 1)"));
    }

    [Test]
    public void CurryWithParametersTest()
    {
        var exp = new Curry(
            new Lambda(Number.One).AsExpression(),
            new IExpression[] { Number.One, Number.Two }.ToImmutableArray());

        Assert.That(exp.ToString(), Is.EqualTo("curry(() => 1, 1, 2)"));
    }

    [Test]
    public void VariableTest()
    {
        var exp = Variable.X;

        Assert.That(exp.ToString(), Is.EqualTo("x"));
    }

    [Test]
    public void DelegateExpressionTest()
    {
        var exp = new DelegateExpression(_ => 0d);

        Assert.That(exp.ToString(), Is.EqualTo("{Delegate Expression}"));
    }

    [Test]
    public void StringExpressionTest()
    {
        var exp = new StringExpression("hello");

        Assert.That(exp.ToString(), Is.EqualTo("'hello'"));
    }

    [Test]
    public void ConvertTest()
    {
        var exp = new xFunc.Maths.Expressions.Units.Convert(
            new Converter(),
            AngleValue.Degree(90).AsExpression(),
            new StringExpression("rad")
        );

        Assert.That(exp.ToString(), Is.EqualTo("convert(90 'degree', 'rad')"));
    }

    [Test]
    public void RationalTest()
    {
        var exp = new Rational(Number.One, Number.Two);

        Assert.That(exp.ToString(), Is.EqualTo("1 // 2"));
    }

    [Test]
    public void ToRationalTest()
    {
        var exp = new ToRational(Number.Two);

        Assert.That(exp.ToString(), Is.EqualTo("torational(2)"));
    }

    #endregion Common

    #region Complex Numbers

    [Test]
    public void ComplexNumberPositiveNegativeToStringTest()
    {
        var complex = new ComplexNumber(3, -2);

        Assert.That(complex.ToString(), Is.EqualTo("3-2i"));
    }

    [Test]
    public void ComplexNumberNegativePositiveToStringTest()
    {
        var complex = new ComplexNumber(-3, 2);

        Assert.That(complex.ToString(), Is.EqualTo("-3+2i"));
    }

    [Test]
    public void ComplexNumberTwoPositiveToStringTest()
    {
        var complex = new ComplexNumber(3, 2);

        Assert.That(complex.ToString(), Is.EqualTo("3+2i"));
    }

    [Test]
    public void ComplexNumberTwoNegativeToStringTest()
    {
        var complex = new ComplexNumber(-3, -2);

        Assert.That(complex.ToString(), Is.EqualTo("-3-2i"));
    }

    [Test]
    public void ComplexNumberOnlyRealPartToStringTest()
    {
        var complex = new ComplexNumber(-3, 0);

        Assert.That(complex.ToString(), Is.EqualTo("-3"));
    }

    [Test]
    public void ComplexNumberOnlyImaginaryPartToStringTest()
    {
        var complex = new ComplexNumber(0, -2);

        Assert.That(complex.ToString(), Is.EqualTo("-2i"));
    }

    [Test]
    public void ComplexNumberBinaryToStringTest()
    {
        var exp = new Add(new ComplexNumber(3, 2), new ComplexNumber(3, 2));

        Assert.That(exp.ToString(), Is.EqualTo("3+2i + 3+2i"));
    }

    [Test]
    public void ComplexNumberAbsToStringTest()
    {
        var exp = new Abs(new ComplexNumber(3, 2));

        Assert.That(exp.ToString(), Is.EqualTo("abs(3+2i)"));
    }

    [Test]
    public void ComplexNumberIToStringTest()
    {
        var exp = new ComplexNumber(0, 1);

        Assert.That(exp.ToString(), Is.EqualTo("i"));
    }

    [Test]
    public void ComplexNumberNegativeIToStringTest()
    {
        var exp = new ComplexNumber(0, -1);

        Assert.That(exp.ToString(), Is.EqualTo("-i"));
    }

    [Test]
    public void ConjugateToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Conjugate(new ComplexNumber(complex));

        Assert.That(exp.ToString(), Is.EqualTo("conjugate(3.1+2.5i)"));
    }

    [Test]
    public void ImToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Im(new ComplexNumber(complex));

        Assert.That(exp.ToString(), Is.EqualTo("im(3.1+2.5i)"));
    }

    [Test]
    public void PhaseToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Phase(new ComplexNumber(complex));

        Assert.That(exp.ToString(), Is.EqualTo("phase(3.1+2.5i)"));
    }

    [Test]
    public void ReciprocalToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Reciprocal(new ComplexNumber(complex));

        Assert.That(exp.ToString(), Is.EqualTo("reciprocal(3.1+2.5i)"));
    }

    [Test]
    public void ReToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Re(new ComplexNumber(complex));

        Assert.That(exp.ToString(), Is.EqualTo("re(3.1+2.5i)"));
    }

    [Test]
    public void ToComplexToStringTest()
    {
        var exp = new ToComplex(Number.Two);

        Assert.That(exp.ToString(), Is.EqualTo("tocomplex(2)"));
    }

    #endregion

    #region Trigonometric

    [Test]
    public void ArccosToStringTest()
    {
        var exp = new Arccos(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arccos(5)"));
    }

    [Test]
    public void ArccotToStringTest()
    {
        var exp = new Arccot(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arccot(5)"));
    }

    [Test]
    public void ArccscToStringTest()
    {
        var exp = new Arccsc(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arccsc(5)"));
    }

    [Test]
    public void ArcsecToStringTest()
    {
        var exp = new Arcsec(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arcsec(5)"));
    }

    [Test]
    public void ArcsinToStringTest()
    {
        var exp = new Arcsin(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arcsin(5)"));
    }

    [Test]
    public void ArctanToStringTest()
    {
        var exp = new Arctan(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arctan(5)"));
    }

    [Test]
    public void CosToStringTest()
    {
        var exp = new Cos(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("cos(5)"));
    }

    [Test]
    public void CotToStringTest()
    {
        var exp = new Cot(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("cot(5)"));
    }

    [Test]
    public void CscToStringTest()
    {
        var exp = new Csc(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("csc(5)"));
    }

    [Test]
    public void SecToStringTest()
    {
        var exp = new Sec(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("sec(5)"));
    }

    [Test]
    public void SinToStringTest()
    {
        var exp = new Sin(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("sin(5)"));
    }

    [Test]
    public void TanToStringTest()
    {
        var exp = new Tan(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("tan(5)"));
    }

    #endregion

    #region Hyperbolic

    [Test]
    public void ArcoshToStringTest()
    {
        var exp = new Arcosh(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arcosh(5)"));
    }

    [Test]
    public void ArcothToStringTest()
    {
        var exp = new Arcoth(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arcoth(5)"));
    }

    [Test]
    public void ArcschToStringTest()
    {
        var exp = new Arcsch(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arcsch(5)"));
    }

    [Test]
    public void ArsechToStringTest()
    {
        var exp = new Arsech(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arsech(5)"));
    }

    [Test]
    public void ArsinhToStringTest()
    {
        var exp = new Arsinh(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("arsinh(5)"));
    }

    [Test]
    public void ArtanhToStringTest()
    {
        var exp = new Artanh(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("artanh(5)"));
    }

    [Test]
    public void CoshToStringTest()
    {
        var exp = new Cosh(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("cosh(5)"));
    }

    [Test]
    public void CothToStringTest()
    {
        var exp = new Coth(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("coth(5)"));
    }

    [Test]
    public void CschToStringTest()
    {
        var exp = new Csch(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("csch(5)"));
    }

    [Test]
    public void SechToStringTest()
    {
        var exp = new Sech(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("sech(5)"));
    }

    [Test]
    public void SinhToStringTest()
    {
        var exp = new Sinh(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("sinh(5)"));
    }

    [Test]
    public void TanhToStringTest()
    {
        var exp = new Tanh(new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("tanh(5)"));
    }

    #endregion

    #region Logical and Bitwise

    [Test]
    public void BoolToStringTest()
    {
        var exp = Bool.False;

        Assert.That(exp.ToString(), Is.EqualTo("False"));
    }

    [Test]
    public void AndAndToStringTest()
    {
        var exp = new And(Bool.True, new And(Bool.True, Bool.True));

        Assert.That(exp.ToString(), Is.EqualTo("True and (True and True)"));
    }

    [Test]
    public void OrToStringTest()
    {
        var exp = new Or(Bool.True, Bool.True);

        Assert.That(exp.ToString(), Is.EqualTo("True or True"));
    }

    [Test]
    public void OrOrToStringTest()
    {
        var exp = new Or(Bool.True, new Or(Bool.True, Bool.True));

        Assert.That(exp.ToString(), Is.EqualTo("True or (True or True)"));
    }

    [Test]
    public void XOrToStringTest()
    {
        var exp = new XOr(Bool.True, Bool.True);

        Assert.That(exp.ToString(), Is.EqualTo("True xor True"));
    }

    [Test]
    public void XOrXOrToStringTest()
    {
        var exp = new XOr(Bool.True, new XOr(Bool.True, Bool.True));

        Assert.That(exp.ToString(), Is.EqualTo("True xor (True xor True)"));
    }

    [Test]
    public void NotToStringTest()
    {
        var exp = new Not(Bool.True);

        Assert.That(exp.ToString(), Is.EqualTo("not(True)"));
    }

    [Test]
    public void EqualityToStringTest1()
    {
        var eq = new Equality(Bool.True, Bool.False);

        Assert.That(eq.ToString(), Is.EqualTo("True <=> False"));
    }

    [Test]
    public void EqualityToStringTest2()
    {
        var eq = new And(new Equality(Bool.True, Bool.False), Bool.False);

        Assert.That(eq.ToString(), Is.EqualTo("(True <=> False) and False"));
    }

    [Test]
    public void ImplicationToStringTest1()
    {
        var eq = new Implication(Bool.True, Bool.False);

        Assert.That(eq.ToString(), Is.EqualTo("True => False"));
    }

    [Test]
    public void ImplicationToStringTest2()
    {
        var eq = new And(new Implication(Bool.True, Bool.False), Bool.False);

        Assert.That(eq.ToString(), Is.EqualTo("(True => False) and False"));
    }

    [Test]
    public void NAndToStringTest1()
    {
        var eq = new NAnd(Bool.True, Bool.False);

        Assert.That(eq.ToString(), Is.EqualTo("True nand False"));
    }

    [Test]
    public void NAndToStringTest2()
    {
        var eq = new And(new NAnd(Bool.True, Bool.False), Bool.False);

        Assert.That(eq.ToString(), Is.EqualTo("(True nand False) and False"));
    }

    [Test]
    public void NOrToStringTest1()
    {
        var eq = new NOr(Bool.True, Bool.False);

        Assert.That(eq.ToString(), Is.EqualTo("True nor False"));
    }

    [Test]
    public void NOrToStringTest2()
    {
        var eq = new And(new NOr(Bool.True, Bool.False), Bool.False);

        Assert.That(eq.ToString(), Is.EqualTo("(True nor False) and False"));
    }

    #endregion

    #region Matrix

    [Test]
    public void MatrixToStringTest()
    {
        var matrix = new Matrix(new[]
        {
            new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) }),
            new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero })
        });

        Assert.That(matrix.ToString(), Is.EqualTo("{{1, -2}, {4, 0}}"));
    }

    [Test]
    public void DeterminantToStringTest()
    {
        var matrix = new Matrix(new[]
        {
            new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) }),
            new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero })
        });

        var det = new Determinant(matrix);

        Assert.That(det.ToString(), Is.EqualTo("det({{1, -2}, {4, 0}})"));
    }

    [Test]
    public void InverseToStringTest()
    {
        var matrix = new Matrix(new[]
        {
            new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) }),
            new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero })
        });

        var exp = new Inverse(matrix);

        Assert.That(exp.ToString(), Is.EqualTo("inverse({{1, -2}, {4, 0}})"));
    }

    [Test]
    public void DotProductToStringTest()
    {
        var left = new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) });
        var right = new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero });
        var exp = new DotProduct(left, right);

        Assert.That(exp.ToString(), Is.EqualTo("dotProduct({1, -2}, {4, 0})"));
    }

    [Test]
    public void CrossProductToStringTest()
    {
        var left = new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) });
        var right = new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero });
        var exp = new CrossProduct(left, right);

        Assert.That(exp.ToString(), Is.EqualTo("crossProduct({1, -2}, {4, 0})"));
    }

    [Test]
    public void TransposeToStringTest()
    {
        var matrix = new Matrix(new[]
        {
            new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) }),
            new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero })
        });

        var exp = new Transpose(matrix);

        Assert.That(exp.ToString(), Is.EqualTo("transpose({{1, -2}, {4, 0}})"));
    }

    #endregion

    #region Statistical

    [Test]
    public void AvgToStringTest()
    {
        var sum = new Avg(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("avg(1, 2)"));
    }

    [Test]
    public void AvgToStringTest2()
    {
        var sum = new Avg(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("avg({1, 2})"));
    }

    [Test]
    public void CountToStringTest()
    {
        var sum = new Count(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("count(1, 2)"));
    }

    [Test]
    public void CountToStringTest2()
    {
        var sum = new Count(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("count({1, 2})"));
    }

    [Test]
    public void ToStringTest()
    {
        var sum = new Max(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("max(1, 2)"));
    }

    [Test]
    public void ToStringTest2()
    {
        var sum = new Max(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("max({1, 2})"));
    }

    [Test]
    public void MinToStringTest()
    {
        var sum = new Min(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("min(1, 2)"));
    }

    [Test]
    public void MinToStringTest2()
    {
        var sum = new Min(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("min({1, 2})"));
    }

    [Test]
    public void ProductToStringTest()
    {
        var sum = new Product(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("product(1, 2)"));
    }

    [Test]
    public void ProductToStringTest2()
    {
        var sum = new Product(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("product({1, 2})"));
    }

    [Test]
    public void StdevpToStringTest()
    {
        var sum = new Stdevp(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("stdevp(1, 2)"));
    }

    [Test]
    public void StdevpToStringTest2()
    {
        var sum = new Stdevp(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("stdevp({1, 2})"));
    }

    [Test]
    public void StdevToStringTest()
    {
        var sum = new Stdev(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("stdev(1, 2)"));
    }

    [Test]
    public void StdevToStringTest2()
    {
        var sum = new Stdev(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("stdev({1, 2})"));
    }

    [Test]
    public void SumToStringTest()
    {
        var sum = new Sum(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("sum(1, 2)"));
    }

    [Test]
    public void SumToStringTest2()
    {
        var sum = new Sum(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("sum({1, 2})"));
    }

    [Test]
    public void VarpToStringTest()
    {
        var sum = new Varp(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("varp(1, 2)"));
    }

    [Test]
    public void VarpToStringTest2()
    {
        var sum = new Varp(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("varp({1, 2})"));
    }

    [Test]
    public void VarToStringTest()
    {
        var sum = new Var(new IExpression[] { Number.One, Number.Two });

        Assert.That(sum.ToString(), Is.EqualTo("var(1, 2)"));
    }

    [Test]
    public void VarToStringTest2()
    {
        var sum = new Var(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.That(sum.ToString(), Is.EqualTo("var({1, 2})"));
    }

    #endregion

    #region Programming

    [Test]
    public void AddAssignToString()
    {
        var exp = new AddAssign(Variable.X, new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("x += 5"));
    }

    [Test]
    public void SubAssignToString()
    {
        var exp = new SubAssign(Variable.X, new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("x -= 5"));
    }

    [Test]
    public void MulAssignToString()
    {
        var exp = new MulAssign(Variable.X, new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("x *= 5"));
    }

    [Test]
    public void DivAssignToString()
    {
        var exp = new DivAssign(Variable.X, new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("x /= 5"));
    }

    [Test]
    public void IncToString()
    {
        var exp = new Inc(Variable.X);

        Assert.That(exp.ToString(), Is.EqualTo("x++"));
    }

    [Test]
    public void DecToString()
    {
        var exp = new Dec(Variable.X);

        Assert.That(exp.ToString(), Is.EqualTo("x--"));
    }

    [Test]
    public void CondAndToString()
    {
        var exp = new ConditionalAnd(Bool.True, Bool.True);

        Assert.That(exp.ToString(), Is.EqualTo("True && True"));
    }

    [Test]
    public void CondAndCondAndToString()
    {
        var exp = new ConditionalAnd(Bool.True, new ConditionalAnd(Bool.True, Bool.True));

        Assert.That(exp.ToString(), Is.EqualTo("True && (True && True)"));
    }

    [Test]
    public void CondOrToString()
    {
        var exp = new ConditionalOr(Bool.True, Bool.True);

        Assert.That(exp.ToString(), Is.EqualTo("True || True"));
    }

    [Test]
    public void CondOrCondOrToString()
    {
        var exp = new ConditionalOr(Bool.True, new ConditionalOr(Bool.True, Bool.True));

        Assert.That(exp.ToString(), Is.EqualTo("True || (True || True)"));
    }

    [Test]
    public void EqualToString()
    {
        var exp = new Equal(new Number(5), new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("5 == 5"));
    }

    [Test]
    public void EqualEqualToString()
    {
        var exp = new Equal(Bool.True, new Equal(new Number(5), new Number(5)));

        Assert.That(exp.ToString(), Is.EqualTo("True == (5 == 5)"));
    }

    [Test]
    public void NotEqualToString()
    {
        var exp = new NotEqual(new Number(5), new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("5 != 5"));
    }

    [Test]
    public void NotEqualNotEqualToString()
    {
        var exp = new NotEqual(Bool.True, new NotEqual(new Number(5), new Number(5)));

        Assert.That(exp.ToString(), Is.EqualTo("True != (5 != 5)"));
    }

    [Test]
    public void LessToString()
    {
        var exp = new LessThan(new Number(5), new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("5 < 5"));
    }

    [Test]
    public void LessLessToString()
    {
        var exp = new ConditionalAnd(Bool.True, new LessThan(new Number(5), new Number(5)));

        Assert.That(exp.ToString(), Is.EqualTo("True && (5 < 5)"));
    }

    [Test]
    public void LessOrEqualToString()
    {
        var exp = new LessOrEqual(new Number(5), new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("5 <= 5"));
    }

    [Test]
    public void LessOrEqualLessOrEqualToString()
    {
        var exp = new ConditionalAnd(Bool.True, new LessOrEqual(new Number(5), new Number(5)));

        Assert.That(exp.ToString(), Is.EqualTo("True && (5 <= 5)"));
    }

    [Test]
    public void GreatToString()
    {
        var exp = new GreaterThan(new Number(5), new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("5 > 5"));
    }

    [Test]
    public void GreatGreatToString()
    {
        var exp = new ConditionalAnd(Bool.True, new GreaterThan(new Number(5), new Number(5)));

        Assert.That(exp.ToString(), Is.EqualTo("True && (5 > 5)"));
    }

    [Test]
    public void GreatOrEqualToString()
    {
        var exp = new GreaterOrEqual(new Number(5), new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("5 >= 5"));
    }

    [Test]
    public void GreatOrEqualGreatOrEqualToString()
    {
        var exp = new ConditionalAnd(Bool.True, new GreaterOrEqual(new Number(5), new Number(5)));

        Assert.That(exp.ToString(), Is.EqualTo("True && (5 >= 5)"));
    }

    [Test]
    public void IfToString()
    {
        var exp = new If(new Equal(new Number(5), new Number(5)), new Number(5));

        Assert.That(exp.ToString(), Is.EqualTo("if(5 == 5, 5)"));
    }

    [Test]
    public void IfElseToString()
    {
        var exp = new If(new Equal(new Number(5), new Number(5)), new Number(5), Number.Zero);

        Assert.That(exp.ToString(), Is.EqualTo("if(5 == 5, 5, 0)"));
    }

    [Test]
    public void ForToString()
    {
        var exp = new For(new Number(5), new Assign(Variable.X, Number.Zero), new Equal(new Number(5), new Number(5)), new AddAssign(Variable.X, Number.One));

        Assert.That(exp.ToString(), Is.EqualTo("for(5, x := 0, 5 == 5, x += 1)"));
    }

    [Test]
    public void WhileToString()
    {
        var exp = new While(new Number(5), new Equal(new Number(5), new Number(5)));

        Assert.That(exp.ToString(), Is.EqualTo("while(5, (5 == 5))"));
    }

    [Test]
    public void SignToString()
    {
        var exp = new Sign(new Number(-5));
        var str = exp.ToString();

        Assert.That(str, Is.EqualTo("sign(-5)"));
    }

    [Test]
    public void LeftShiftTest()
    {
        var exp = new LeftShift(Number.One, new Number(10));
        var str = exp.ToString();

        Assert.That(str, Is.EqualTo("1 << 10"));
    }

    [Test]
    public void RightShiftTest()
    {
        var exp = new RightShift(Number.One, new Number(10));
        var str = exp.ToString();

        Assert.That(str, Is.EqualTo("1 >> 10"));
    }

    [Test]
    public void LeftShiftAssignTest()
    {
        var exp = new LeftShiftAssign(Variable.X, new Number(10));
        var str = exp.ToString();

        Assert.That(str, Is.EqualTo("x <<= 10"));
    }

    [Test]
    public void RightShiftAssignTest()
    {
        var exp = new RightShiftAssign(Variable.X, new Number(10));
        var str = exp.ToString();

        Assert.That(str, Is.EqualTo("x >>= 10"));
    }

    [Test]
    public void ToBinTest()
    {
        var exp = new ToBin(new Number(10));

        Assert.That(exp.ToString(), Is.EqualTo("tobin(10)"));
    }

    [Test]
    public void ToOctTest()
    {
        var exp = new ToOct(new Number(10));

        Assert.That(exp.ToString(), Is.EqualTo("tooct(10)"));
    }

    [Test]
    public void ToHexTest()
    {
        var exp = new ToHex(new Number(10));

        Assert.That(exp.ToString(), Is.EqualTo("tohex(10)"));
    }

    #endregion
}