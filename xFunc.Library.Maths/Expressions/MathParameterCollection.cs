using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace xFunc.Library.Maths.Expressions
{

    [Serializable]
    public class MathParameterCollection : Dictionary<char, double>
    {

        public MathParameterCollection()
        {
            Add('π', Math.PI);
            Add('e', Math.E);
        }

        protected MathParameterCollection(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

}
