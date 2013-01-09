using System;

namespace xFunc.Library.Maths.Expressions
{

    public class PlotMathExpression : UnaryMathExpression
    {

        public PlotMathExpression() : base(null) { }

        public PlotMathExpression(IMathExpression expression) : base(expression) { }

        public override double Calculate(MathParameterCollection parameters)
        {
            throw new NotSupportedException();
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            throw new NotSupportedException();
        }

        public override IMathExpression Clone()
        {
            return new PlotMathExpression(firstMathExpression.Clone());
        }

    }

}
