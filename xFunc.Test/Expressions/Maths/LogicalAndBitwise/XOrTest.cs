using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Test.Expressions.alAndBitwise
{

    [TestClass]
    public class XOrTest
    {

        private Parser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new Parser();
        }

        [TestMethod]
        public void CalculateTest()
        {
            var exp = parser.Parse("a xor b");
            var parameters = new ParameterCollection();
            parameters.Add("a");
            parameters.Add("b");

            parameters["a"] = true;
            parameters["b"] = true;
            Assert.IsFalse((bool)exp.Calculate(parameters));

            parameters["a"] = true;
            parameters["b"] = false;
            Assert.IsTrue((bool)exp.Calculate(parameters));

            parameters["a"] = false;
            parameters["b"] = true;
            Assert.IsTrue((bool)exp.Calculate(parameters));

            parameters["a"] = false;
            parameters["b"] = false;
            Assert.IsFalse((bool)exp.Calculate(parameters));
        }

    }

}
