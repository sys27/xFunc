using System;

namespace xFunc.Library.Maths.Expressions
{

    public class UnaryMinusMathExpression : UnaryMathExpression
    {

        public UnaryMinusMathExpression() : base(null) { }

        public UnaryMinusMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            if (firstMathExpression is BinaryMathExpression)
                return ToString("-({0})");
            else
                return ToString("-{0}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return -firstMathExpression.Calculate(parameters);
        }

        public override IMathExpression Clone()
        {
            return new UnaryMinusMathExpression(firstMathExpression.Clone());
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            return new UnaryMinusMathExpression(firstMathExpression.Clone().Derivative());
        }

    }

}
