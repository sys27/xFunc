using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Programming;
using Xunit;

namespace xFunc.Test.Expressions.Maths.Programming
{
    
    public class ForTest
    {

        [Fact]
        public void CalculateForTest()
        {
            var parameters = new ExpressionParameters();

            var init = new Define(new Variable("i"), new Number(0));
            var cond = new LessThan(new Variable("i"), new Number(10));
            var iter = new Define(new Variable("i"), new Add(new Variable("i"), new Number(1))); 

            var @for = new For(new Variable("i"), init, cond, iter);
            @for.Calculate(parameters);

            Assert.Equal(10.0, parameters.Parameters["i"]);
        }

    }

}
