#region

using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using Hardcodet.Wpf.TaskbarNotification;
using Squirrel;

#endregion

namespace SquirrelTest
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TaskbarIcon TrayIcon;

        public MainWindow()
        {
            InitializeComponent();
            TrayIcon = new TaskbarIcon {Visibility = Visibility.Hidden};
            Task.Run(async () =>
            {
                bool hadUpdate = false;
                try
                {
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
                }
                catch (Exception)
                {
                    // ignored
                    // Used for debugging purposes
                }
                if (hadUpdate)
                {
                    Dispatcher.Invoke(() => { TrayIcon.ShowCustomBalloon(new InfoBalloon(), PopupAnimation.Fade, 5000); });
                }
            });
        }
    }
}