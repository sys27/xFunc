using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{

    public class LogTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Log(new Number(10), new Number(2));

            Assert.Equal(Math.Log(10, 2), exp.Calculate());
        }

    }

}
