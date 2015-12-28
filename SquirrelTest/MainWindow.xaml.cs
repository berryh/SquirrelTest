#region

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
            TrayIcon = new TaskbarIcon { Visibility = Visibility.Hidden };
            TrayIcon.ShowBalloonTip("Checking for updates","Update checking",BalloonIcon.Info);
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
                            MessageBox.Show("Had Update");
                        }
                    }
                }

                if (hadUpdate)
                {
                    TrayIcon.ShowCustomBalloon(new InfoBalloon(), PopupAnimation.Fade, 5000);
                }
            });
        }
    }
}