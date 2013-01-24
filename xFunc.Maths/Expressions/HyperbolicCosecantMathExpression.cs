using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{

    public class HyperbolicCosecantMathExpression : UnaryMathExpression
    {

        public HyperbolicCosecantMathExpression()
            : base(null)
        {

        }

        public HyperbolicCosecantMathExpression(IMathExpression firstMathExpression)
            : base(firstMathExpression)
        {

        }

        public override string ToString()
        {
            return ToString("csch({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return MathExtentions.Csch(firstMathExpression.Calculate(parameters));
        }

        public override IMathExpression Clone()
        {
            return new HyperbolicCosecantMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            var coth = new HyperbolicCotangentMathExpression(firstMathExpression.Clone());
            var csch = Clone();
            var mul1 = new MultiplicationMathExpression(coth, csch);
            var mul2 = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), mul1);
            var unMinus = new UnaryMinusMathExpression(mul2);

            return unMinus;
        }

    }

}
