#region

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Squirrel;

#endregion

namespace SquirrelTest
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                using (var mgr = new UpdateManager(@"http://minio.digitalocean.berryh.tk/releases/SquirrelTest/"))
                {
                    ReleaseEntry entry = await mgr.UpdateApp();
                    if (entry.Version != mgr.CurrentlyInstalledVersion())
                    {
                        UpdateManager.RestartApp();
                    }
                }
            });
        }
    }
}