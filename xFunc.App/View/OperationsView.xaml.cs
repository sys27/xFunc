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
using xFunc.App.Properties;

namespace xFunc.App.View
{

    public partial class OperationsView : Window
    {

        public OperationsView()
        {
            InitializeComponent();

            this.Top = App.Current.MainWindow.Top;
            this.Left = App.Current.MainWindow.Left + App.Current.MainWindow.Width + 12;
            App.Current.MainWindow.LocationChanged += (o, args) =>
            {
                if (Environment.OSVersion.Version.Major < 6)
                    this.Top = App.Current.MainWindow.Top;
                else
                    this.Top = App.Current.MainWindow.Top - 5;
                this.Left = App.Current.MainWindow.Left + App.Current.MainWindow.Width + 12;
            };
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = System.Windows.Visibility.Hidden;
        }

    }

}
