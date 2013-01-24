using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class HyperbolicArsineMathExpression : UnaryMathExpression
    {

        public HyperbolicArsineMathExpression()
            : base(null)
        {

        }

        public HyperbolicArsineMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Asinh(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicArsineMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var sqr = new ExponentiationMathExpression(firstMathExpression.Clone(), new NumberMathExpression(2));
            var add = new AdditionMathExpression(sqr, new NumberMathExpression(1));
            var sqrt = new SqrtMathExpression(add);
            var div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), sqrt);

            return div;
        }

    }

}
