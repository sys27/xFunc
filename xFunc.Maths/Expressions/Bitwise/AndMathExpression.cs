using System;

namespace xFunc.Maths.Expressions.Bitwise
{

    public class AndMathExpression : BinaryMathExpression
    {

        public AndMathExpression()
            : base(null, null)
        {

        }

        public AndMathExpression(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {

        }

        public override string ToString()
        {
            if (parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} and {1})");
            }

            return ToString("{0} and {1}");
        }
        
        public override double Calculate(MathParameterCollection parameters)
        {
            return (int)firstMathExpression.Calculate(parameters) & (int)secondMathExpression.Calculate(parameters);
        }

        public override IMathExpression Clone()
        {
            return new AndMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            throw new NotSupportedException();
        }

    }

}
