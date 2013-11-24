using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class UndefineTest
    {

        [TestMethod]
        public void UndefVarTest()
        {
            var parameters = new MathParameterCollection { { "a", 1 } };

            var undef = new Undefine(new Variable("a"));
            undef.Calculate(parameters);
            Assert.IsFalse(parameters.ContainsKey("a"));
        }

        [TestMethod]
        public void UndefFuncTest()
        {
            var key1 = new UserFunction("f", 0);
            var key2 = new UserFunction("f", 1);

            var functions = new MathFunctionCollection { { key1, new Number(1) }, { key2, new Number(2) } };

            var undef = new Undefine(key1);
            undef.Calculate(functions);
            Assert.IsFalse(functions.ContainsKey(key1));
            Assert.IsTrue(functions.ContainsKey(key2));
        }

        [TestMethod]
        public void UndefConstTest()
        {
            var parameters = new MathParameterCollection();

            var undef = new Undefine(new Variable("π"));
            undef.Calculate(parameters);
        }

    }

}
