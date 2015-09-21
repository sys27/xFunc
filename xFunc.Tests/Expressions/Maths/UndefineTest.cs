using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{

    public class UndefineTest
    {

        [Fact]
        public void UndefVarTest()
        {
            var parameters = new ParameterCollection { { "a", 1 } };

            var undef = new Undefine(new Variable("a"));
            undef.Calculate(parameters);
            Assert.False(parameters.ContainsKey("a"));
        }

        [Fact]
        public void UndefFuncTest()
        {
            var key1 = new UserFunction("f", 0);
            var key2 = new UserFunction("f", 1);

            var functions = new FunctionCollection { { key1, new Number(1) }, { key2, new Number(2) } };

            var undef = new Undefine(key1);
            undef.Calculate(functions);
            Assert.False(functions.ContainsKey(key1));
            Assert.True(functions.ContainsKey(key2));
        }

        [Fact]
        public void UndefConstTest()
        {
            var parameters = new ParameterCollection();

            var undef = new Undefine(new Variable("π"));

            Assert.Throws<ArgumentException>(() => undef.Calculate(parameters));
        }

    }

}
