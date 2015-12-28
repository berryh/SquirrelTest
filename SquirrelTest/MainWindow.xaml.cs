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

            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                Task.Run(async () =>
                {
                    using (var mgr = new UpdateManager(@"http://minio.digitalocean.berryh.tk/releases/SquirrelTest/"))
                    {
                        if (mgr.CheckForUpdate().Result.ReleasesToApply.Any())
                        {
                            var x = await mgr.UpdateApp();
                            MessageBox.Show("The app has been updated.\nRestarting");
                            try
                            {
                                UpdateManager.RestartApp();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Exception: " + ex);
                            }
                        }
                    }
                });
            }));

//            Task.Run(async () =>
//            {
//                using (var mgr = new UpdateManager(@"http://minio.digitalocean.berryh.tk/releases/SquirrelTest/"))
//                {
//                    if (mgr.CheckForUpdate().Result.ReleasesToApply.Any())
//                    {
//                        var x =await mgr.UpdateApp();
//                        MessageBox.Show("The app has been updated.\nRestarting");
//                        UpdateManager.RestartApp();
//                    }
//                }
//            });
        }
    }
}