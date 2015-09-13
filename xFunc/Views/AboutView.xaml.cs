// Copyright 2012-2015 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace xFunc.Views
{

    public partial class AboutView : Window
    {

        public AboutView()
        {
            InitializeComponent();

            versionNumber.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            link.RequestNavigate += (o, args) => Process.Start(args.Uri.ToString());
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        public static void LaunchEmailClient(string mailURL)
        {
            ShellExecute(IntPtr.Zero, "open", mailURL, "", "", 4);
        }

        private void MailtoHyperlink_Click(object sender, RoutedEventArgs e)
        {
            LaunchEmailClient("mailto:sys2712@gmail.com");
        }

    }

}
