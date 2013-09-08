using System;

namespace xFunc.Maths.Results
{

    /// <summary>
    /// Represents the string result
    /// </summary>
    public class StringResult : IResult
    {

        private string str;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringResult"/> class.
        /// </summary>
        /// <param name="str">The string representation of result.</param>
        public StringResult(string str)
        {
            this.str = str;
        }

        /// <summary>
        /// Returns a <see cref="String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return str;
        }

        /// <summary>
        /// Gets the string representation of result.
        /// </summary>
        /// <value>
        /// The string representation of result.
        /// </value>
        public string Result
        {
            get
            {
                return str;
            }
        }

    }

}
