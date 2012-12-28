using System;

namespace xFunc.Library.Maths.Expressions
{

    public class ArcsinMathExpression : TrigonometryMathExpression
    {

        public ArcsinMathExpression() : base(null) { }

        public ArcsinMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("arcsin({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Asin(radian) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Asin(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Asin(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            ExponentiationMathExpression involution = new ExponentiationMathExpression(firstMathExpression, new NumberMathExpression(2));
            SubtractionMathExpression sub = new SubtractionMathExpression(new NumberMathExpression(1), involution);
            SqrtMathExpression sqrt = new SqrtMathExpression(sub);
            DivisionMathExpression division = new DivisionMathExpression(firstMathExpression.Derivative(variable), sqrt);

            return division;
        }

    }

}
