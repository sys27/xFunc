using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class WhileTest
    {

        [Fact]
        public void CalculateWhileTest()
        {
            var parameters = new ExpressionParameters();
            parameters.Parameters.Add(new Parameter("x", 0));

            var body = new Define(new Variable("x"), new Add(new Variable("x"), new Number(2)));
            var cond = new LessThan(new Variable("x"), new Number(10));

            var @while = new While(body, cond);
            @while.Calculate(parameters);

            Assert.Equal(10.0, parameters.Parameters["x"]);
        }

    }

}
