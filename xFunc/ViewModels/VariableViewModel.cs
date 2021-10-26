// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Expressions.Collections;

namespace xFunc.ViewModels
{

    public class VariableViewModel
    {

        private readonly Parameter parameter;

        public VariableViewModel(Parameter parameter) => this.parameter = parameter;

        public string Variable => parameter.Key;

        public object Value => parameter.Value.Value;

        public ParameterType Type => parameter.Type;

    }

}