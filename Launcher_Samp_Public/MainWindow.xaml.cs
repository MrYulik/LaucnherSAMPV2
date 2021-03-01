using System.Windows;
using System;
using System.Net.NetworkInformation;
using System.Windows.Media;
using System.Net;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Launcher_Samp_Public
{
    /*
     * Доработан лаунчер
     * Cover Corporation
     */
    public partial class MainWindow : Window
    {
        public string IP = "135.181.113.179"; // IP Сервера
        public int PORT = 9888; // ПОРТ Сервера
        public static int GroupID = 235422513; // ID Группы
        public static string SaitURL = "http://covercorp.tk"; // URL Сайта
        public static string FileDirectoryURL = "http://covercorp.tk/files/launcher.rar";
        /*
         * /// ID группы вставлять без vk.com/
         * Например vk.com/cover_corporation а должно быть cover_corporation
         */


        // НЕ ТРОГАТЬ ЕСЛИ НОВИЧЁК!
        public string UserName;
        public static string[] www;
        public static readonly string RegistryKey = "HKEY_CURRENT_USER\\SOFTWARE\\SAMP";
        public static readonly string ConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\GTA San Andreas User Files\\SAMP";
        public static readonly string SAMPConfigPath = ConfigPath + "\\sa-mp.cfg";

        public MainWindow()
        {
            InitializeComponent();
            Load();
            Settingcs set = new Settingcs();
            set.LoadSetting();
        }


        void Load()
        {
            try
            {
                Api.IP = IP;
                Api.PORT = PORT;
                Api.Query q = new Api.Query(Api.IP, Api.PORT);
                q.Send('i');
                int count = q.Receive();
                string[] info = q.Store(count);
                Api.ServerPassword = info[0];
                Api.Player = info[1];
                Api.MaxPlayer = info[2];
                Api.ServerName = info[3];
                Api.Gamemode = info[4];
                Api.Language = info[5];
                Pass.Content = "";
                if (Api.ServerPassword == "1")
                {
                    Pass.Foreground = Brushes.Red;
                    Pass.Content += "(!)SERVER PASSWORD(!) ";
                }
                NameServer.Foreground = Brushes.White;
                NameServer.Content += Api.ServerName + "\t\tИгроки: " + Api.Player + "/" + Api.MaxPlayer;
            }
            catch { }
        }
        private void Label_Click(object sender, RoutedEventArgs e) => Process.Start("https://vk.com/club" + GroupID);
        private void LoadMain(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new Uri("Main.xaml", UriKind.Relative));
            HeroImg.Visibility = Visibility.Visible;
        }

        private void LoadPageSettings(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            bool isActive = false;
            if(!isActive)
            {
                frame.NavigationService.Navigate(new Uri("Settings.xaml", UriKind.Relative));
                HeroImg.Visibility = Visibility.Hidden;
                isActive = true;
            }
            else
            {
                frame.NavigationService.Navigate(new Uri("Main.xaml", UriKind.Relative));
                isActive = false;
            }
        }

        private void MagazClick(object sender, RoutedEventArgs e) => Process.Start(SaitURL);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation
            {
                From = 0,
                To = 450,
                Duration = TimeSpan.FromSeconds(5)
            };
            LoadBar.BeginAnimation(Border.WidthProperty, da);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.045);
            int count = 0;
            int max = 100;
            timer.Tick += new EventHandler((o, ev) => {
                count++;
                LoadText.Content = count + "%";

                if (count == max)
                {
                    LoadedText.Content = "Загрузка завершена!";
                    timer.Stop();
                }
            });
            timer.Start();
        }
    }
}
