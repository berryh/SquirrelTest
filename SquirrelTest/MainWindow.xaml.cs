#region

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
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
                    TrayIcon.Visibility = Visibility.Visible;
                    TrayIcon.ShowCustomBalloon(new InfoBalloon(), PopupAnimation.Fade, 5000);
                    TrayIcon.Visibility = Visibility.Hidden;
                }
            });
        }
    }
}