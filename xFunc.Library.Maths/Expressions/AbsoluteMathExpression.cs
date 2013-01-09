using System;

namespace xFunc.Library.Maths.Expressions
{

    public class AbsoluteMathExpression : UnaryMathExpression
    {

        public AbsoluteMathExpression() : base(null) { }

        public AbsoluteMathExpression(IMathExpression expression) : base(expression) { }

        public override string ToString()
        {
            return ToString("abs({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Abs(firstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Clone(), this.Clone());
            MultiplicationMathExpression mul = new MultiplicationMathExpression(firstMathExpression.Clone().Derivative(variable), div);

            return mul;
        }

        public override IMathExpression Clone()
        {
            return new AbsoluteMathExpression(firstMathExpression.Clone());
        }

    }

}
