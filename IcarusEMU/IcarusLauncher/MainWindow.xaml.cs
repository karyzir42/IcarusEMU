// This program is a private software, based on c# source code.
// To sell or change credits of this software is forbidden,
// except if someone approve it from NecrozProject Team.
// 
// Copyrights © 2014-2015 NecrozProject Team. All rights reserved.

using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using Hik.Communication.ScsServices.Service;

namespace IcarusLauncher
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        protected string Token;
        public static IScsServiceClient<ILauncherContract> LauncherService;

        public MainWindow()
        {
            InitializeComponent();
            LauncherService =
                ScsServiceClientBuilder.CreateClient<ILauncherContract>(new ScsTcpEndPoint("127.0.0.1", 6666));

            LauncherService.ConnectTimeout = 3000;
            LauncherService.Timeout = 1000;

            LauncherService.Connect();

            StringCollection news = LauncherService.ServiceProxy.GetNews();

            foreach (string s in news)
                NewsBlock.Text += "\n" + s;

            #region status

            string[] status = LauncherService.ServiceProxy.GetStatusAboutServers().Split('@');

            string gameServerStatus = status[0];
            string loginServerStatus = status[1];
            string lobbyServerStatus = status[2];

            if (gameServerStatus == "Online")
            {
                GameServerStatus.Content = "Online";
                GameServerStatus.Foreground = Brushes.LawnGreen;
            }
            else if (gameServerStatus == "Offline")
            {
                GameServerStatus.Content = "Offline";
                GameServerStatus.Foreground = Brushes.Red;
            }
            if (loginServerStatus == "Online")
            {
                LoginServerStatus.Content = "Online";
                LoginServerStatus.Foreground = Brushes.LawnGreen;
            }
            else if (loginServerStatus == "Offline")
            {
                LoginServerStatus.Content = "Offline";
                LoginServerStatus.Foreground = Brushes.Red;
            }
            if (lobbyServerStatus == "Online")
            {
                LobbyServerStatus.Content = "Online";
                LobbyServerStatus.Foreground = Brushes.LawnGreen;
            }
            else if (lobbyServerStatus == "Offline")
            {
                LobbyServerStatus.Content = "Offline";
                LobbyServerStatus.Foreground = Brushes.Red;
            }

            #endregion

            UsersOnline.Content += string.Format("Игроков онлайн: {0}",
                LauncherService.ServiceProxy.GetTotalPlayersOnline());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginBox.Text) && string.IsNullOrEmpty(PasswordBox.Text))
                return;

            var launcherBin = new FileInfo("./bin32/Launcher.exe");
            if (launcherBin.Exists)
            {
                Token = LauncherService.ServiceProxy.GetToken(LoginBox.Text, PasswordBox.Text);
                if (Token == null)
                {
                    MessageBox.Show("INCORRECT LOGIN OR PASSWORD");
                    Close();
                }
                Process.Start(launcherBin.FullName, string.Format("/i:{0} /r:0000000 /O /u:0000002 /m:P", Token));
            }
            Close();
        }

        [ScsService]
        public interface ILauncherContract
        {
            string GetToken(string name, string password);
            string GetStatusAboutServers();
            StringCollection GetNews();
            int GetTotalPlayersOnline();
        }
    }
}