using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class HyperbolicCosineMathExpression : UnaryMathExpression
    {

        public HyperbolicCosineMathExpression()
            : base(null)
        {

        }

        public HyperbolicCosineMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("cosh({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Cosh(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicCosineMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var sinh = new HyperbolicSineMathExpression(firstMathExpression.Clone());
            var mul = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), sinh);

            return mul;
        }

    }

}
