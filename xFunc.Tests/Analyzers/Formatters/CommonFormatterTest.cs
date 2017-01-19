using System;
using System.Numerics;
using xFunc.Maths.Analyzers.Formatters;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.ComplexNumbers;
using xFunc.Maths.Expressions.Matrices;
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
            var exp = new Mul(new Variable("x"), new Add(new Number(5), new Number(0)));

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
            var exp = new Define(new Variable("x"), new Number(0));

            Assert.Equal("x := 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void DelToStringTest()
        {
            var exp = new Del(new Add(new Add(new Mul(new Number(2), new Variable("x1")), new Pow(new Variable("x2"), new Number(2))), new Pow(new Variable("x3"), new Number(3))));

            Assert.Equal("del(2 * x1 + x2 ^ 2 + x3 ^ 3)", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void DerivativeToStringExpTest()
        {
            var deriv = new Derivative(new Sin(new Variable("x")));

            Assert.Equal("deriv(sin(x))", deriv.ToString(commoonFormatter));
        }

        [Fact]
        public void DerivativeToStringVarTest()
        {
            var deriv = new Derivative(new Sin(new Variable("x")), new Variable("x"));

            Assert.Equal("deriv(sin(x), x)", deriv.ToString(commoonFormatter));
        }

        [Fact]
        public void DerivativeToStringPointTest()
        {
            var deriv = new Derivative(new Sin(new Variable("x")), new Variable("x"), new Number(1));

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
            var exp = new Mul(new Variable("x"), new Div(new Number(5), new Number(0)));

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
            var exp = new Mul(new Variable("x"), new Mod(new Number(5), new Number(0)));

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
            var exp = new Add(new Variable("x"), new Mul(new Number(5), new Number(0)));

            Assert.Equal("x + 5 * 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void MulToStringSubTest()
        {
            var exp = new Sub(new Variable("x"), new Mul(new Number(5), new Number(0)));

            Assert.Equal("x - 5 * 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void MulToStringMulTest()
        {
            var exp = new Mul(new Variable("x"), new Mul(new Number(5), new Number(0)));

            Assert.Equal("x * 5 * 0", exp.ToString(commoonFormatter));
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
            var exp = new Add(new Variable("x"), new Pow(new Number(5), new Number(0)));

            Assert.Equal("x + 5 ^ 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void PowToStringSubTest()
        {
            var exp = new Sub(new Variable("x"), new Pow(new Number(5), new Number(0)));

            Assert.Equal("x - 5 ^ 0", exp.ToString(commoonFormatter));
        }

        [Fact]
        public void PowToStringMulTest()
        {
            var exp = new Mul(new Variable("x"), new Pow(new Number(5), new Number(0)));

            Assert.Equal("x * 5 ^ 0", exp.ToString(commoonFormatter));
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
            var exp = new Simplify(new Sin(new Variable("x")));

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
            var exp = new Sub(new Variable("x"), new Sub(new Number(5), new Number(0)));

            Assert.Equal("x - 5 - 0", exp.ToString(commoonFormatter));
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
            var exp = new Undefine(new Variable("x"));

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
            var exp = new Variable("x");

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
        #endregion

        #region Hyperbolic
        #endregion

        #region Logical and Bitwise
        #endregion

        #region Matrix

        [Fact]
        public void AvgToStringTest()
        {
            var sum = new Avg(new[] { new Number(1), new Number(2) }, 2);

            Assert.Equal("avg(1, 2)", sum.ToString(commoonFormatter));
        }

        [Fact]
        public void AvgToStringTest2()
        {
            var sum = new Avg(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

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
            var sum = new Count(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

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
            var sum = new Max(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

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
            var sum = new Min(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

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
            var sum = new Product(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

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
            var sum = new Stdevp(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

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
            var sum = new Stdev(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

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
            var sum = new Sum(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

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
            var sum = new Varp(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

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
            var sum = new Var(new[] { new Vector(new[] { new Number(1), new Number(2) }) }, 1);

            Assert.Equal("var({1, 2})", sum.ToString(commoonFormatter));
        }

        #endregion

        #region Statistical
        #endregion

        #region Programming
        #endregion

    }

}
