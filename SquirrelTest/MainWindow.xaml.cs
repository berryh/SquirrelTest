using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Squirrel;

namespace SquirrelTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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
                    if (mgr.CheckForUpdate().Result.ReleasesToApply.Any())
                    {
                        await mgr.UpdateApp();
                        MessageBox.Show("The app has been updated.\nRestarting");
                        UpdateManager.RestartApp();
                    }
                }
            });
        }
    }
}
