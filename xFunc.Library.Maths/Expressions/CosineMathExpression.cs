using System;

namespace xFunc.Library.Maths.Expressions
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
            SineMathExpression sine = new SineMathExpression(firstMathExpression.Clone());
            MultiplicationMathExpression multiplication = new MultiplicationMathExpression(sine, firstMathExpression.Clone().Derivative(variable));
            UnaryMinusMathExpression unMinus = new UnaryMinusMathExpression(multiplication);

            return unMinus;
        }

        public override IMathExpression Clone()
        {
            return new CosineMathExpression(firstMathExpression.Clone());
        }

    }

}
