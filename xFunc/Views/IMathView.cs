using System;
using System.Collections.Generic;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public interface IMathView : IView
    {

        IEnumerable<MathWorkspaceItemViewModel> MathExpressions { set; }

    }

}
