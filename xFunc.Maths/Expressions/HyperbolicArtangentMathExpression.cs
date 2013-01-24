using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class HyperbolicArtangentMathExpression : UnaryMathExpression
    {

        public HyperbolicArtangentMathExpression()
            : base(null)
        {

        }

        public HyperbolicArtangentMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("artanh({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Atanh(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicArtangentMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var sqr = new ExponentiationMathExpression(firstMathExpression.Clone(), new NumberMathExpression(2));
            var sub = new SubtractionMathExpression(new NumberMathExpression(1), sqr);
            var div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), sub);

            return div;
        }

    }

}
