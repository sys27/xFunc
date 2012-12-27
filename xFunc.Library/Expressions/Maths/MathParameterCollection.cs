using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace xFunc.Library.Expressions.Maths
{

    [Serializable]
    public class MathParameterCollection : Dictionary<char, double>
    {

        public MathParameterCollection()
        {
            Add('π', Math.PI);
            Add('e', Math.E);
        }

        public MathParameterCollection(IDictionary<char, double> dictionary) : base(dictionary) { }

        public MathParameterCollection(int capacity) : base(capacity) { }

        protected MathParameterCollection(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

}
