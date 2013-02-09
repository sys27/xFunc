using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test
{

    [TestClass]
    public class MathParserSimplificationTest
    {

        private void SimpleTest(IMathExpression exp, IMathExpression expected)
        {
            var simple = MathParser.SimplifyExpressions(exp);

            Assert.AreEqual(expected, simple);
        }

        [TestMethod]
        public void DoubleUnary()
        {
            var un = new UnaryMinus(new UnaryMinus(new Variable('x')));
            var expected = new Variable('x');

            SimpleTest(un, expected);
        }

        [TestMethod]
        public void UnaryNumber()
        {
            var un = new UnaryMinus(new Number(1));
            var expected = new Number(-1);

            SimpleTest(un, expected);
        }

        [TestMethod]
        public void AddFirstZero()
        {
            var add = new Addition(new Number(0), new Variable('x'));
            var expected = new Variable('x');

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddSecondZero()
        {
            var add = new Addition(new Variable('x'), new Number(0));
            var expected = new Variable('x');

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddTwoNumbers()
        {
            var add = new Addition(new Number(3), new Number(2));
            var expected = new Number(5);

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddFirstUnaryMinus()
        {
            var add = new Addition(new UnaryMinus(new Variable('x')), new Number(2));
            var expected = new Subtraction(new Number(2), new Variable('x'));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddSecondUnaryMinus()
        {
            var add = new Addition(new Number(2), new UnaryMinus(new Variable('x')));
            var expected = new Subtraction(new Number(2), new Variable('x'));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiffNumAdd_NumAddVar_()
        {
            var add = new Addition(new Number(2), new Addition(new Number(2), new Variable('x')));
            var expected = new Addition(new Variable('x'), new Number(4));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiffNumAdd_VarAddNum_()
        {
            var add = new Addition(new Number(2), new Addition(new Variable('x'), new Number(2)));
            var expected = new Addition(new Variable('x'), new Number(4));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiff_NumAddVar_AddNum()
        {
            var add = new Addition(new Addition(new Number(2), new Variable('x')), new Number(2));
            var expected = new Addition(new Variable('x'), new Number(4));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddDiff_VarAddNum_AddNum()
        {
            var add = new Addition(new Addition(new Variable('x'), new Number(2)), new Number(2));
            var expected = new Addition(new Variable('x'), new Number(4));

            SimpleTest(add, expected);
        }

    }

}
