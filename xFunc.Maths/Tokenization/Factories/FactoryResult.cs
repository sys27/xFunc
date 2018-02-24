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
using xFunc.Maths.Tokenization.Tokens;

namespace xFunc.Maths.Tokenization.Factories
{

    /// <summary>
    /// The result of token factory.
    /// </summary>
    public class FactoryResult
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryResult"/> class.
        /// </summary>
        public FactoryResult() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FactoryResult"/> class.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="processedLength">The processed length.</param>
        public FactoryResult(IToken token, int processedLength)
        {
            Token = token;
            ProcessedLength = processedLength;
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public IToken Token { get; set; }
        /// <summary>
        /// Gets or sets the processed length.
        /// </summary>
        /// <value>
        /// The processed length.
        /// </value>
        public int ProcessedLength { get; set; }

    }

}