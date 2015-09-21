using System;
using xFunc.Maths.Expressions;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class BinaryTest
    {

        [Fact]
        public void EqualsTest1()
        {
            var add1 = new Add(new Number(2), new Number(3));
            var add2 = new Add(new Number(2), new Number(3));

            Assert.Equal(add1, add2);
        }

        [Fact]
        public void EqualsTest2()
        {
            var add = new Add(new Number(2), new Number(3));
            var sub = new Sub(new Number(2), new Number(3));

            Assert.NotEqual<IExpression>(add, sub);
        }

    }

}
