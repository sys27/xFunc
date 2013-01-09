using System;

namespace xFunc.Library.Maths.Expressions
{
    
    public class VariableMathExpression : IMathExpression
    {

        private IMathExpression parentMathExpression;
        private char variable;

        public VariableMathExpression(char variable)
        {
            this.variable = variable;
        }

        public override bool Equals(object obj)
        {
            VariableMathExpression @var = obj as VariableMathExpression;
            if (@var != null && @var.Variable == this.variable)
                return true;

            return false;
        }

        public override string ToString()
        {
            return variable.ToString();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            return parameters[variable];
        }

        public IMathExpression Clone()
        {
            return new VariableMathExpression(variable);
        }

        public IMathExpression Derivative()
        {
            return Derivative(new VariableMathExpression('x'));
        }

        public IMathExpression Derivative(VariableMathExpression variable)
        {
            if (this.Equals(variable))
                return new NumberMathExpression(1);
            else
                return this.Clone();
        }

        public char Variable
        {
            get
            {
                return variable;
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
