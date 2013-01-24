using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class HyperbolicSecantMathExpression : UnaryMathExpression
    {

        public HyperbolicSecantMathExpression()
            : base(null)
        {

        }

        public HyperbolicSecantMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("sech({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Sech(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicSecantMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var tanh = new HyperbolicTangentMathExpression(firstMathExpression.Clone());
            var sech = Clone();
            var mul1 = new MultiplicationMathExpression(tanh, sech);
            var mul2 = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), mul1);
            var unMinus = new UnaryMinusMathExpression(mul2);

            return unMinus;
        }

    }

}
