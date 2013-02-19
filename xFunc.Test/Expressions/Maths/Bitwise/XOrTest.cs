using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Bitwise;

namespace xFunc.Test.Expressions.Maths.Bitwise
{

    [TestClass]
    public class XOrTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IMathExpression exp = new XOr(new Number(1), new Number(2));

            Assert.AreEqual(3, exp.Calculate(null));
        }

    }

}
