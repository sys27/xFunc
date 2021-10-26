// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using xFunc.Maths.Results;
using xFunc.Presenters;

namespace xFunc.ViewModels
{

    public class MathWorkspaceItemViewModel
    {
        public MathWorkspaceItemViewModel(int index, MathWorkspaceItem item)
        {
            this.Index = index;
            this.Item = item;
        }

        public int Index { get; }

        public MathWorkspaceItem Item { get; }

        public string StringExpression => Item.StringExpression;

        public IResult Result => Item.Result;

        public string Answer => Item.Answer;

    }

}