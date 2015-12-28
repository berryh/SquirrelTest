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
                bool hadUpdate = false;
                using (var mgr = new UpdateManager(@"http://minio.digitalocean.berryh.tk/releases/SquirrelTest/"))
                {
                    if (mgr.IsInstalledApp)
                    {
                        ReleaseEntry entry = await mgr.UpdateApp();
                        if (entry.Version != mgr.CurrentlyInstalledVersion())
                        {
                            hadUpdate = true;
                        }
                    }
                }

                if (hadUpdate)
                {
                    UpdateManager.RestartApp();
                }
            });
        }
    }
}