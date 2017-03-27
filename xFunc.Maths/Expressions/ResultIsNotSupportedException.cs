// Copyright 2012-2017 Dmitry Kischenko
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
using System.Linq;
using System.Runtime.Serialization;

namespace xFunc.Maths.Expressions
{

    // TODO: !!!
    [Serializable]
    public class ResultIsNotSupportedException : Exception
    {

        public ResultIsNotSupportedException() { }

        public ResultIsNotSupportedException(object that, params object[] result)
            : this(string.Format(
                "The result of calculation is not supported (Function: '{0}({1})').",
                that.GetType().Name,
                string.Join(", ", result.Select(x => x.GetType().Name))))
        { }

        public ResultIsNotSupportedException(string message) : base(message) { }

        public ResultIsNotSupportedException(string message, Exception inner) : base(message, inner) { }

        protected ResultIsNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }

}
