using System;
using System.Numerics;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Hyperbolic;
using xFunc.Maths.Expressions.LogicalAndBitwise;
using xFunc.Maths.Expressions.Matrices;
using xFunc.Maths.Expressions.Programming;
using xFunc.Maths.Expressions.Statistical;
using xFunc.Maths.Expressions.Trigonometric;
using Xunit;

namespace xFunc.Tests.Analyzers.Formatters
{

    public class CommonFormatterTest
    {

        private CommonFormatter commoonFormatter = new CommonFormatter();

        #region Common

        [Fact]
        public void AbsToStringTest()
        {
            var exp = new Abs(new Number(5));

            Assert.Equal("abs(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void AddToStringTest()
        {
            var exp = new Add(new Number(5), new Number(0));

            Assert.Equal("5 + 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void AddToStringBinTest()
        {
            var exp = new Mul(Variable.X, new Add(new Number(5), new Number(0)));

            Assert.Equal("x * (5 + 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CeilToStringTest()
        {
            var ceil = new Ceil(new Number(5.55555555));

            Assert.Equal("ceil(5.55555555)", ceil.ToString(commoonFormatter));
        }

        [Fact]
        public void DefineToStringTest()
        {
            var exp = new Define(Variable.X, new Number(0));

            Assert.Equal("x := 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void DelToStringTest()
        {
            var exp = new Del(null, null, new Add(new Add(new Mul(new Number(2), new Variable("x1")), new Pow(new Variable("x2"), new Number(2))), new Pow(new Variable("x3"), new Number(3))));

            Assert.Equal("del(2 * x1 + x2 ^ 2 + x3 ^ 3)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void DerivativeToStringExpTest()
        {
            var deriv = new Derivative(null, null, new Sin(Variable.X));

            Assert.Equal("deriv(sin(x))", deriv.ToString(commoonFormatter));
        }

        [Fact]
        public void DerivativeToStringVarTest()
        {
            var deriv = new Derivative(null, null, new Sin(Variable.X), Variable.X);

            Assert.Equal("deriv(sin(x), x)", deriv.ToString(commoonFormatter));
        }

        [Fact]
        public void DerivativeToStringPointTest()
        {
            var deriv = new Derivative(null, null, new Sin(Variable.X), Variable.X, new Number(1));

            Assert.Equal("deriv(sin(x), x, 1)", deriv.ToString(commoonFormatter));
        }

        [Fact]
        public void DivToStringTest()
        {
            var exp = new Div(new Number(5), new Number(0));

            Assert.Equal("5 / 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void DivToStringBinTest()
        {
            var exp = new Mul(Variable.X, new Div(new Number(5), new Number(0)));

            Assert.Equal("x * (5 / 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ExpToStringTest()
        {
            var exp = new Exp(new Number(5));

            Assert.Equal("exp(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void FactToStringTest()
        {
            var exp = new Fact(new Number(5));

            Assert.Equal("5!", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void FloorToStringTest()
        {
            var exp = new Floor(new Number(5.55555555));

            Assert.Equal("floor(5.55555555)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void GCDToStringTest()
        {
            var exp = new GCD(new Number(5), new Number(0));

            Assert.Equal("gcd(5, 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void LCMToStringTest()
        {
            var exp = new LCM(new Number(5), new Number(0));

            Assert.Equal("lcm(5, 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void LbToStringTest()
        {
            var exp = new Lb(new Number(5));

            Assert.Equal("lb(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void LgToStringTest()
        {
            var exp = new Lg(new Number(5));

            Assert.Equal("lg(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void LnToStringTest()
        {
            var exp = new Ln(new Number(5));

            Assert.Equal("ln(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void LogToStringTest()
        {
            var exp = new Log(new Number(5), new Number(0));

            Assert.Equal("log(0, 5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ModToStringTest()
        {
            var exp = new Mod(new Number(5), new Number(0));

            Assert.Equal("5 % 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ModToStringBinTest()
        {
            var exp = new Mul(Variable.X, new Mod(new Number(5), new Number(0)));

            Assert.Equal("x * (5 % 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void MulToStringTest()
        {
            var exp = new Mul(new Number(5), new Number(0));

            Assert.Equal("5 * 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void MulToStringAddTest()
        {
            var exp = new Add(Variable.X, new Mul(new Number(5), new Number(0)));

            Assert.Equal("x + 5 * 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void MulToStringSubTest()
        {
            var exp = new Sub(Variable.X, new Mul(new Number(5), new Number(0)));

            Assert.Equal("x - 5 * 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void MulToStringMulTest()
        {
            var exp = new Mul(Variable.X, new Mul(new Number(5), new Number(0)));

            Assert.Equal("x * 5 * 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void MulToStringDivTest()
        {
            var exp = new Div(Variable.X, new Mul(new Number(5), new Number(0)));

            Assert.Equal("x / (5 * 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void NumberTest()
        {
            var exp = new Number(3.3);

            Assert.Equal("3.3", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void NumberSubTest()
        {
            var exp = new Sub(new Number(1), new Number(-3.3));

            Assert.Equal("1 - (-3.3)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void PowToStringTest()
        {
            var exp = new Pow(new Number(5), new Number(0));

            Assert.Equal("5 ^ 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void PowToStringAddTest()
        {
            var exp = new Add(Variable.X, new Pow(new Number(5), new Number(0)));

            Assert.Equal("x + 5 ^ 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void PowToStringSubTest()
        {
            var exp = new Sub(Variable.X, new Pow(new Number(5), new Number(0)));

            Assert.Equal("x - 5 ^ 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void PowToStringMulTest()
        {
            var exp = new Mul(Variable.X, new Pow(new Number(5), new Number(0)));

            Assert.Equal("x * 5 ^ 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void PowToStringDivTest()
        {
            var exp = new Div(Variable.X, new Pow(new Number(5), new Number(0)));

            Assert.Equal("x / (5 ^ 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void RootToStringTest()
        {
            var exp = new Root(new Number(5), new Number(0));

            Assert.Equal("root(5, 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void RoundToStringTest()
        {
            var exp = new Round(new Number(5), new Number(0));

            Assert.Equal("round(5, 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SimplifyToStringTest()
        {
            var exp = new Simplify(null, new Sin(Variable.X));

            Assert.Equal("simplify(sin(x))", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SqrtToStringTest()
        {
            var exp = new Sqrt(new Number(5));

            Assert.Equal("sqrt(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SubToStringTest()
        {
            var exp = new Sub(new Number(5), new Number(0));

            Assert.Equal("5 - 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SubToStringSubTest()
        {
            var exp = new Sub(Variable.X, new Sub(new Number(5), new Number(0)));

            Assert.Equal("x - 5 - 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SubToStringDivTest()
        {
            var exp = new Div(Variable.X, new Sub(new Number(5), new Number(0)));

            Assert.Equal("x / (5 - 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void UnaryMinusToStringTest()
        {
            var exp = new UnaryMinus(new Number(5));

            Assert.Equal("-5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void UnaryMinusToStringBinTest()
        {
            var exp = new UnaryMinus(new Add(new Number(5), new Number(0)));

            Assert.Equal("-(5 + 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void UnaryMinusToStringSubTest()
        {
            var exp = new Sub(new Number(0), new UnaryMinus(new Number(5)));

            Assert.Equal("0 - (-5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void UndefineToStringTest()
        {
            var exp = new Undefine(Variable.X);

            Assert.Equal("undef(x)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void UserFunctionToStringArgTest()
        {
            var exp = new UserFunction("f", new[] { new Number(5), new Number(2) }, 1);

            Assert.Equal("f(5, 2)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void UserFunctionToStringCountTest()
        {
            var exp = new UserFunction("f", 3);

            Assert.Equal("f(x1, x2, x3)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void VariableTest()
        {
            var exp = Variable.X;

            Assert.Equal("x", exp.ToString(commoonFormatter));
        }

        #endregion Common

        #region Complex Numbers

        [Fact]
        public void ComplexNumberPositiveNegativeToStringTest()
        {
            var complex = new ComplexNumber(3, -2);

            Assert.Equal("3-2i", complex.ToString(commoonFormatter));
        }

        [Fact]
        public void ComplexNumberNegativePositiveToStringTest()
        {
            var complex = new ComplexNumber(-3, 2);

            Assert.Equal("-3+2i", complex.ToString(commoonFormatter));
        }

        [Fact]
        public void ComplexNumberTwoPositiveToStringTest()
        {
            var complex = new ComplexNumber(3, 2);

            Assert.Equal("3+2i", complex.ToString(commoonFormatter));
        }

        [Fact]
        public void ComplexNumberTwoNegativeToStringTest()
        {
            var complex = new ComplexNumber(-3, -2);

            Assert.Equal("-3-2i", complex.ToString(commoonFormatter));
        }

        [Fact]
        public void ComplexNumberOnlyRealPartToStringTest()
        {
            var complex = new ComplexNumber(-3, 0);

            Assert.Equal("-3", complex.ToString(commoonFormatter));
        }

        [Fact]
        public void ComplexNumberOnlyImaginaryPartToStringTest()
        {
            var complex = new ComplexNumber(0, -2);

            Assert.Equal("-2i", complex.ToString(commoonFormatter));
        }

        [Fact]
        public void ComplexNumberBinaryToStringTest()
        {
            var exp = new Add(new ComplexNumber(3, 2), new ComplexNumber(3, 2));

            Assert.Equal("3+2i + 3+2i", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ComplexNumberAbsToStringTest()
        {
            var exp = new Abs(new ComplexNumber(3, 2));

            Assert.Equal("abs(3+2i)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ComplexNumberIToStringTest()
        {
            var exp = new ComplexNumber(0, 1);

            Assert.Equal("i", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ComplexNumberNegativeIToStringTest()
        {
            var exp = new ComplexNumber(0, -1);

            Assert.Equal("-i", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ConjugateToStringTest()
        {
            var complex = new Complex(3.1, 2.5);
            var exp = new Conjugate(new ComplexNumber(complex));

            Assert.Equal("conjugate(3.1+2.5i)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ImToStringTest()
        {
            var complex = new Complex(3.1, 2.5);
            var exp = new Im(new ComplexNumber(complex));

            Assert.Equal("im(3.1+2.5i)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void PhaseToStringTest()
        {
            var complex = new Complex(3.1, 2.5);
            var exp = new Phase(new ComplexNumber(complex));

            Assert.Equal("phase(3.1+2.5i)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ReciprocalToStringTest()
        {
            var complex = new Complex(3.1, 2.5);
            var exp = new Reciprocal(new ComplexNumber(complex));

            Assert.Equal("reciprocal(3.1+2.5i)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ReToStringTest()
        {
            var complex = new Complex(3.1, 2.5);
            var exp = new Re(new ComplexNumber(complex));

            Assert.Equal("re(3.1+2.5i)", exp.ToString(commoonFormatter));
        }

        #endregion

        #region Trigonometric

        [Fact]
        public void ArccosToStringTest()
        {
            var exp = new Arccos(new Number(5));

            Assert.Equal("arccos(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArccotToStringTest()
        {
            var exp = new Arccot(new Number(5));

            Assert.Equal("arccot(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArccscToStringTest()
        {
            var exp = new Arccsc(new Number(5));

            Assert.Equal("arccsc(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArcsecToStringTest()
        {
            var exp = new Arcsec(new Number(5));

            Assert.Equal("arcsec(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArcsinToStringTest()
        {
            var exp = new Arcsin(new Number(5));

            Assert.Equal("arcsin(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArctanToStringTest()
        {
            var exp = new Arctan(new Number(5));

            Assert.Equal("arctan(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CosToStringTest()
        {
            var exp = new Cos(new Number(5));

            Assert.Equal("cos(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CotToStringTest()
        {
            var exp = new Cot(new Number(5));

            Assert.Equal("cot(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CscToStringTest()
        {
            var exp = new Csc(new Number(5));

            Assert.Equal("csc(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SecToStringTest()
        {
            var exp = new Sec(new Number(5));

            Assert.Equal("sec(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SinToStringTest()
        {
            var exp = new Sin(new Number(5));

            Assert.Equal("sin(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void TanToStringTest()
        {
            var exp = new Tan(new Number(5));

            Assert.Equal("tan(5)", exp.ToString(commoonFormatter));
        }

        #endregion

        #region Hyperbolic

        [Fact]
        public void ArcoshToStringTest()
        {
            var exp = new Arcosh(new Number(5));

            Assert.Equal("arcosh(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArcothToStringTest()
        {
            var exp = new Arcoth(new Number(5));

            Assert.Equal("arcoth(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArcschToStringTest()
        {
            var exp = new Arcsch(new Number(5));

            Assert.Equal("arcsch(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArsechToStringTest()
        {
            var exp = new Arsech(new Number(5));

            Assert.Equal("arsech(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArsinhToStringTest()
        {
            var exp = new Arsinh(new Number(5));

            Assert.Equal("arsinh(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ArtanhToStringTest()
        {
            var exp = new Artanh(new Number(5));

            Assert.Equal("artanh(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CoshToStringTest()
        {
            var exp = new Cosh(new Number(5));

            Assert.Equal("cosh(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CothToStringTest()
        {
            var exp = new Coth(new Number(5));

            Assert.Equal("coth(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CschToStringTest()
        {
            var exp = new Csch(new Number(5));

            Assert.Equal("csch(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SechToStringTest()
        {
            var exp = new Sech(new Number(5));

            Assert.Equal("sech(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SinhToStringTest()
        {
            var exp = new Sinh(new Number(5));

            Assert.Equal("sinh(5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void TanhToStringTest()
        {
            var exp = new Tanh(new Number(5));

            Assert.Equal("tanh(5)", exp.ToString(commoonFormatter));
        }

        #endregion

        #region Logical and Bitwise

        [Fact]
        public void BoolToStringTest()
        {
            var exp = new Bool(false);

            Assert.Equal("False", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void AndAndToStringTest()
        {
            var exp = new Maths.Expressions.LogicalAndBitwise.And(new Bool(true), new Maths.Expressions.LogicalAndBitwise.And(new Bool(true), new Bool(true)));

            Assert.Equal("True and (True and True)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void OrToStringTest()
        {
            var exp = new Maths.Expressions.LogicalAndBitwise.Or(new Bool(true), new Bool(true));

            Assert.Equal("True or True", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void OrOrToStringTest()
        {
            var exp = new Maths.Expressions.LogicalAndBitwise.Or(new Bool(true), new Maths.Expressions.LogicalAndBitwise.Or(new Bool(true), new Bool(true)));

            Assert.Equal("True or (True or True)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void XOrToStringTest()
        {
            var exp = new XOr(new Bool(true), new Bool(true));

            Assert.Equal("True xor True", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void XOrXOrToStringTest()
        {
            var exp = new XOr(new Bool(true), new XOr(new Bool(true), new Bool(true)));

            Assert.Equal("True xor (True xor True)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void NotToStringTest()
        {
            var exp = new Not(new Bool(true));

            Assert.Equal("not(True)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void EqualityToStringTest1()
        {
            var eq = new Equality(new Bool(true), new Bool(false));

            Assert.Equal("True <=> False", eq.ToString(commoonFormatter));
        }

        [Fact]
        public void EqualityToStringTest2()
        {
            var eq = new Maths.Expressions.LogicalAndBitwise.And(new Equality(new Bool(true), new Bool(false)), new Bool(false));

            Assert.Equal("(True <=> False) and False", eq.ToString(commoonFormatter));
        }

        [Fact]
        public void ImplicationToStringTest1()
        {
            var eq = new Implication(new Bool(true), new Bool(false));

            Assert.Equal("True => False", eq.ToString(commoonFormatter));
        }

        [Fact]
        public void ImplicationToStringTest2()
        {
            var eq = new Maths.Expressions.LogicalAndBitwise.And(new Implication(new Bool(true), new Bool(false)), new Bool(false));

            Assert.Equal("(True => False) and False", eq.ToString(commoonFormatter));
        }

        [Fact]
        public void NAndToStringTest1()
        {
            var eq = new NAnd(new Bool(true), new Bool(false));

            Assert.Equal("True nand False", eq.ToString(commoonFormatter));
        }

        [Fact]
        public void NAndToStringTest2()
        {
            var eq = new Maths.Expressions.LogicalAndBitwise.And(new NAnd(new Bool(true), new Bool(false)), new Bool(false));

            Assert.Equal("(True nand False) and False", eq.ToString(commoonFormatter));
        }

        [Fact]
        public void NOrToStringTest1()
        {
            var eq = new NOr(new Bool(true), new Bool(false));

            Assert.Equal("True nor False", eq.ToString(commoonFormatter));
        }

        [Fact]
        public void NOrToStringTest2()
        {
            var eq = new Maths.Expressions.LogicalAndBitwise.And(new NOr(new Bool(true), new Bool(false)), new Bool(false));

            Assert.Equal("(True nor False) and False", eq.ToString(commoonFormatter));
        }

        #endregion

        #region Matrix

        [Fact]
        public void DeterminantToStringTest()
        {
            var matrix = new Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(-2) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(4), new Number(0) })
            });

            var det = new Determinant(matrix);

            Assert.Equal("det({{1, -2}, {4, 0}})", det.ToString(commoonFormatter));
        }

        [Fact]
        public void InverseToStringTest()
        {
            var matrix = new Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(-2) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(4), new Number(0) })
            });

            var exp = new Inverse(matrix);

            Assert.Equal("inverse({{1, -2}, {4, 0}})", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void TransposeToStringTest()
        {
            var matrix = new Matrix(new[]
            {
                new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(-2) }),
                new Maths.Expressions.Matrices.Vector(new[] { new Number(4), new Number(0) })
            });

            var exp = new Transpose(matrix);

            Assert.Equal("transpose({{1, -2}, {4, 0}})", exp.ToString(commoonFormatter));
        }

        #endregion

        #region Statistical

        [Fact]
        public void AvgToStringTest()
        {
            var sum = new Avg(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("avg(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void AvgToStringTest2()
        {
            var sum = new Avg(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("avg({1, 2})", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void CountToStringTest()
        {
            var sum = new Count(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("count(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void CountToStringTest2()
        {
            var sum = new Count(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("count({1, 2})", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void ToStringTest()
        {
            var sum = new Max(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("max(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void ToStringTest2()
        {
            var sum = new Max(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("max({1, 2})", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void MinToStringTest()
        {
            var sum = new Min(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("min(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void MinToStringTest2()
        {
            var sum = new Min(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("min({1, 2})", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void ProductToStringTest()
        {
            var sum = new Product(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("product(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void ProductToStringTest2()
        {
            var sum = new Product(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("product({1, 2})", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void StdevpToStringTest()
        {
            var sum = new Stdevp(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("stdevp(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void StdevpToStringTest2()
        {
            var sum = new Stdevp(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("stdevp({1, 2})", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void StdevToStringTest()
        {
            var sum = new Stdev(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("stdev(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void StdevToStringTest2()
        {
            var sum = new Stdev(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("stdev({1, 2})", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void SumToStringTest()
        {
            var sum = new Sum(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("sum(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void SumToStringTest2()
        {
            var sum = new Sum(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("sum({1, 2})", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void VarpToStringTest()
        {
            var sum = new Varp(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("varp(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void VarpToStringTest2()
        {
            var sum = new Varp(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("varp({1, 2})", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void VarToStringTest()
        {
            var sum = new Var(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("var(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void VarToStringTest2()
        {
            var sum = new Var(new[] { new Maths.Expressions.Matrices.Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("var({1, 2})", sum.ToString(commoonFormatter));
        }

        #endregion

        #region Programming

        [Fact]
        public void AddAssignToString()
        {
            var exp = new AddAssign(Variable.X, new Number(5));

            Assert.Equal("x += 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void SubAssignToString()
        {
            var exp = new SubAssign(Variable.X, new Number(5));

            Assert.Equal("x -= 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void MulAssignToString()
        {
            var exp = new MulAssign(Variable.X, new Number(5));

            Assert.Equal("x *= 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void DivAssignToString()
        {
            var exp = new DivAssign(Variable.X, new Number(5));

            Assert.Equal("x /= 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void IncToString()
        {
            var exp = new Inc(Variable.X);

            Assert.Equal("x++", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void DecToString()
        {
            var exp = new Dec(Variable.X);

            Assert.Equal("x--", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CondAndToString()
        {
            var exp = new Maths.Expressions.Programming.And(new Bool(true), new Bool(true));

            Assert.Equal("True && True", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CondAndCondAndToString()
        {
            var exp = new Maths.Expressions.Programming.And(new Bool(true), new Maths.Expressions.Programming.And(new Bool(true), new Bool(true)));

            Assert.Equal("True && (True && True)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CondOrToString()
        {
            var exp = new Maths.Expressions.Programming.Or(new Bool(true), new Bool(true));

            Assert.Equal("True || True", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void CondOrCondOrToString()
        {
            var exp = new Maths.Expressions.Programming.Or(new Bool(true), new Maths.Expressions.Programming.Or(new Bool(true), new Bool(true)));

            Assert.Equal("True || (True || True)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void EqualToString()
        {
            var exp = new Equal(new Number(5), new Number(5));

            Assert.Equal("5 == 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void EqualEqualToString()
        {
            var exp = new Equal(new Bool(true), new Equal(new Number(5), new Number(5)));

            Assert.Equal("True == (5 == 5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void NotEqualToString()
        {
            var exp = new NotEqual(new Number(5), new Number(5));

            Assert.Equal("5 != 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void NotEqualNotEqualToString()
        {
            var exp = new NotEqual(new Bool(true), new NotEqual(new Number(5), new Number(5)));

            Assert.Equal("True != (5 != 5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void LessToString()
        {
            var exp = new LessThan(new Number(5), new Number(5));

            Assert.Equal("5 < 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void LessLessToString()
        {
            var exp = new Maths.Expressions.Programming.And(new Bool(true), new LessThan(new Number(5), new Number(5)));

            Assert.Equal("True && (5 < 5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void LessOrEqualToString()
        {
            var exp = new LessOrEqual(new Number(5), new Number(5));

            Assert.Equal("5 <= 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void LessOrEqualLessOrEqualToString()
        {
            var exp = new Maths.Expressions.Programming.And(new Bool(true), new LessOrEqual(new Number(5), new Number(5)));

            Assert.Equal("True && (5 <= 5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void GreatToString()
        {
            var exp = new GreaterThan(new Number(5), new Number(5));

            Assert.Equal("5 > 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void GreatGreatToString()
        {
            var exp = new Maths.Expressions.Programming.And(new Bool(true), new GreaterThan(new Number(5), new Number(5)));

            Assert.Equal("True && (5 > 5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void GreatOrEqualToString()
        {
            var exp = new GreaterOrEqual(new Number(5), new Number(5));

            Assert.Equal("5 >= 5", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void GreatOrEqualGreatOrEqualToString()
        {
            var exp = new Maths.Expressions.Programming.And(new Bool(true), new GreaterOrEqual(new Number(5), new Number(5)));

            Assert.Equal("True && (5 >= 5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void IfToString()
        {
            var exp = new If(new Equal(new Number(5), new Number(5)), new Number(5));

            Assert.Equal("if(5 == 5, 5)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void IfElseToString()
        {
            var exp = new If(new Equal(new Number(5), new Number(5)), new Number(5), new Number(0));

            Assert.Equal("if(5 == 5, 5, 0)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void ForToString()
        {
            var exp = new For(new Number(5), new Define(Variable.X, new Number(0)), new Equal(new Number(5), new Number(5)), new AddAssign(Variable.X, new Number(1)));

            Assert.Equal("for(5, x := 0, 5 == 5, x += 1)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void WhileToString()
        {
            var exp = new While(new Number(5), new Equal(new Number(5), new Number(5)));

            Assert.Equal("while(5, 5 == 5)", exp.ToString(commoonFormatter));
        }

        #endregion

    }

}
