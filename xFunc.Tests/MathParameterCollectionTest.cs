using System;
using System.Collections.Generic;
using System.Linq;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Test
{

    public class MathParameterCollectionTest
    {

        [Fact]
        public void GetItemFromCollectionTest()
        {
            var parameters = new ParameterCollection();
            parameters.Add(new Parameter("x", 2.3));

            Assert.Equal(2.3, parameters["x"]);
        }

        [Fact]
        public void GetItemFromConstsTest()
        {
            var parameters = new ParameterCollection();

            Assert.Equal(Math.PI, parameters["π"]);
        }

        [Fact]
        public void GetItemKeyNotFoundTest()
        {
            var parameters = new ParameterCollection();

            Assert.Throws<KeyNotFoundException>(() => parameters["hello"]);
        }

        [Fact]
        public void SetItemFromCollectionTest()
        {
            var parameters = new ParameterCollection();
            parameters["x"] = 2.3;

            Assert.Equal(1, parameters.Collection.Count());
            Assert.True(parameters.ContainsKey("x"));
            Assert.Equal(2.3, parameters["x"]);
        }

        [Fact]
        public void SetExistItemFromCollectionTest()
        {
            var parameters = new ParameterCollection();
            parameters.Add(new Parameter("x", 2.3));
            parameters["x"] = 3.3;

            Assert.Equal(1, parameters.Collection.Count());
            Assert.True(parameters.ContainsKey("x"));
            Assert.Equal(3.3, parameters["x"]);
        }

        [Fact]
        public void SetReadOnlyItemTest()
        {
            var parameters = new ParameterCollection();
            parameters.Add(new Parameter("hello", 2.5, ParameterType.ReadOnly));

            Assert.Throws<ParameterIsReadOnlyException>(() => parameters["hello"] = 5);
        }

        [Fact]
        public void OverrideConstsTest()
        {
            var parameters = new ParameterCollection();
            parameters["π"] = 4;

            Assert.Equal(1, parameters.Collection.Count());
            Assert.True(parameters.ContainsKey("π"));
            Assert.Equal(4.0, parameters["π"]);
        }

        [Fact]
        public void OverrideRemoveTest()
        {
            var parameters = new ParameterCollection();
            parameters.Add(new Parameter("a", 1));
            parameters["a"] = 2;
            parameters.Remove("a");

            Assert.Equal(0, parameters.Collection.Count());
        }

    }

}
