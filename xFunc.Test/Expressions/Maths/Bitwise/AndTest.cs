using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Bitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class AndTest
    {

        private MathParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new MathParser();
        }

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = parser.Parse("1 and 3");

            Assert.AreEqual(1, exp.Calculate(null));
        }

    }

}
