using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Windows.Media;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using static Launcher_Samp_Public.MainWindow;
using System.Windows.Media.Imaging;
using System;
using Microsoft.Win32;
using System.Diagnostics;

namespace Launcher_Samp_Public
{
    public partial class Main : Page
    {
        public static readonly string RegistryKey = "HKEY_CURRENT_USER\\SOFTWARE\\SAMP";
        public Main()
        {
            InitializeComponent();
            Playername.Text = Registry.GetValue(RegistryKey, "PlayerName", "").ToString(); ;
        }
        private void Play(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Playername.Text.Length >= 5 && Playername.Text.Length <= 24)
                {
                    Registry.SetValue(RegistryKey, "PlayerName", Playername.Text);
                    Process.Start("samp://" + Api.IP + ":" + Api.PORT);
                }
                else
                    MessageBox.Show("Ваш Nickname не соответствует требованиями! \nNickname должен состоять от 5 и до 24 символов!");
            }
            catch (Exception err)
            {
                MessageBox.Show("Возможно у Вас не установлен SAMP! Установите его и попробуйте заного. \n\n\n\n Error:\n" + err.Message);
            }
         }

        
        
    }
}
