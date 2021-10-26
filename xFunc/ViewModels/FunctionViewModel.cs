// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions;

namespace xFunc.ViewModels
{

    public class FunctionViewModel
    {
        private readonly IExpression value;

        public FunctionViewModel(UserFunction function, IExpression value)
        {
            this.Function = function;
            this.value = value;
        }

        public UserFunction Function { get; }

        public string Value => value.ToString();

    }

}