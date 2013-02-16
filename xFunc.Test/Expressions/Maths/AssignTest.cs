using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFunc.Maths;
using xFunc.Maths.Expressions;

namespace xFunc.Test.Expressions.Maths
{

    [TestClass]
    public class AssignTest
    {

        private MathParser parser;

        [TestInitialize]
        public void TestInit()
        {
            parser = new MathParser();
        }

        [TestMethod]
        public void CalculateTest1()
        {
            IMathExpression exp = parser.Parse("x := 1");
            MathParameterCollection parameters = new MathParameterCollection();

            double answer = exp.Calculate(parameters);

            Assert.AreEqual(1, parameters['x']);
            Assert.AreEqual(double.NaN, answer);
        }

        [TestMethod]
        public void CalculateTest2()
        {
            parser.AngleMeasurement = AngleMeasurement.Radian;
            IMathExpression exp = parser.Parse("x := sin(1)");
            MathParameterCollection parameters = new MathParameterCollection();

            double answer = exp.Calculate(parameters);

            Assert.AreEqual(Math.Sin(1), parameters['x']);
            Assert.AreEqual(double.NaN, answer);
        }

        [TestMethod]
        public void CalculateTest3()
        {
            parser.AngleMeasurement = AngleMeasurement.Radian;
            IMathExpression exp = parser.Parse("x := 4 * (8 + 1)");
            MathParameterCollection parameters = new MathParameterCollection();

            double answer = exp.Calculate(parameters);

            Assert.AreEqual(36, parameters['x']);
            Assert.AreEqual(double.NaN, answer);
        }

    }

}
