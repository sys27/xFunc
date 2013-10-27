using System;
using System.Collections.Generic;
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

    public partial class VariableView : Window
    {

        public VariableView(MathProcessor processor)
        {
            this.DataContext = processor.Parameters.Select(v => new VariableViewModel(v));
            var view = (CollectionView)CollectionViewSource.GetDefaultView(this.DataContext);
            view.GroupDescriptions.Add(new PropertyGroupDescription("Type"));

            this.SourceInitialized += (o, args) => this.HideMinimizeAndMaximizeButtons();

            InitializeComponent();
        }

    }

}
