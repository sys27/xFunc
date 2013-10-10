using System;
using System.Collections.Generic;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public interface IGraphsView
    {

        IEnumerable<GraphItemViewModel> Graphs { set; }

    }

}
