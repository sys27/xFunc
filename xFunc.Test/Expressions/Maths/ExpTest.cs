using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class ExpTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            IExpression exp = new Exp(new Number(2));

            Assert.AreEqual(Math.Exp(2), exp.Calculate());
        }
        
    }

}
