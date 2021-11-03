// Copyright (c) Dmytro Kyshchenko. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Navigation;

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
        private static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);

        private static void LaunchEmailClient(string mailURL)
        {
            ShellExecute(IntPtr.Zero, "open", mailURL, "", "", 4);
        }

        private void MailtoHyperlink_Click(object sender, RoutedEventArgs e)
        {
            LaunchEmailClient("mailto:sys2712@gmail.com");
        }

        private void DocsHyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
        }
    }

}