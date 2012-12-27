using System;

namespace xFunc.Library.Expressions.Maths
{

    public class ArctanMathExpression : TrigonometryMathExpression
    {

        public ArctanMathExpression() : base(null) { }

        public ArctanMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("arctan({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Atan(radian) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return Math.Atan(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return Math.Atan(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            ExponentiationMathExpression involution = new ExponentiationMathExpression(firstMathExpression, new NumberMathExpression(2));
            AdditionMathExpression add = new AdditionMathExpression(new NumberMathExpression(1), involution);
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Derivative(variable), add);

            return div;
        }

    }

}
