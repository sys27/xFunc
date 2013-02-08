using System;

namespace xFunc.Maths.Expressions.Bitwise
{

    public class XOrMathExpression : BinaryMathExpression
    {

        public XOrMathExpression()
            : base(null, null)
        {

        }

        public XOrMathExpression(IMathExpression firstMathExpression, IMathExpression secondMathExpression)
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
            return new XOrMathExpression(firstMathExpression.Clone(), secondMathExpression.Clone());
        }

        public override IMathExpression Derivative(VariableMathExpression variable)
        {
            throw new NotSupportedException();
        }

    }

}
