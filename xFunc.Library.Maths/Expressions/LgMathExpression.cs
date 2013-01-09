using System;

namespace xFunc.Library.Maths.Expressions
{

    public class LgMathExpression : UnaryMathExpression
    {

        public LgMathExpression() : base(null) { }

        public LgMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("lg({0})");
        }

        public override double Calculate(MathParameterCollection parameters)
        {
            return Math.Log10(FirstMathExpression.Calculate(parameters));
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            LnMathExpression ln = new LnMathExpression(new NumberMathExpression(10));
            MultiplicationMathExpression mul1 = new MultiplicationMathExpression(firstMathExpression.Clone(), ln);
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), mul1);

            return div;
        }

        public override IMathExpression Clone()
        {
            return new LgMathExpression(firstMathExpression.Clone());
        }

    }

}
