// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Matrices = xFunc.Maths.Expressions.Matrices;

namespace xFunc.Tests.Analyzers.Formatters;

public class CommonFormatterTest
{
    #region Common

    [Fact]
    public void AbsToStringTest()
    {
        var exp = new Abs(new Number(5));

        Assert.Equal("abs(5)", exp.ToString());
    }

    [Fact]
    public void AddToStringTest()
    {
        var exp = new Add(new Number(5), Number.Zero);

        Assert.Equal("5 + 0", exp.ToString());
    }

    [Fact]
    public void AddToStringBinTest()
    {
        var exp = new Mul(Variable.X, new Add(new Number(5), Number.Zero));

        Assert.Equal("x * (5 + 0)", exp.ToString());
    }

    [Fact]
    public void CeilToStringTest()
    {
        var ceil = new Ceil(new Number(5.55555555));

        Assert.Equal("ceil(5.55555555)", ceil.ToString());
    }

    [Fact]
    public void DefineToStringTest()
    {
        var exp = new Define(Variable.X, Number.Zero);

        Assert.Equal("x := 0", exp.ToString());
    }

    [Fact]
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

        Assert.Equal("del(((2 * x1) + (x2 ^ 2)) + (x3 ^ 3))", exp.ToString());
    }

    [Fact]
    public void DerivativeToStringExpTest()
    {
        var deriv = new Derivative(new Differentiator(), new Simplifier(), new Sin(Variable.X));

        Assert.Equal("deriv(sin(x))", deriv.ToString());
    }

    [Fact]
    public void DerivativeToStringVarTest()
    {
        var deriv = new Derivative(new Differentiator(), new Simplifier(), new Sin(Variable.X), Variable.X);

        Assert.Equal("deriv(sin(x), x)", deriv.ToString());
    }

    [Fact]
    public void DerivativeToStringPointTest()
    {
        var deriv = new Derivative(new Differentiator(), new Simplifier(), new Sin(Variable.X), Variable.X, Number.One);

        Assert.Equal("deriv(sin(x), x, 1)", deriv.ToString());
    }

    [Fact]
    public void DivToStringTest()
    {
        var exp = new Div(new Number(5), Number.Zero);

        Assert.Equal("5 / 0", exp.ToString());
    }

    [Fact]
    public void DivToStringBinTest()
    {
        var exp = new Mul(Variable.X, new Div(new Number(5), Number.Zero));

        Assert.Equal("x * (5 / 0)", exp.ToString());
    }

    [Fact]
    public void ExpToStringTest()
    {
        var exp = new Exp(new Number(5));

        Assert.Equal("exp(5)", exp.ToString());
    }

    [Fact]
    public void FactToStringTest()
    {
        var exp = new Fact(new Number(5));

        Assert.Equal("5!", exp.ToString());
    }

    [Fact]
    public void FloorToStringTest()
    {
        var exp = new Floor(new Number(5.55555555));

        Assert.Equal("floor(5.55555555)", exp.ToString());
    }

    [Fact]
    public void TruncToStringTest()
    {
        var exp = new Trunc(new Number(5.55555555));

        Assert.Equal("trunc(5.55555555)", exp.ToString());
    }

    [Fact]
    public void FracToStringTest()
    {
        var exp = new Frac(new Number(5.55555555));

        Assert.Equal("frac(5.55555555)", exp.ToString());
    }

    [Fact]
    public void GCDToStringTest()
    {
        var exp = new GCD(new Number(5), Number.Zero);

        Assert.Equal("gcd(5, 0)", exp.ToString());
    }

    [Fact]
    public void LCMToStringTest()
    {
        var exp = new LCM(new Number(5), Number.Zero);

        Assert.Equal("lcm(5, 0)", exp.ToString());
    }

    [Fact]
    public void LbToStringTest()
    {
        var exp = new Lb(new Number(5));

        Assert.Equal("lb(5)", exp.ToString());
    }

    [Fact]
    public void LgToStringTest()
    {
        var exp = new Lg(new Number(5));

        Assert.Equal("lg(5)", exp.ToString());
    }

    [Fact]
    public void LnToStringTest()
    {
        var exp = new Ln(new Number(5));

        Assert.Equal("ln(5)", exp.ToString());
    }

    [Fact]
    public void LogToStringTest()
    {
        var exp = new Log(Number.Zero, new Number(5));

        Assert.Equal("log(0, 5)", exp.ToString());
    }

    [Fact]
    public void ModToStringTest()
    {
        var exp = new Mod(new Number(5), Number.Zero);

        Assert.Equal("5 % 0", exp.ToString());
    }

    [Fact]
    public void ModToStringBinTest()
    {
        var exp = new Mul(Variable.X, new Mod(new Number(5), Number.Zero));

        Assert.Equal("x * (5 % 0)", exp.ToString());
    }

    [Fact]
    public void MulToStringTest()
    {
        var exp = new Mul(new Number(5), Number.Zero);

        Assert.Equal("5 * 0", exp.ToString());
    }

    [Fact]
    public void MulToStringAddTest()
    {
        var exp = new Add(Variable.X, new Mul(new Number(5), Number.Zero));

        Assert.Equal("x + (5 * 0)", exp.ToString());
    }

    [Fact]
    public void MulToStringSubTest()
    {
        var exp = new Sub(Variable.X, new Mul(new Number(5), Number.Zero));

        Assert.Equal("x - (5 * 0)", exp.ToString());
    }

    [Fact]
    public void MulToStringMulTest()
    {
        var exp = new Mul(Variable.X, new Mul(new Number(5), Number.Zero));

        Assert.Equal("x * (5 * 0)", exp.ToString());
    }

    [Fact]
    public void MulToStringDivTest()
    {
        var exp = new Div(Variable.X, new Mul(new Number(5), Number.Zero));

        Assert.Equal("x / (5 * 0)", exp.ToString());
    }

    [Fact]
    public void NumberTest()
    {
        var exp = new Number(3.3);

        Assert.Equal("3.3", exp.ToString());
    }

    [Fact]
    public void AngleNumberTest()
    {
        var exp = AngleValue.Degree(10).AsExpression();

        Assert.Equal("10 degree", exp.ToString());
    }

    [Fact]
    public void PowerNumberTest()
    {
        var exp = PowerValue.Watt(10).AsExpression();

        Assert.Equal("10 W", exp.ToString());
    }

    [Fact]
    public void TemperatureNumberTest()
    {
        var exp = TemperatureValue.Celsius(10).AsExpression();

        Assert.Equal("10 °C", exp.ToString());
    }

    [Fact]
    public void MassNumberTest()
    {
        var exp = MassValue.Gram(10).AsExpression();

        Assert.Equal("10 g", exp.ToString());
    }

    [Fact]
    public void LengthNumberTest()
    {
        var exp = LengthValue.Meter(10).AsExpression();

        Assert.Equal("10 m", exp.ToString());
    }

    [Fact]
    public void TimeNumberTest()
    {
        var exp = TimeValue.Second(10).AsExpression();

        Assert.Equal("10 s", exp.ToString());
    }

    [Fact]
    public void AreaNumberTest()
    {
        var exp = AreaValue.Meter(10).AsExpression();

        Assert.Equal("10 m^2", exp.ToString());
    }

    [Fact]
    public void VolumeNumberTest()
    {
        var exp = VolumeValue.Meter(10).AsExpression();

        Assert.Equal("10 m^3", exp.ToString());
    }

    [Fact]
    public void ToDegreeTest()
    {
        var exp = new ToDegree(new Number(10));

        Assert.Equal("todegree(10)", exp.ToString());
    }

    [Fact]
    public void ToRadianTest()
    {
        var exp = new ToRadian(new Number(10));

        Assert.Equal("toradian(10)", exp.ToString());
    }

    [Fact]
    public void ToGradianTest()
    {
        var exp = new ToGradian(new Number(10));

        Assert.Equal("togradian(10)", exp.ToString());
    }

    [Fact]
    public void ToNumberTest()
    {
        var exp = new ToNumber(AngleValue.Degree(10).AsExpression());

        Assert.Equal("tonumber(10 degree)", exp.ToString());
    }

    [Fact]
    public void NumberSubTest()
    {
        var exp = new Sub(Number.One, new Number(-3.3));

        Assert.Equal("1 - -3.3", exp.ToString());
    }

    [Fact]
    public void PowToStringTest()
    {
        var exp = new Pow(new Number(5), Number.Zero);

        Assert.Equal("5 ^ 0", exp.ToString());
    }

    [Fact]
    public void PowToStringAddTest()
    {
        var exp = new Add(Variable.X, new Pow(new Number(5), Number.Zero));

        Assert.Equal("x + (5 ^ 0)", exp.ToString());
    }

    [Fact]
    public void PowToStringSubTest()
    {
        var exp = new Sub(Variable.X, new Pow(new Number(5), Number.Zero));

        Assert.Equal("x - (5 ^ 0)", exp.ToString());
    }

    [Fact]
    public void PowToStringMulTest()
    {
        var exp = new Mul(Variable.X, new Pow(new Number(5), Number.Zero));

        Assert.Equal("x * (5 ^ 0)", exp.ToString());
    }

    [Fact]
    public void PowToStringDivTest()
    {
        var exp = new Div(Variable.X, new Pow(new Number(5), Number.Zero));

        Assert.Equal("x / (5 ^ 0)", exp.ToString());
    }

    [Fact]
    public void RootToStringTest()
    {
        var exp = new Root(new Number(5), Number.Zero);

        Assert.Equal("root(5, 0)", exp.ToString());
    }

    [Fact]
    public void RoundToStringTest()
    {
        var exp = new Round(new Number(5), Number.Zero);

        Assert.Equal("round(5, 0)", exp.ToString());
    }

    [Fact]
    public void SimplifyToStringTest()
    {
        var exp = new Simplify(new Simplifier(), new Sin(Variable.X));

        Assert.Equal("simplify(sin(x))", exp.ToString());
    }

    [Fact]
    public void SqrtToStringTest()
    {
        var exp = new Sqrt(new Number(5));

        Assert.Equal("sqrt(5)", exp.ToString());
    }

    [Fact]
    public void SubToStringTest()
    {
        var exp = new Sub(new Number(5), Number.Zero);

        Assert.Equal("5 - 0", exp.ToString());
    }

    [Fact]
    public void SubToStringSubTest()
    {
        var exp = new Sub(Variable.X, new Sub(new Number(5), Number.Zero));

        Assert.Equal("x - (5 - 0)", exp.ToString());
    }

    [Fact]
    public void SubToStringDivTest()
    {
        var exp = new Div(Variable.X, new Sub(new Number(5), Number.Zero));

        Assert.Equal("x / (5 - 0)", exp.ToString());
    }

    [Fact]
    public void UnaryMinusToStringTest()
    {
        var exp = new UnaryMinus(new Number(5));

        Assert.Equal("-5", exp.ToString());
    }

    [Fact]
    public void UnaryMinusToStringBinTest()
    {
        var exp = new UnaryMinus(new Add(new Number(5), Number.Zero));

        Assert.Equal("-(5 + 0)", exp.ToString());
    }

    [Fact]
    public void UnaryMinusToStringSubTest()
    {
        var exp = new Sub(Number.Zero, new UnaryMinus(new Number(5)));

        Assert.Equal("0 - -5", exp.ToString());
    }

    [Fact]
    public void UndefineToStringTest()
    {
        var exp = new Undefine(Variable.X);

        Assert.Equal("undef(x)", exp.ToString());
    }

    [Fact]
    public void UserFunctionToStringArgTest()
    {
        var exp = new UserFunction("f", new IExpression[] { new Number(5), Number.Two });

        Assert.Equal("f(5, 2)", exp.ToString());
    }

    [Fact]
    public void VariableTest()
    {
        var exp = Variable.X;

        Assert.Equal("x", exp.ToString());
    }

    [Fact]
    public void DelegateExpressionTest()
    {
        var exp = new DelegateExpression(param => 0d);

        Assert.Equal("{Delegate Expression}", exp.ToString());
    }

    [Fact]
    public void StringExpressionTest()
    {
        var exp = new StringExpression("hello");

        Assert.Equal("'hello'", exp.ToString());
    }

    [Fact]
    public void ConvertTest()
    {
        var exp = new xFunc.Maths.Expressions.Units.Convert(
            new Converter(),
            AngleValue.Degree(90).AsExpression(),
            new StringExpression("rad")
        );

        Assert.Equal("convert(90 degree, 'rad')", exp.ToString());
    }

    #endregion Common

    #region Complex Numbers

    [Fact]
    public void ComplexNumberPositiveNegativeToStringTest()
    {
        var complex = new ComplexNumber(3, -2);

        Assert.Equal("3-2i", complex.ToString());
    }

    [Fact]
    public void ComplexNumberNegativePositiveToStringTest()
    {
        var complex = new ComplexNumber(-3, 2);

        Assert.Equal("-3+2i", complex.ToString());
    }

    [Fact]
    public void ComplexNumberTwoPositiveToStringTest()
    {
        var complex = new ComplexNumber(3, 2);

        Assert.Equal("3+2i", complex.ToString());
    }

    [Fact]
    public void ComplexNumberTwoNegativeToStringTest()
    {
        var complex = new ComplexNumber(-3, -2);

        Assert.Equal("-3-2i", complex.ToString());
    }

    [Fact]
    public void ComplexNumberOnlyRealPartToStringTest()
    {
        var complex = new ComplexNumber(-3, 0);

        Assert.Equal("-3", complex.ToString());
    }

    [Fact]
    public void ComplexNumberOnlyImaginaryPartToStringTest()
    {
        var complex = new ComplexNumber(0, -2);

        Assert.Equal("-2i", complex.ToString());
    }

    [Fact]
    public void ComplexNumberBinaryToStringTest()
    {
        var exp = new Add(new ComplexNumber(3, 2), new ComplexNumber(3, 2));

        Assert.Equal("3+2i + 3+2i", exp.ToString());
    }

    [Fact]
    public void ComplexNumberAbsToStringTest()
    {
        var exp = new Abs(new ComplexNumber(3, 2));

        Assert.Equal("abs(3+2i)", exp.ToString());
    }

    [Fact]
    public void ComplexNumberIToStringTest()
    {
        var exp = new ComplexNumber(0, 1);

        Assert.Equal("i", exp.ToString());
    }

    [Fact]
    public void ComplexNumberNegativeIToStringTest()
    {
        var exp = new ComplexNumber(0, -1);

        Assert.Equal("-i", exp.ToString());
    }

    [Fact]
    public void ConjugateToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Conjugate(new ComplexNumber(complex));

        Assert.Equal("conjugate(3.1+2.5i)", exp.ToString());
    }

    [Fact]
    public void ImToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Im(new ComplexNumber(complex));

        Assert.Equal("im(3.1+2.5i)", exp.ToString());
    }

    [Fact]
    public void PhaseToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Phase(new ComplexNumber(complex));

        Assert.Equal("phase(3.1+2.5i)", exp.ToString());
    }

    [Fact]
    public void ReciprocalToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Reciprocal(new ComplexNumber(complex));

        Assert.Equal("reciprocal(3.1+2.5i)", exp.ToString());
    }

    [Fact]
    public void ReToStringTest()
    {
        var complex = new Complex(3.1, 2.5);
        var exp = new Re(new ComplexNumber(complex));

        Assert.Equal("re(3.1+2.5i)", exp.ToString());
    }

    [Fact]
    public void ToComplexToStringTest()
    {
        var exp = new ToComplex(Number.Two);

        Assert.Equal("tocomplex(2)", exp.ToString());
    }

    #endregion

    #region Trigonometric

    [Fact]
    public void ArccosToStringTest()
    {
        var exp = new Arccos(new Number(5));

        Assert.Equal("arccos(5)", exp.ToString());
    }

    [Fact]
    public void ArccotToStringTest()
    {
        var exp = new Arccot(new Number(5));

        Assert.Equal("arccot(5)", exp.ToString());
    }

    [Fact]
    public void ArccscToStringTest()
    {
        var exp = new Arccsc(new Number(5));

        Assert.Equal("arccsc(5)", exp.ToString());
    }

    [Fact]
    public void ArcsecToStringTest()
    {
        var exp = new Arcsec(new Number(5));

        Assert.Equal("arcsec(5)", exp.ToString());
    }

    [Fact]
    public void ArcsinToStringTest()
    {
        var exp = new Arcsin(new Number(5));

        Assert.Equal("arcsin(5)", exp.ToString());
    }

    [Fact]
    public void ArctanToStringTest()
    {
        var exp = new Arctan(new Number(5));

        Assert.Equal("arctan(5)", exp.ToString());
    }

    [Fact]
    public void CosToStringTest()
    {
        var exp = new Cos(new Number(5));

        Assert.Equal("cos(5)", exp.ToString());
    }

    [Fact]
    public void CotToStringTest()
    {
        var exp = new Cot(new Number(5));

        Assert.Equal("cot(5)", exp.ToString());
    }

    [Fact]
    public void CscToStringTest()
    {
        var exp = new Csc(new Number(5));

        Assert.Equal("csc(5)", exp.ToString());
    }

    [Fact]
    public void SecToStringTest()
    {
        var exp = new Sec(new Number(5));

        Assert.Equal("sec(5)", exp.ToString());
    }

    [Fact]
    public void SinToStringTest()
    {
        var exp = new Sin(new Number(5));

        Assert.Equal("sin(5)", exp.ToString());
    }

    [Fact]
    public void TanToStringTest()
    {
        var exp = new Tan(new Number(5));

        Assert.Equal("tan(5)", exp.ToString());
    }

    #endregion

    #region Hyperbolic

    [Fact]
    public void ArcoshToStringTest()
    {
        var exp = new Arcosh(new Number(5));

        Assert.Equal("arcosh(5)", exp.ToString());
    }

    [Fact]
    public void ArcothToStringTest()
    {
        var exp = new Arcoth(new Number(5));

        Assert.Equal("arcoth(5)", exp.ToString());
    }

    [Fact]
    public void ArcschToStringTest()
    {
        var exp = new Arcsch(new Number(5));

        Assert.Equal("arcsch(5)", exp.ToString());
    }

    [Fact]
    public void ArsechToStringTest()
    {
        var exp = new Arsech(new Number(5));

        Assert.Equal("arsech(5)", exp.ToString());
    }

    [Fact]
    public void ArsinhToStringTest()
    {
        var exp = new Arsinh(new Number(5));

        Assert.Equal("arsinh(5)", exp.ToString());
    }

    [Fact]
    public void ArtanhToStringTest()
    {
        var exp = new Artanh(new Number(5));

        Assert.Equal("artanh(5)", exp.ToString());
    }

    [Fact]
    public void CoshToStringTest()
    {
        var exp = new Cosh(new Number(5));

        Assert.Equal("cosh(5)", exp.ToString());
    }

    [Fact]
    public void CothToStringTest()
    {
        var exp = new Coth(new Number(5));

        Assert.Equal("coth(5)", exp.ToString());
    }

    [Fact]
    public void CschToStringTest()
    {
        var exp = new Csch(new Number(5));

        Assert.Equal("csch(5)", exp.ToString());
    }

    [Fact]
    public void SechToStringTest()
    {
        var exp = new Sech(new Number(5));

        Assert.Equal("sech(5)", exp.ToString());
    }

    [Fact]
    public void SinhToStringTest()
    {
        var exp = new Sinh(new Number(5));

        Assert.Equal("sinh(5)", exp.ToString());
    }

    [Fact]
    public void TanhToStringTest()
    {
        var exp = new Tanh(new Number(5));

        Assert.Equal("tanh(5)", exp.ToString());
    }

    #endregion

    #region Logical and Bitwise

    [Fact]
    public void BoolToStringTest()
    {
        var exp = Bool.False;

        Assert.Equal("False", exp.ToString());
    }

    [Fact]
    public void AndAndToStringTest()
    {
        var exp = new And(Bool.True, new And(Bool.True, Bool.True));

        Assert.Equal("True and (True and True)", exp.ToString());
    }

    [Fact]
    public void OrToStringTest()
    {
        var exp = new Or(Bool.True, Bool.True);

        Assert.Equal("True or True", exp.ToString());
    }

    [Fact]
    public void OrOrToStringTest()
    {
        var exp = new Or(Bool.True, new Or(Bool.True, Bool.True));

        Assert.Equal("True or (True or True)", exp.ToString());
    }

    [Fact]
    public void XOrToStringTest()
    {
        var exp = new XOr(Bool.True, Bool.True);

        Assert.Equal("True xor True", exp.ToString());
    }

    [Fact]
    public void XOrXOrToStringTest()
    {
        var exp = new XOr(Bool.True, new XOr(Bool.True, Bool.True));

        Assert.Equal("True xor (True xor True)", exp.ToString());
    }

    [Fact]
    public void NotToStringTest()
    {
        var exp = new Not(Bool.True);

        Assert.Equal("not(True)", exp.ToString());
    }

    [Fact]
    public void EqualityToStringTest1()
    {
        var eq = new Equality(Bool.True, Bool.False);

        Assert.Equal("True <=> False", eq.ToString());
    }

    [Fact]
    public void EqualityToStringTest2()
    {
        var eq = new And(new Equality(Bool.True, Bool.False), Bool.False);

        Assert.Equal("(True <=> False) and False", eq.ToString());
    }

    [Fact]
    public void ImplicationToStringTest1()
    {
        var eq = new Implication(Bool.True, Bool.False);

        Assert.Equal("True => False", eq.ToString());
    }

    [Fact]
    public void ImplicationToStringTest2()
    {
        var eq = new And(new Implication(Bool.True, Bool.False), Bool.False);

        Assert.Equal("(True => False) and False", eq.ToString());
    }

    [Fact]
    public void NAndToStringTest1()
    {
        var eq = new NAnd(Bool.True, Bool.False);

        Assert.Equal("True nand False", eq.ToString());
    }

    [Fact]
    public void NAndToStringTest2()
    {
        var eq = new And(new NAnd(Bool.True, Bool.False), Bool.False);

        Assert.Equal("(True nand False) and False", eq.ToString());
    }

    [Fact]
    public void NOrToStringTest1()
    {
        var eq = new NOr(Bool.True, Bool.False);

        Assert.Equal("True nor False", eq.ToString());
    }

    [Fact]
    public void NOrToStringTest2()
    {
        var eq = new And(new NOr(Bool.True, Bool.False), Bool.False);

        Assert.Equal("(True nor False) and False", eq.ToString());
    }

    #endregion

    #region Matrix

    [Fact]
    public void MatrixToStringTest()
    {
        var matrix = new Matrix(new[]
        {
            new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) }),
            new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero })
        });

        Assert.Equal("{{1, -2}, {4, 0}}", matrix.ToString());
    }

    [Fact]
    public void DeterminantToStringTest()
    {
        var matrix = new Matrix(new[]
        {
            new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) }),
            new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero })
        });

        var det = new Determinant(matrix);

        Assert.Equal("det({{1, -2}, {4, 0}})", det.ToString());
    }

    [Fact]
    public void InverseToStringTest()
    {
        var matrix = new Matrix(new[]
        {
            new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) }),
            new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero })
        });

        var exp = new Inverse(matrix);

        Assert.Equal("inverse({{1, -2}, {4, 0}})", exp.ToString());
    }

    [Fact]
    public void DotProductToStringTest()
    {
        var left = new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) });
        var right = new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero });
        var exp = new DotProduct(left, right);

        Assert.Equal("dotProduct({1, -2}, {4, 0})", exp.ToString());
    }

    [Fact]
    public void CrossProductToStringTest()
    {
        var left = new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) });
        var right = new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero });
        var exp = new CrossProduct(left, right);

        Assert.Equal("crossProduct({1, -2}, {4, 0})", exp.ToString());
    }

    [Fact]
    public void TransposeToStringTest()
    {
        var matrix = new Matrix(new[]
        {
            new Matrices.Vector(new IExpression[] { Number.One, new Number(-2) }),
            new Matrices.Vector(new IExpression[] { new Number(4), Number.Zero })
        });

        var exp = new Transpose(matrix);

        Assert.Equal("transpose({{1, -2}, {4, 0}})", exp.ToString());
    }

    #endregion

    #region Statistical

    [Fact]
    public void AvgToStringTest()
    {
        var sum = new Avg(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("avg(1, 2)", sum.ToString());
    }

    [Fact]
    public void AvgToStringTest2()
    {
        var sum = new Avg(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("avg({1, 2})", sum.ToString());
    }

    [Fact]
    public void CountToStringTest()
    {
        var sum = new Count(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("count(1, 2)", sum.ToString());
    }

    [Fact]
    public void CountToStringTest2()
    {
        var sum = new Count(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("count({1, 2})", sum.ToString());
    }

    [Fact]
    public void ToStringTest()
    {
        var sum = new Max(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("max(1, 2)", sum.ToString());
    }

    [Fact]
    public void ToStringTest2()
    {
        var sum = new Max(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("max({1, 2})", sum.ToString());
    }

    [Fact]
    public void MinToStringTest()
    {
        var sum = new Min(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("min(1, 2)", sum.ToString());
    }

    [Fact]
    public void MinToStringTest2()
    {
        var sum = new Min(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("min({1, 2})", sum.ToString());
    }

    [Fact]
    public void ProductToStringTest()
    {
        var sum = new Product(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("product(1, 2)", sum.ToString());
    }

    [Fact]
    public void ProductToStringTest2()
    {
        var sum = new Product(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("product({1, 2})", sum.ToString());
    }

    [Fact]
    public void StdevpToStringTest()
    {
        var sum = new Stdevp(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("stdevp(1, 2)", sum.ToString());
    }

    [Fact]
    public void StdevpToStringTest2()
    {
        var sum = new Stdevp(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("stdevp({1, 2})", sum.ToString());
    }

    [Fact]
    public void StdevToStringTest()
    {
        var sum = new Stdev(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("stdev(1, 2)", sum.ToString());
    }

    [Fact]
    public void StdevToStringTest2()
    {
        var sum = new Stdev(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("stdev({1, 2})", sum.ToString());
    }

    [Fact]
    public void SumToStringTest()
    {
        var sum = new Sum(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("sum(1, 2)", sum.ToString());
    }

    [Fact]
    public void SumToStringTest2()
    {
        var sum = new Sum(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("sum({1, 2})", sum.ToString());
    }

    [Fact]
    public void VarpToStringTest()
    {
        var sum = new Varp(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("varp(1, 2)", sum.ToString());
    }

    [Fact]
    public void VarpToStringTest2()
    {
        var sum = new Varp(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("varp({1, 2})", sum.ToString());
    }

    [Fact]
    public void VarToStringTest()
    {
        var sum = new Var(new IExpression[] { Number.One, Number.Two });

        Assert.Equal("var(1, 2)", sum.ToString());
    }

    [Fact]
    public void VarToStringTest2()
    {
        var sum = new Var(new IExpression[] { new Matrices.Vector(new IExpression[] { Number.One, Number.Two }) });

        Assert.Equal("var({1, 2})", sum.ToString());
    }

    #endregion

    #region Programming

    [Fact]
    public void AddAssignToString()
    {
        var exp = new AddAssign(Variable.X, new Number(5));

        Assert.Equal("x += 5", exp.ToString());
    }

    [Fact]
    public void SubAssignToString()
    {
        var exp = new SubAssign(Variable.X, new Number(5));

        Assert.Equal("x -= 5", exp.ToString());
    }

    [Fact]
    public void MulAssignToString()
    {
        var exp = new MulAssign(Variable.X, new Number(5));

        Assert.Equal("x *= 5", exp.ToString());
    }

    [Fact]
    public void DivAssignToString()
    {
        var exp = new DivAssign(Variable.X, new Number(5));

        Assert.Equal("x /= 5", exp.ToString());
    }

    [Fact]
    public void IncToString()
    {
        var exp = new Inc(Variable.X);

        Assert.Equal("x++", exp.ToString());
    }

    [Fact]
    public void DecToString()
    {
        var exp = new Dec(Variable.X);

        Assert.Equal("x--", exp.ToString());
    }

    [Fact]
    public void CondAndToString()
    {
        var exp = new ConditionalAnd(Bool.True, Bool.True);

        Assert.Equal("True && True", exp.ToString());
    }

    [Fact]
    public void CondAndCondAndToString()
    {
        var exp = new ConditionalAnd(Bool.True, new ConditionalAnd(Bool.True, Bool.True));

        Assert.Equal("True && (True && True)", exp.ToString());
    }

    [Fact]
    public void CondOrToString()
    {
        var exp = new ConditionalOr(Bool.True, Bool.True);

        Assert.Equal("True || True", exp.ToString());
    }

    [Fact]
    public void CondOrCondOrToString()
    {
        var exp = new ConditionalOr(Bool.True, new ConditionalOr(Bool.True, Bool.True));

        Assert.Equal("True || (True || True)", exp.ToString());
    }

    [Fact]
    public void EqualToString()
    {
        var exp = new Equal(new Number(5), new Number(5));

        Assert.Equal("5 == 5", exp.ToString());
    }

    [Fact]
    public void EqualEqualToString()
    {
        var exp = new Equal(Bool.True, new Equal(new Number(5), new Number(5)));

        Assert.Equal("True == (5 == 5)", exp.ToString());
    }

    [Fact]
    public void NotEqualToString()
    {
        var exp = new NotEqual(new Number(5), new Number(5));

        Assert.Equal("5 != 5", exp.ToString());
    }

    [Fact]
    public void NotEqualNotEqualToString()
    {
        var exp = new NotEqual(Bool.True, new NotEqual(new Number(5), new Number(5)));

        Assert.Equal("True != (5 != 5)", exp.ToString());
    }

    [Fact]
    public void LessToString()
    {
        var exp = new LessThan(new Number(5), new Number(5));

        Assert.Equal("5 < 5", exp.ToString());
    }

    [Fact]
    public void LessLessToString()
    {
        var exp = new ConditionalAnd(Bool.True, new LessThan(new Number(5), new Number(5)));

        Assert.Equal("True && (5 < 5)", exp.ToString());
    }

    [Fact]
    public void LessOrEqualToString()
    {
        var exp = new LessOrEqual(new Number(5), new Number(5));

        Assert.Equal("5 <= 5", exp.ToString());
    }

    [Fact]
    public void LessOrEqualLessOrEqualToString()
    {
        var exp = new ConditionalAnd(Bool.True, new LessOrEqual(new Number(5), new Number(5)));

        Assert.Equal("True && (5 <= 5)", exp.ToString());
    }

    [Fact]
    public void GreatToString()
    {
        var exp = new GreaterThan(new Number(5), new Number(5));

        Assert.Equal("5 > 5", exp.ToString());
    }

    [Fact]
    public void GreatGreatToString()
    {
        var exp = new ConditionalAnd(Bool.True, new GreaterThan(new Number(5), new Number(5)));

        Assert.Equal("True && (5 > 5)", exp.ToString());
    }

    [Fact]
    public void GreatOrEqualToString()
    {
        var exp = new GreaterOrEqual(new Number(5), new Number(5));

        Assert.Equal("5 >= 5", exp.ToString());
    }

    [Fact]
    public void GreatOrEqualGreatOrEqualToString()
    {
        var exp = new ConditionalAnd(Bool.True, new GreaterOrEqual(new Number(5), new Number(5)));

        Assert.Equal("True && (5 >= 5)", exp.ToString());
    }

    [Fact]
    public void IfToString()
    {
        var exp = new If(new Equal(new Number(5), new Number(5)), new Number(5));

        Assert.Equal("if(5 == 5, 5)", exp.ToString());
    }

    [Fact]
    public void IfElseToString()
    {
        var exp = new If(new Equal(new Number(5), new Number(5)), new Number(5), Number.Zero);

        Assert.Equal("if(5 == 5, 5, 0)", exp.ToString());
    }

    [Fact]
    public void ForToString()
    {
        var exp = new For(new Number(5), new Define(Variable.X, Number.Zero), new Equal(new Number(5), new Number(5)), new AddAssign(Variable.X, Number.One));

        Assert.Equal("for(5, x := 0, 5 == 5, x += 1)", exp.ToString());
    }

    [Fact]
    public void WhileToString()
    {
        var exp = new While(new Number(5), new Equal(new Number(5), new Number(5)));

        Assert.Equal("while(5, (5 == 5))", exp.ToString());
    }

    [Fact]
    public void SignToString()
    {
        var exp = new Sign(new Number(-5));
        var str = exp.ToString();

        Assert.Equal("sign(-5)", str);
    }

    [Fact]
    public void LeftShiftTest()
    {
        var exp = new LeftShift(Number.One, new Number(10));
        var str = exp.ToString();

        Assert.Equal("1 << 10", str);
    }

    [Fact]
    public void RightShiftTest()
    {
        var exp = new RightShift(Number.One, new Number(10));
        var str = exp.ToString();

        Assert.Equal("1 >> 10", str);
    }

    [Fact]
    public void LeftShiftAssignTest()
    {
        var exp = new LeftShiftAssign(Variable.X, new Number(10));
        var str = exp.ToString();

        Assert.Equal("x <<= 10", str);
    }

    [Fact]
    public void RightShiftAssignTest()
    {
        var exp = new RightShiftAssign(Variable.X, new Number(10));
        var str = exp.ToString();

        Assert.Equal("x >>= 10", str);
    }

    [Fact]
    public void ToBinTest()
    {
        var exp = new ToBin(new Number(10));

        Assert.Equal("tobin(10)", exp.ToString());
    }

    [Fact]
    public void ToOctTest()
    {
        var exp = new ToOct(new Number(10));

        Assert.Equal("tooct(10)", exp.ToString());
    }

    [Fact]
    public void ToHexTest()
    {
        var exp = new ToHex(new Number(10));

        Assert.Equal("tohex(10)", exp.ToString());
    }

    #endregion
}