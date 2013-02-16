using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Bitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class OrTest
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
            IMathExpression exp = parser.Parse("1 or 2");

            Assert.AreEqual(3, exp.Calculate(null));
        }

    }

}
