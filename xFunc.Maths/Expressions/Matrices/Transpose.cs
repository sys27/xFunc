using System;

namespace xFunc.Maths.Expressions.Matrices
{

    public class Transpose : UnaryExpression
    {

        internal Transpose() { }

        public Transpose(IExpression argument)
            : base(argument)
        {

        }

        public override object Calculate(ExpressionParameters parameters)
        {
            if (argument is Vector)
                return ((Vector)argument).Transpose();
            if (argument is Matrix)
                return ((Matrix)argument).Transpose();

            throw new NotSupportedException();
        }

        public override IExpression Clone()
        {
            return new Transpose(this.argument.Clone());
        }

        protected override IExpression _Differentiation(Variable variable)
        {
            throw new NotSupportedException();
        }

        public override IExpression Argument
        {
            get
            {
                return argument;
            }
            set
            {
                if (value != null)
                {
                    if (!(value is Vector || value is Matrix))
                        throw new NotSupportedException();

                    value.Parent = this;
                }

                argument = value;
            }
        }

    }

}
