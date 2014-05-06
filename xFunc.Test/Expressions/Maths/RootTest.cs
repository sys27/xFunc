using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class RootTest
    {
        
        [TestMethod]
        public void CalculateRootTest1()
        {
            IExpression exp = new Root(new Number(8), new Number(3));

            Assert.AreEqual(Math.Pow(8, 1.0 / 3.0), exp.Calculate());
        }

        [TestMethod]
        public void CalculateRootTest2()
        {
            IExpression exp = new Root(new Number(-8), new Number(3));

            Assert.AreEqual(-2.0, exp.Calculate());
        }
                
    }

}
