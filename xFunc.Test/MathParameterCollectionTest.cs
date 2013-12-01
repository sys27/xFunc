using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;

namespace xFunc.Test
{

    [TestClass]
    public class MathParameterCollectionTest
    {

        [TestMethod]
        public void GetItemFromCollectionTest()
        {
            var parameters = new ParameterCollection();
            parameters.Add(new Parameter("x", 2.3));

            Assert.AreEqual(2.3, parameters["x"]);
        }

        [TestMethod]
        public void GetItemFromConstsTest()
        {
            var parameters = new ParameterCollection();

            Assert.AreEqual(Math.PI, parameters["π"]);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetItemKeyNotFoundTest()
        {
            var parameters = new ParameterCollection();

            var actual = parameters["hello"];
        }

        [TestMethod]
        public void SetItemFromCollectionTest()
        {
            var parameters = new ParameterCollection();
            parameters["x"] = 2.3;

            Assert.AreEqual(1, parameters.Collection.Count());
            Assert.IsTrue(parameters.ContainsKey("x"));
            Assert.AreEqual(2.3, parameters["x"]);
        }

        [TestMethod]
        public void SetExistItemFromCollectionTest()
        {
            var parameters = new ParameterCollection();
            parameters.Add(new Parameter("x", 2.3));
            parameters["x"] = 3.3;

            Assert.AreEqual(1, parameters.Collection.Count());
            Assert.IsTrue(parameters.ContainsKey("x"));
            Assert.AreEqual(3.3, parameters["x"]);
        }

        [TestMethod]
        [ExpectedException(typeof(ParameterIsReadOnlyException))]
        public void SetReadOnlyItemTest()
        {
            var parameters = new ParameterCollection();
            parameters.Add(new Parameter("hello", 2.5, ParameterType.ReadOnly));
            parameters["hello"] = 5;
        }

        [TestMethod]
        public void OverrideConstsTest()
        {
            var parameters = new ParameterCollection();
            parameters["π"] = 4;

            Assert.AreEqual(1, parameters.Collection.Count());
            Assert.IsTrue(parameters.ContainsKey("π"));
            Assert.AreEqual(4, parameters["π"]);
        }

    }

}
