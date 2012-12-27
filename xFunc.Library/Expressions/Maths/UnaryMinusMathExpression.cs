using System;

namespace xFunc.Library.Expressions.Maths
{
    
    public class UnaryMinusMathExpression : UnaryMathExpression
    {

        public UnaryMinusMathExpression() : base(null) { }

        public UnaryMinusMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("-{0}");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return -firstMathExpression.Calculate(parameters);
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            return new UnaryMinusMathExpression(firstMathExpression.Derivative());
        }

    }

}
