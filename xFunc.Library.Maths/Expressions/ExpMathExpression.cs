using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Maths.Expressions
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
            MultiplicationMathExpression mul = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), this.Clone());

            return mul;
        }

        public override IMathExpression Clone()
        {
            return new ExpMathExpression(firstMathExpression.Clone());
        }

    }

}
