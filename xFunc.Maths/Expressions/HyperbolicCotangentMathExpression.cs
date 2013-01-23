using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class HyperbolicCotangentMathExpression : UnaryMathExpression
    {

        public HyperbolicCotangentMathExpression()
            : base(null)
        {

        }

        public HyperbolicCotangentMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Coth(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicCotangentMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var sinh = new HyperbolicSineMathExpression(firstMathExpression.Clone());
            var inv = new ExponentiationMathExpression(sinh, new NumberMathExpression(2));
            var div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), inv);
            var unMinus = new UnaryMinusMathExpression(div);

            return unMinus;
        }

    }

}
