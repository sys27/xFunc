using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Expressions
{
    
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class ReverseFunctionAttribute : Attribute
    {

        private Type reverseType;

        public ReverseFunctionAttribute(Type reverseType)
        {
            this.reverseType = reverseType;
        }

        public Type ReverseType
        {
            get
            {
                return reverseType;
            }
        }

    }

}
