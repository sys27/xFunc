using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class ExpTest
    {

        [Fact]
        public void CalculateTest()
        {
            IExpression exp = new Exp(new Number(2));

            Assert.Equal(Math.Exp(2), exp.Calculate());
        }
        
    }

}
