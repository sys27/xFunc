using System;

namespace xFunc.Library.Expressions.Maths
{

    public class SineMathExpression : TrigonometryMathExpression
    {

        public SineMathExpression() : base(null) { }

        public SineMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("sin({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 180;

            return Math.Sin(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Sin(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 200;

            return Math.Sin(radian);
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            CosineMathExpression cos = new CosineMathExpression(firstMathExpression);
            MultiplicationMathExpression mul = new MultiplicationMathExpression(cos, firstMathExpression.Derivative(variable));

            return mul;
        }

    }

}
