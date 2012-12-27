using System;

namespace xFunc.Library.Expressions.Maths
{
    
    public class CosineMathExpression : TrigonometryMathExpression
    {

        public CosineMathExpression() : base(null) { }

        public CosineMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("cos({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 180;

            return Math.Cos(radian);
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Cos(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters) * Math.PI / 200;

            return Math.Cos(radian);
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            SineMathExpression sine = new SineMathExpression(firstMathExpression);
            MultiplicationMathExpression multiplication = new MultiplicationMathExpression(sine, firstMathExpression.Derivative(variable));
            UnaryMinusMathExpression unMinus = new UnaryMinusMathExpression(multiplication);

            return unMinus;
        }

    }

}
