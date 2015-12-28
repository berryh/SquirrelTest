#region

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Squirrel;

#endregion

namespace SquirrelTest
{
    /// <summary>
    ///     Interaction logic for InfoBalloon.xaml
    /// </summary>
    public partial class InfoBalloon : UserControl
    {
        public InfoBalloon()
        {
            InitializeComponent();
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.TrayIcon?.CloseBalloon();
            UpdateManager.RestartApp();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.TrayIcon?.CloseBalloon();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindow.TrayIcon?.ResetBalloonCloseTimer();
        }
    }
}