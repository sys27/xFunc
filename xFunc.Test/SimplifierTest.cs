using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Trigonometric;
using xFunc.Maths.Expressions.Hyperbolic;

namespace xFunc.Test
{

    [TestClass]
    public class SimplifierTest
    {

        ISimplifier simplifier;

        public SimplifierTest()
        {
            simplifier = new Simplifier();
        }

        private void SimpleTest(IExpression exp, IExpression expected)
        {
            var simple = simplifier.Simplify(exp);

            Assert.AreEqual(expected, simple);
        }

        [TestMethod]
        public void DoubleUnary()
        {
            var un = new UnaryMinus(new UnaryMinus(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(un, expected);
        }

        [TestMethod]
        public void UnaryNumber()
        {
            var un = new UnaryMinus(new Number(1));
            var expected = new Number(-1);

            SimpleTest(un, expected);
        }

        #region Add

        [TestMethod]
        public void AddFirstZero()
        {
            var add = new Add(new Number(0), new Variable("x"));
            var expected = new Variable("x");

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddSecondZero()
        {
            var add = new Add(new Variable("x"), new Number(0));
            var expected = new Variable("x");

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddTwoNumbers()
        {
            var add = new Add(new Number(3), new Number(2));
            var expected = new Number(5);

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddFirstUnaryMinus()
        {
            var add = new Add(new UnaryMinus(new Variable("x")), new Number(2));
            var expected = new Sub(new Number(2), new Variable("x"));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddSecondUnaryMinus()
        {
            var add = new Add(new Number(2), new UnaryMinus(new Variable("x")));
            var expected = new Sub(new Number(2), new Variable("x"));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiffNumAdd_NumAddVar_()
        {
            // 2 + (2 + x)
            var add = new Add(new Number(2), new Add(new Number(2), new Variable("x")));
            var expected = new Add(new Variable("x"), new Number(4));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiffNumAdd_VarAddNum_()
        {
            // 2 + (x + 2)
            var add = new Add(new Number(2), new Add(new Variable("x"), new Number(2)));
            var expected = new Add(new Variable("x"), new Number(4));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiff_NumAddVar_AddNum()
        {
            // (2 + x) + 2
            var add = new Add(new Add(new Number(2), new Variable("x")), new Number(2));
            var expected = new Add(new Variable("x"), new Number(4));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiff_VarAddNum_AddNum()
        {
            // (x + 2) + 2
            var add = new Add(new Add(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Add(new Variable("x"), new Number(4));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiffNum_NumSubVar_()
        {
            // 2 + (2 - x)
            var add = new Add(new Number(2), new Sub(new Number(2), new Variable("x")));
            var expected = new Sub(new Number(4), new Variable("x"));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiffNum_VarSubNum_()
        {
            // 2 + (x - 2)
            var add = new Add(new Number(2), new Sub(new Variable("x"), new Number(2)));
            var expected = new Variable("x");

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiff_NumSubVar_AddNum()
        {
            // (2 - x) + 2
            var add = new Add(new Sub(new Number(2), new Variable("x")), new Number(2));
            var expected = new Sub(new Number(4), new Variable("x"));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiff_VarSubNum_AddNum()
        {
            // (x - 2) + 2
            var add = new Add(new Sub(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Variable("x");

            SimpleTest(add, expected);
        }

        #endregion

        #region Sub

        [TestMethod]
        public void SubFirstZero()
        {
            var sub = new Sub(new Number(0), new Variable("x"));
            var expected = new UnaryMinus(new Variable("x"));

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubSecondZero()
        {
            var sub = new Sub(new Variable("x"), new Number(0));
            var expected = new Variable("x");

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubTwoNumbers()
        {
            var sub = new Sub(new Number(3), new Number(2));
            var expected = new Number(1);

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubSecondUnaryMinus()
        {
            var sub = new Sub(new Number(2), new UnaryMinus(new Variable("x")));
            var expected = new Add(new Number(2), new Variable("x"));

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubDiff_NumAddVar_SubNum()
        {
            // (2 + x) - 2
            var sub = new Sub(new Add(new Number(2), new Variable("x")), new Number(2));
            var expected = new Variable("x");

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubDiff_VarAddNum_SubNum()
        {
            // (x + 2) - 2
            var sub = new Sub(new Add(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Variable("x");

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubDiffNumSub_NumAddVar_()
        {
            // 2 - (2 + x)
            var sub = new Sub(new Number(2), new Add(new Number(2), new Variable("x")));
            var expected = new UnaryMinus(new Variable("x"));

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubDiffNumSub_VarAddNum_()
        {
            // 2 - (x + 2)
            var sub = new Sub(new Number(2), new Add(new Variable("x"), new Number(2)));
            var expected = new UnaryMinus(new Variable("x"));

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubDiff_NumSubVar_SubNum()
        {
            var sub = new Sub(new Sub(new Number(2), new Variable("x")), new Number(2));
            var expected = new UnaryMinus(new Variable("x"));

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubDiff_VarSubNum_SubNum()
        {
            var sub = new Sub(new Sub(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Sub(new Variable("x"), new Number(4));

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubDiffNumSub_NumSubVar_()
        {
            var sub = new Sub(new Number(2), new Sub(new Number(2), new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(sub, expected);
        }

        [TestMethod]
        public void SubDiffNumSub_VarSubNum_()
        {
            var sub = new Sub(new Number(2), new Sub(new Variable("x"), new Number(2)));
            var expected = new Sub(new Number(4), new Variable("x"));

            SimpleTest(sub, expected);
        }

        #endregion

        #region Mul

        [TestMethod]
        public void MulByZero()
        {
            var mul = new Mul(new Variable("x"), new Number(0));
            var expected = new Number(0);

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulFirstOne()
        {
            var mul = new Mul(new Number(1), new Variable("x"));
            var expected = new Variable("x");

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulSecondOne()
        {
            var mul = new Mul(new Variable("x"), new Number(1));
            var expected = new Variable("x");

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulTwoNumbers()
        {
            var mul = new Mul(new Number(2), new Number(3));
            var expected = new Number(6);

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulDiffNumMul_NumMulVar_()
        {
            var mul = new Mul(new Number(2), new Mul(new Number(2), new Variable("x")));
            var expected = new Mul(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulDiffNumMul_VarMulNum_()
        {
            var mul = new Mul(new Number(2), new Mul(new Variable("x"), new Number(2)));
            var expected = new Mul(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulDiff_NumMulVar_MulNum()
        {
            var mul = new Mul(new Mul(new Number(2), new Variable("x")), new Number(2));
            var expected = new Mul(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulDiff_VarMulNum_MulNum()
        {
            var mul = new Mul(new Mul(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Mul(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulDiffNumMul_NumDivVar_()
        {
            // 2 * (2 / x)
            var mul = new Mul(new Number(2), new Div(new Number(2), new Variable("x")));
            var expected = new Div(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulDiffNumMul_VarDivNum_()
        {
            // 2 * (x / 2)
            var mul = new Mul(new Number(2), new Div(new Variable("x"), new Number(2)));
            var expected = new Variable("x");

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulDiffl_NumDivVar_MulNum()
        {
            // (2 / x) * 2
            var mul = new Mul(new Div(new Number(2), new Variable("x")), new Number(2));
            var expected = new Div(new Number(4), new Variable("x"));

            SimpleTest(mul, expected);
        }

        [TestMethod]
        public void MulDiff_VarDivNum_MulNum()
        {
            // (x / 2) * 2
            var mul = new Mul(new Div(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Variable("x");

            SimpleTest(mul, expected);
        }

        #endregion

        #region Div

        [TestMethod]
        public void DivZero()
        {
            var div = new Div(new Number(0), new Variable("x"));
            var expected = new Number(0);

            SimpleTest(div, expected);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivByZero()
        {
            var div = new Div(new Variable("x"), new Number(0));

            SimpleTest(div, null);
        }

        [TestMethod]
        public void ZeroDivByZero()
        {
            var div = new Div(new Number(0), new Number(0));
            var expected = new Number(double.NaN);

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivByOne()
        {
            var div = new Div(new Variable("x"), new Number(1));
            var expected = new Variable("x");

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivTwoNumbers()
        {
            var div = new Div(new Number(8), new Number(2));
            var expected = new Number(4);

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivDiff_NumMulVar_DivNum()
        {
            // (2 * x) / 4
            var div = new Div(new Mul(new Number(2), new Variable("x")), new Number(4));
            var expected = new Div(new Variable("x"), new Number(2));

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivDiff_VarMulNum_DivNum()
        {
            // (x * 2) / 4
            var div = new Div(new Mul(new Variable("x"), new Number(2)), new Number(4));
            var expected = new Div(new Variable("x"), new Number(2));

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivDiffNumDiv_NumMulVar_()
        {
            // 2 / (2 * x)
            var div = new Div(new Number(2), new Mul(new Number(2), new Variable("x")));
            var expected = new Div(new Number(1), new Variable("x"));

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivDiffNumDiv_VarMulNum_()
        {
            // 2 / (2 * x)
            var div = new Div(new Number(2), new Mul(new Variable("x"), new Number(2)));
            var expected = new Div(new Number(1), new Variable("x"));

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivDiff_NumDivVar_DivNum()
        {
            // (2 / x) / 2
            var div = new Div(new Div(new Number(2), new Variable("x")), new Number(2));
            var expected = new Div(new Number(1), new Variable("x"));

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivDiff_VarDivNum_DivNum()
        {
            // (x / 2) / 2
            var div = new Div(new Div(new Variable("x"), new Number(2)), new Number(2));
            var expected = new Div(new Variable("x"), new Number(4));

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivDiffNumDiv_NumDivVar_()
        {
            // 2 / (2 / x)
            var div = new Div(new Number(2), new Div(new Number(2), new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(div, expected);
        }

        [TestMethod]
        public void DivDiffNumDiv_VarDivNum_()
        {
            // 2 / (x / 2)
            var div = new Div(new Number(2), new Div(new Variable("x"), new Number(2)));
            var expected = new Div(new Number(4), new Variable("x"));

            SimpleTest(div, expected);
        }

        #endregion

        [TestMethod]
        public void PowerZero()
        {
            var pow = new Pow(new Variable("x"), new Number(0));
            var expected = new Number(1);

            SimpleTest(pow, expected);
        }

        [TestMethod]
        public void PowerOne()
        {
            var pow = new Pow(new Variable("x"), new Number(1));
            var expected = new Variable("x");

            SimpleTest(pow, expected);
        }

        [TestMethod]
        public void RootOne()
        {
            var root = new Root(new Variable("x"), new Number(1));
            var expected = new Variable("x");

            SimpleTest(root, expected);
        }

        [TestMethod]
        public void Log()
        {
            var log = new Log(new Variable("x"), new Variable("x"));
            var expected = new Number(1);

            SimpleTest(log, expected);
        }

        [TestMethod]
        public void Ln()
        {
            var ln = new Ln(new Variable("e"));
            var expected = new Number(1);

            SimpleTest(ln, expected);
        }

        [TestMethod]
        public void Lg()
        {
            var log = new Lg(new Number(10));
            var expected = new Number(1);

            SimpleTest(log, expected);
        }

        #region Trigonometric

        [TestMethod]
        public void ArcsinSin()
        {
            var exp = new Arcsin(new Sin(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArccosCos()
        {
            var exp = new Arccos(new Cos(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArctanTan()
        {
            var exp = new Arctan(new Tan(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArccotCot()
        {
            var exp = new Arccot(new Cot(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArcsecSec()
        {
            var exp = new Arcsec(new Sec(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArccscCsc()
        {
            var exp = new Arccsc(new Csc(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void SinArcsin()
        {
            var exp = new Sin(new Arcsin(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void CosArccos()
        {
            var exp = new Cos(new Arccos(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void TanArctan()
        {
            var exp = new Tan(new Arctan(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void CotArccot()
        {
            var exp = new Cot(new Arccot(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void SecArcsec()
        {
            var exp = new Sec(new Arcsec(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void CscArccsc()
        {
            var exp = new Csc(new Arccsc(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        #endregion

        #region Hyperbolic

        [TestMethod]
        public void ArsinhSinh()
        {
            var exp = new Arsinh(new Sinh(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArcoshCosh()
        {
            var exp = new Arcosh(new Cosh(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArtanhTanh()
        {
            var exp = new Artanh(new Tanh(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArcothCoth()
        {
            var exp = new Arcoth(new Coth(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArsechSech()
        {
            var exp = new Arsech(new Sech(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void ArcschCsch()
        {
            var exp = new Arcsch(new Csch(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void SinhArsinh()
        {
            var exp = new Sinh(new Arsinh(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void CoshArcosh()
        {
            var exp = new Cosh(new Arcosh(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void TanhArtanh()
        {
            var exp = new Tanh(new Artanh(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void CothArcoth()
        {
            var exp = new Coth(new Arcoth(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void SechArsech()
        {
            var exp = new Sech(new Arsech(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        [TestMethod]
        public void CschArcsch()
        {
            var exp = new Csch(new Arcsch(new Variable("x")));
            var expected = new Variable("x");

            SimpleTest(exp, expected);
        }

        #endregion

    }

}
