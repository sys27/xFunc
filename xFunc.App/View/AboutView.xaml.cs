using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace xFunc.App.View
{

    public partial class AboutView : Window
    {

        public AboutView()
        {
            InitializeComponent();
            versionNumber.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            link.RequestNavigate += (o, args) => Process.Start(args.Uri.ToString());

            Uri uri = new Uri("/LICENSE", UriKind.Relative);
            using (Stream file = Application.GetResourceStream(uri).Stream)
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    license.Text = reader.ReadToEnd();
                }
            }
        }

    }

}
