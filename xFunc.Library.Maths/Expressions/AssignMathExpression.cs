using System;

namespace xFunc.Library.Maths.Expressions
{

    public class AssignMathExpression : IMathExpression
    {

        private VariableMathExpression variable;
        private IMathExpression value;

        public AssignMathExpression()
            : this(null, null)
        {

        }

        public AssignMathExpression(VariableMathExpression variable, IMathExpression value)
        {
            this.variable = variable;
            this.value = value;
        }

        public double Calculate(MathParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            parameters[variable.Variable] = value.Calculate(parameters);

            return double.NaN;
        }

        public IMathExpression Derivative()
        {
            throw new NotSupportedException();
        }

        public IMathExpression Derivative(VariableMathExpression variable)
        {
            throw new NotSupportedException();
        }

        public IMathExpression Clone()
        {
            return new AssignMathExpression((VariableMathExpression)variable.Clone(), value.Clone());
        }

        public VariableMathExpression Variable
        {
            get
            {
                return variable;
            }
            set
            {
                variable = value;
            }
        }

        public IMathExpression Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public IMathExpression Parent
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

    }

}
