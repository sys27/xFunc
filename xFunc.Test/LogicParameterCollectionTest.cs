using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using xFunc.Logics.Expressions;

namespace xFunc.Test
{

    [TestClass]
    public class LogicParameterCollectionTest
    {

        [TestMethod]
        public void AddTest()
        {
            LogicParameterCollection target = new LogicParameterCollection();
            target.Add("a");
            target.Add("b");

            target["a"] = true;
            target["b"] = false;

            Assert.AreEqual(2, target.Bits);
            Assert.IsTrue(target["a"]);
            Assert.IsFalse(target["b"]);
        }

        [TestMethod]
        public void RemoveTest()
        {
            LogicParameterCollection target = new LogicParameterCollection();
            target.Add("a");
            target.Add("b");

            target["a"] = true;
            target["b"] = false;

            target.Remove("a");
            target.Remove("b");

            Assert.AreEqual(0, target.Count);
            Assert.AreEqual(0, target.Bits);
        }

    }

}
