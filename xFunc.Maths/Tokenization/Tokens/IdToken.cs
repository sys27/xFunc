// Copyright 2012-2020 Dmytro Kyshchenko
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
using System.Diagnostics;

namespace xFunc.Maths.Tokenization.Tokens
{
    /// <summary>
    /// Represents a id token.
    /// </summary>
    [DebuggerDisplay("Id: {" + nameof(Id) + "}")]
    internal sealed class IdToken : IToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdToken"/> class.
        /// </summary>
        /// <param name="id">An id.</param>
        public IdToken(string id)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(id), "The id should not be empty.");

            Id = id;
        }

        /// <summary>
        /// Gets an id.
        /// </summary>
        public string Id { get; }
    }
}