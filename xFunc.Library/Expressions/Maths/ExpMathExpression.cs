using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Expressions.Maths
{

    public class ExpMathExpression : UnaryMathExpression
    {

        public ExpMathExpression() : base(null) { }

        public ExpMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("exp({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Exp(firstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            MultiplicationMathExpression mul = new MultiplicationMathExpression(firstMathExpression.Derivative(variable), this);

            return mul;
        }

    }

}
