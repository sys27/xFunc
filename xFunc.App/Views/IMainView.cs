using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xFunc.Library.Maths;

namespace xFunc.App.Views
{

    public interface IMainView
    {

        IEnumerable<MathWorkspaceItem> MathExpressions { set; }

    }

}
