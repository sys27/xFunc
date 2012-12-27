using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Library.Expressions.Logics
{

    public class TruthTableExpression : UnaryLogicExpression
    {

        public TruthTableExpression() : base(null) { }

        public TruthTableExpression(ILogicExpression expression) : base(expression) { }

        public override bool Calculate(LogicParameterCollection parameters)
        {
            throw new NotSupportedException();
        }

    }

}
