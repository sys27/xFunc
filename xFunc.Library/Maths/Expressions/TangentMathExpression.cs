using System;

namespace xFunc.Library.Maths.Expressions
{
    
    public class TangentMathExpression : TrigonometryMathExpression
    {

        public TangentMathExpression() : base(null) { }

        public TangentMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("tan({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 180;

            return Math.Tan(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Tan(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 200;

            return Math.Tan(radian);
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            CosineMathExpression cos = new CosineMathExpression(firstMathExpression);
            ExponentiationMathExpression inv = new ExponentiationMathExpression(cos, new NumberMathExpression(2));
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Derivative(variable), inv);

            return div;
        }

    }

}
