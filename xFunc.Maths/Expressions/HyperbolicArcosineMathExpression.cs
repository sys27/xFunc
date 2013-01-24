using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class HyperbolicArcosineMathExpression : UnaryMathExpression
    {

        public HyperbolicArcosineMathExpression()
            : base(null)
        {

        }

        public HyperbolicArcosineMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("arcosh({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Acosh(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicArcosineMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var sqr = new ExponentiationMathExpression(firstMathExpression.Clone(), new NumberMathExpression(2));
            var sub = new SubtractionMathExpression(sqr, new NumberMathExpression(1));
            var sqrt = new SqrtMathExpression(sub);
            var div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), sqrt);

            return div;
        }

    }

}
