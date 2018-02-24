// Copyright 2012-2018 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{

    /// <summary>
    /// The factory which creates complex number tokens.
    /// </summary>
    /// <seealso cref="xFunc.Maths.Tokenization.Factories.FactoryBase" />
    public class ComplexNumberTokenFactory : FactoryBase
    {

        private Regex regexAllWhitespaces = new Regex(@"\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexNumberTokenFactory"/> class.
        /// </summary>
        public ComplexNumberTokenFactory() : base(new Regex(@"\G([+-]?\s*\d*\.?\d+)\s*([∠+-]+\s*\s*\d*\.?\d+)°", RegexOptions.Compiled | RegexOptions.IgnoreCase)) { }

        private bool DoubleTryParse(string str, out double number)
        {
            return double.TryParse(str, NumberStyles.Number, CultureInfo.InvariantCulture, out number);
        }

        /// <summary>
        /// Creates the token.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>
        /// The token.
        /// </returns>
        protected override FactoryResult CreateTokenInternal(Match match, ReadOnlyCollection<IToken> tokens)
        {
            string magnitudeString = regexAllWhitespaces.Replace(match.Groups[1].Value, string.Empty);
            if (!DoubleTryParse(magnitudeString, out double magnitude))
                magnitude = 0.0;

            string phaseString = regexAllWhitespaces.Replace(match.Groups[2].Value, string.Empty).Replace("∠", "");
            if (!DoubleTryParse(phaseString, out double phase))
                phase = 1.0;

            var token = new ComplexNumberToken(Complex.FromPolarCoordinates(magnitude, phase * Math.PI / 180));
            return new FactoryResult(token, match.Length);
        }

    }

}