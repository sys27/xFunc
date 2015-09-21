using System;
using xFunc.Maths;
using xFunc.Maths.Expressions.Collections;
using Xunit;

namespace xFunc.Test.Expressions.alAndBitwise
{
    
    public class NotTest
    {

        private Parser parser;
        
        public NotTest()
        {
            parser = new Parser();
        }

        [Fact]
        public void CalculateTest()
        {
            var exp = parser.Parse("~a");
            var parameters = new ParameterCollection();
            parameters.Add("a");

            parameters["a"] = true;
            Assert.False((bool)exp.Calculate(parameters));

            parameters["a"] = false;
            Assert.True((bool)exp.Calculate(parameters));
        }

    }

}
