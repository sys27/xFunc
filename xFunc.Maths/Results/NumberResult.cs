using System;
using System.Globalization;

namespace xFunc.Maths.Results
{

    public class NumberResult : IResult
    {

        private double number;

        public NumberResult(double number)
        {
            this.number = number;
        }

        public override string ToString()
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }

        public double Result
        {
            get
            {
                return number;
            }
        }

    }

}
