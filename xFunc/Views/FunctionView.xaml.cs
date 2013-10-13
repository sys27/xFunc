using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using xFunc.Maths;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class FunctionView : Window
    {

        public FunctionView(MathProcessor processor)
        {
            this.DataContext = processor.UserFunctions.Select(f => new FunctionViewModel(f.Key.ToString(), f.Value.ToString()));

            InitializeComponent();
        }

    }

}
