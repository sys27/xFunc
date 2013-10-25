// Copyright 2012-2013 Dmitry Kischenko
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
using xFunc.Maths.Results;

namespace xFunc.Presenters
{

    public class MathWorkspaceItem
    {

        private string strExp;
        private IResult result;
        private string answer;

        public MathWorkspaceItem(string strExp, IResult result)
        {
            this.strExp = strExp;
            this.result = result;
            this.answer = result.ToString();
        }

        public string StringExpression
        {
            get
            {
                return strExp;
            }
        }

        public IResult Result
        {
            get
            {
                return result;
            }
        }

        public string Answer
        {
            get
            {
                return answer;
            }
            internal set
            {
                answer = value;
            }
        }

    }

}
