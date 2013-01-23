using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class HyperbolicSineMathExpression : UnaryMathExpression
    {

        public HyperbolicSineMathExpression()
            : base(null)
        {

        }

        public HyperbolicSineMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("sinh({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Sinh(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicSineMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var cosh = new HyperbolicCosineMathExpression(firstMathExpression.Clone());
            var mul = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), cosh);

            return mul;
        }

    }

}
