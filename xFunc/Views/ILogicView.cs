using System;
using System.Collections.Generic;
using xFunc.ViewModels;

namespace xFunc.Views
{
    
    public interface ILogicView : IView
    {

        IEnumerable<LogicWorkspaceItemViewModel> LogicExpressions { set; }

    }

}
