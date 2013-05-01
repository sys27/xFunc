using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xFunc.Maths.Expressions
{

    [Serializable]
    public class MathFunctionCollection : Dictionary<string, IMathExpression>
    {

        public MathFunctionCollection()
        {

        }

        protected MathFunctionCollection(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

}
