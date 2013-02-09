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
            var un = new UnaryMinusMathExpression(new UnaryMinusMathExpression(new VariableMathExpression('x')));
            var expected = new VariableMathExpression('x');

            SimpleTest(un, expected);
        }

        [TestMethod]
        public void UnaryNumber()
        {
            var un = new UnaryMinusMathExpression(new NumberMathExpression(1));
            var expected = new NumberMathExpression(-1);

            SimpleTest(un, expected);
        }

        [TestMethod]
        public void AddFirstZero()
        {
            var add = new AdditionMathExpression(new NumberMathExpression(0), new VariableMathExpression('x'));
            var expected = new VariableMathExpression('x');

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddSecondZero()
        {
            var add = new AdditionMathExpression(new VariableMathExpression('x'), new NumberMathExpression(0));
            var expected = new VariableMathExpression('x');

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddTwoNumbers()
        {
            var add = new AdditionMathExpression(new NumberMathExpression(3), new NumberMathExpression(2));
            var expected = new NumberMathExpression(5);

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddFirstUnaryMinus()
        {
            var add = new AdditionMathExpression(new UnaryMinusMathExpression(new VariableMathExpression('x')), new NumberMathExpression(2));
            var expected = new SubtractionMathExpression(new NumberMathExpression(2), new VariableMathExpression('x'));

            SimpleTest(add, expected);
        }

        [TestMethod]
        public void AddSecondUnaryMinus()
        {
            var add = new AdditionMathExpression(new NumberMathExpression(2), new UnaryMinusMathExpression(new VariableMathExpression('x')));
            var expected = new SubtractionMathExpression(new NumberMathExpression(2), new VariableMathExpression('x'));

            SimpleTest(add, expected);
        }

    }

}
