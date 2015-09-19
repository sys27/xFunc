using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Test.Expressions.alAndBitwise
{

    [TestClass]
    public class NotTest
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
            var exp = parser.Parse("~a");
            var parameters = new ParameterCollection();
            parameters.Add("a");

            parameters["a"] = true;
            Assert.IsFalse((bool)exp.Calculate(parameters));

            parameters["a"] = false;
            Assert.IsTrue((bool)exp.Calculate(parameters));
        }

    }

}
