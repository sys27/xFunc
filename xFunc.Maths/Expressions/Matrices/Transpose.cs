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
            throw new NotImplementedException();
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
                if (!(argument is Vector || argument is Matrix))
                    throw new NotSupportedException();

                argument = value;
                if (argument != null)
                    argument.Parent = this;
            }
        }

    }

}
