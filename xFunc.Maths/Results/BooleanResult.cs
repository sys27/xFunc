using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xFunc.Maths.Results
{

    /// <summary>
    /// Represents the boolean result.
    /// </summary>
    public class BooleanResult : IResult
    {

        private readonly bool value;

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanResult"/> class.
        /// </summary>
        /// <param name="value">The value of result.</param>
        public BooleanResult(bool value)
        {
            this.value = value;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return value.ToString();
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public bool Result
        {
            get
            {
                return value;
            }
        }

        object IResult.Result
        {
            get
            {
                return value;
            }
        }

    }

}
