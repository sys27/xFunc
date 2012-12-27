using System;
using System.Collections.Generic;

namespace xFunc.Library.Expressions.Logics
{

    public class LogicParameterCollection : List<char>
    {

        private int bits;

        public LogicParameterCollection() { }

        public LogicParameterCollection(IEnumerable<char> vars) : base(vars) { }

        public bool this[char variable]
        {
            get
            {
                int index = 1 << Count - IndexOf(variable) - 1;

                return (bits & index) == index;
            }
            set
            {
                int index = 1 << Count - IndexOf(variable) - 1;

                if (((bits & index) == index) != value)
                {
                    if (value)
                    {
                        bits |= index;
                    }
                    else
                    {
                        if ((bits & index) == index)
                            bits ^= index;
                    }
                }
            }
        }

        public new void Add(char c)
        {
            bits <<= Count;
            base.Add(c);
        }

        public new void Remove(char c)
        {
            bits >>= Count;
            Remove(c);
        }

        public int Bits
        {
            get { return bits; }
            set { bits = value; }
        }

    }

}
