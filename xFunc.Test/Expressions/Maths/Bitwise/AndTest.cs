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
        
        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new And(new Number(1), new Number(3));

            Assert.AreEqual(1, exp.Calculate(null));
        }

    }

}
