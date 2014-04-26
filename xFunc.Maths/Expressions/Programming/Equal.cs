using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions.Programming
{

    public class Equal : BinaryExpression
    {

        internal Equal() { }

        public Equal(IExpression left, IExpression right)
            : base(left, right) { }

        public override object Calculate(ExpressionParameters parameters)
        {
            throw new NotImplementedException();
        }

        public override IExpression Clone()
        {
            return new Equal(left.Clone(), right.Clone());
        }

        public override IExpression Differentiate(Variable variable)
        {
            throw new NotSupportedException();
        }

    }

}
