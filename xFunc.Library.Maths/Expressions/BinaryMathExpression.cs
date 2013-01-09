namespace xFunc.Library.Maths.Expressions
{

    public abstract class BinaryMathExpression : IMathExpression
    {

        protected IMathExpression parentMathExpression;
        protected IMathExpression firstMathExpression;
        protected IMathExpression secondMathExpression;

        public BinaryMathExpression(IMathExpression firstOperand, IMathExpression secondOperand)
        {
            this.FirstMathExpression = firstOperand;
            this.SecondMathExpression = secondOperand;
        }

        protected string ToString(string format)
        {
            return string.Format(format, firstMathExpression.ToString(), secondMathExpression.ToString());
        }

        public abstract double Calculate(MathParameterCollection parameters);

        public abstract IMathExpression Clone();

        public IMathExpression Derivative()
        {
            return Derivative(new VariableMathExpression('x'));
        }

        public abstract IMathExpression Derivative(VariableMathExpression variable);

        public IMathExpression FirstMathExpression
        {
            get
            {
                return firstMathExpression;
            }
            set
            {
                firstMathExpression = value;
                if (firstMathExpression != null)
                    firstMathExpression.Parent = this;
            }
        }

        public IMathExpression SecondMathExpression
        {
            get
            {
                return secondMathExpression;
            }
            set
            {
                secondMathExpression = value;
                if (secondMathExpression != null)
                    secondMathExpression.Parent = this;
            }
        }

        public IMathExpression Parent
        {
            get
            {
                return parentMathExpression;
            }
            set
            {
                parentMathExpression = value;
            }
        }

    }

}
