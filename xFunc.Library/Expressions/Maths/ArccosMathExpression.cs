using System;

namespace xFunc.Library.Expressions.Maths
{

    public class ArccosMathExpression : TrigonometryMathExpression
    {

        public ArccosMathExpression() : base(null) { }

        public ArccosMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("arccos({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Acos(radian) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Acos(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Acos(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            ExponentiationMathExpression involution = new ExponentiationMathExpression(firstMathExpression, new NumberMathExpression(2));
            SubtractionMathExpression sub = new SubtractionMathExpression(new NumberMathExpression(1), involution);
            SqrtMathExpression sqrt = new SqrtMathExpression(sub);
            DivisionMathExpression division = new DivisionMathExpression(firstMathExpression.Derivative(variable), sqrt);
            UnaryMinusMathExpression unMinus = new UnaryMinusMathExpression(division);

            return unMinus;
        }

    }

}
