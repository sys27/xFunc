// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public interface IGraphsView
    {

        IEnumerable<GraphItemViewModel> Graphs { set; }

    }

}