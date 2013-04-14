using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Logics.Expressions;

namespace xFunc.Test.Expressions.Logics
{

    [TestClass]
    public class UndefineTest
    {

        [TestMethod]
        public void CalculateTest()
        {
            LogicParameterCollection parameters = new LogicParameterCollection();
            ILogicExpression def = new Assign(new Variable("a"), new Const(true));
            def.Calculate(parameters);
            Assert.AreEqual(true, parameters["a"]);

            ILogicExpression undef = new Undefine(new Variable("a"));
            undef.Calculate(parameters);
            Assert.IsFalse(parameters.Contains("a"));
        }

    }

}
