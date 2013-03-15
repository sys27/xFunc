using System;

namespace xFunc.Maths.Expressions
{
    
    public class Undefine : IMathExpression
    {

        private Variable variable;

        public Undefine()
            : this(null)
        {

        }

        public Undefine(Variable variable)
        {
            this.variable = variable;
        }

        public double Calculate()
        {
            throw new NotSupportedException();
        }

        public double Calculate(MathParameterCollection parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            parameters.Remove(variable.Character);

            return double.NaN;
        }

        public IMathExpression Differentiate()
        {
            throw new NotSupportedException();
        }

        public IMathExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

        public IMathExpression Clone()
        {
            return new Undefine((Variable)variable.Clone());
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

        public Variable Variable
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

    }

}
