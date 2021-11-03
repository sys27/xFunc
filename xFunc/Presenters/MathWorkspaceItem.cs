// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Results;

namespace xFunc.Presenters
{

    public class MathWorkspaceItem
    {
        public MathWorkspaceItem(string strExp, IResult result, string answer)
        {
            this.StringExpression = strExp;
            this.Result = result;
            this.Answer = answer;
        }

        public string StringExpression { get; }

        public IResult Result { get; }

        public string Answer { get; internal set; }

    }

}