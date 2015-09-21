using System;
using xFunc.Maths.Expressions;
using System.Collections.Generic;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{

    public class UserFunctionTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var functions = new FunctionCollection();
            functions.Add(new UserFunction("f", new IExpression[] { new Variable("x") }, 1), new Ln(new Variable("x")));

            var func = new UserFunction("f", new IExpression[] { new Number(1) }, 1);
            Assert.Equal(Math.Log(1), func.Calculate(functions));
        }

        [Fact]
        public void CalculateTest2()
        {
            var functions = new FunctionCollection();

            var func = new UserFunction("f", new IExpression[] { new Number(1) }, 1);

            Assert.Throws<KeyNotFoundException>(() => func.Calculate(functions));
        }

    }

}
