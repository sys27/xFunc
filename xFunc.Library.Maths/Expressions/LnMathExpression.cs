using System;

namespace xFunc.Library.Maths.Expressions
{

    public class LnMathExpression : UnaryMathExpression
    {

        public LnMathExpression() : base(null) { }

        public LnMathExpression(IMathExpression FirstMathExpression) : base(FirstMathExpression) { }

        public override string ToString()
        {
            return ToString("ln({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Log(FirstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Derivative(variable), firstMathExpression);

            return div;
        }

    }

}
