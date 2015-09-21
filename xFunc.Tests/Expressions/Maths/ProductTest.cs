using System;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Test.Expressions.Maths
{
    
    public class ProductTest
    {

        [Fact]
        public void CalculateTest1()
        {
            var sum = new Product(new Variable("i"), new Number(8));

            Assert.Equal(40320.0, sum.Calculate());
        }

        [Fact]
        public void CalculateTest2()
        {
            var sum = new Product(new Variable("i"), new Number(4), new Number(8));

            Assert.Equal(6720.0, sum.Calculate());
        }

        [Fact]
        public void CalculateTest3()
        {
            var sum = new Product(new Variable("i"), new Number(4), new Number(8), new Number(2));

            Assert.Equal(192.0, sum.Calculate());
        }

        [Fact]
        public void CalculateTest4()
        {
            var sum = new Product(new Variable("k"), new Number(4), new Number(8), new Number(2), new Variable("k"));

            Assert.Equal(192.0, sum.Calculate());
        }

        [Fact]
        public void CalculateTest5()
        {
            var sum = new Product(new Pow(new Variable("a"), new Variable("i")), new Number(4));

            Assert.Equal(1024.0, sum.Calculate(new ParameterCollection() { new Parameter("a", 2) }));
        }

        [Fact]
        public void CalculateTest6()
        {
            var sum = new Product(new Pow(new Variable("a"), new Variable("i")), new Number(2), new Number(5));

            Assert.Equal(16384.0, sum.Calculate(new ParameterCollection() { new Parameter("a", 2) }));
        }

        [Fact]
        public void CalculateTest7()
        {
            var sum = new Product(new Pow(new Variable("a"), new Variable("i")), new Number(4), new Number(8), new Number(2));

            Assert.Equal(262144.0, sum.Calculate(new ParameterCollection() { new Parameter("a", 2) }));
        }

        [Fact]
        public void CalculateTest8()
        {
            var sum = new Product(new Pow(new Variable("a"), new Variable("k")), new Number(4), new Number(8), new Number(2), new Variable("k"));

            Assert.Equal(262144.0, sum.Calculate(new ParameterCollection() { new Parameter("a", 2) }));
        }

    }

}
