using System;

namespace xFunc.Library.Maths.Expressions
{
    
    public class SqrtMathExpression : UnaryMathExpression
    {

        public SqrtMathExpression() : base(null) { }

        public SqrtMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("sqrt({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Sqrt(FirstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            MultiplicationMathExpression mul = new MultiplicationMathExpression(new NumberMathExpression(2), this);
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Derivative(variable), mul);

            return div;
        }

    }

}
