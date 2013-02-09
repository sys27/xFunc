using System;

namespace xFunc.Maths.Expressions.Bitwise
{

    public class XOr : BinaryMathExpression
    {

        public XOr()
            : base(null, null)
        {

        }

        public XOr(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
            : base(firstMathExpression, secondMathExpression)
        {

        }

        public override string ToString()
        {
            if (parentMathExpression is BinaryMathExpression)
            {
                return ToString("({0} xor {1})");
            }

            return ToString("{0} xor {1}");
        }
        
        public override double Calculate(MathParameterCollection parameters)
        {
            return (int)firstMathExpression.Calculate(parameters) ^ (int)secondMathExpression.Calculate(parameters);
        }

        public override IMathExpression Clone()
        {
            return new XOr(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

        public override IMathExpression Differentiation(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
