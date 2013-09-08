using System;
using System.Globalization;

namespace xFunc.Maths.Results
{

    /// <summary>
    /// Represents the numerical result.
    /// </summary>
    public class NumberResult : IResult
    {

        private double number;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberResult"/> class.
        /// </summary>
        /// <param name="number">The numerical representation of result.</param>
        public NumberResult(double number)
        {
            this.number = number;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return number.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the numerical representation of result.
        /// </summary>
        /// <value>
        /// The numerical representation of result.
        /// </value>
        public double Result
        {
            get
            {
                return number;
            }
        }

    }

}
