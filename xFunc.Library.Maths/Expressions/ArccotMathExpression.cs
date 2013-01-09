using System;

namespace xFunc.Library.Maths.Expressions
{

    public class ArccotMathExpression : TrigonometryMathExpression
    {

        public ArccotMathExpression() : base(null) { }

        public ArccotMathExpression(IMathExpression firstMathExpression) : base(firstMathExpression) { }

        public override string ToString()
        {
            return ToString("arccot({0})");
        }

        public override double CalculateDergee(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return MathExtentions.Acot(radian) / Math.PI * 180;
        }

        public override double CalculateRadian(MathParameterCollection parameters)
        {
            return MathExtentions.Acot(firstMathExpression.Calculate(parameters));
        }

        public override double CalculateGradian(MathParameterCollection parameters)
        {
            var radian = firstMathExpression.Calculate(parameters);

            return MathExtentions.Acot(radian) / Math.PI * 200;
        }

        protected override IMathExpression _Derivative(VariableMathExpression variable)
        {
            ExponentiationMathExpression involution = new ExponentiationMathExpression(firstMathExpression.Clone(), new NumberMathExpression(2));
            AdditionMathExpression add = new AdditionMathExpression(new NumberMathExpression(1), involution);
            DivisionMathExpression div = new DivisionMathExpression(firstMathExpression.Clone().Derivative(variable), add);
            UnaryMinusMathExpression unMinus = new UnaryMinusMathExpression(div);

            return unMinus;
        }

        public override IMathExpression Clone()
        {
            return new ArccotMathExpression(firstMathExpression.Clone());
        }

    }

}
