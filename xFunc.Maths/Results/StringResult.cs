using System;

namespace xFunc.Maths.Results
{

    public class StringResult : IResult
    {

        private string str;

        public StringResult(string str)
        {
            this.str = str;
        }

        public override string ToString()
        {
            return str;
        }

        public string Result
        {
            get
            {
                return str;
            }
        }

    }

}
