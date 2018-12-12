using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Battleship
{
    /// <summary>
    /// Логика взаимодействия для HelloWnd.xaml
    /// </summary>
    public partial class HelloWnd : Window
    {
        string title = "Добро пожаловать Эльф!";
        string tbText = "\nВведите свое имя:";
        public string userName { get; private set; }
        public bool isConnected { get; set; } = false;
        public bool TryConnect = false;

        public HelloWnd()
        {
            InitializeComponent();
            Title = title;
            TextBlock.Text = tbText;

            textBox.Focus();
            CreateBtn.IsEnabled = false;
            ConnectBtn.IsEnabled = false;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
      {
            if (textBox.Text != "")
            {
                CreateBtn.IsEnabled = true;
                ConnectBtn.IsEnabled = true;
            }
            else
            {
                CreateBtn.IsEnabled = false;
                ConnectBtn.IsEnabled = false;
            }
        }

        private void Network_ConnectedEvent()
        {
            Dispatcher.BeginInvoke(new ThreadStart(() => lblState.Content = "Connected"));
            TryConnect = false;
            isConnected = true;
            Thread.Sleep(500);

            Dispatcher.BeginInvoke(new ThreadStart(() => this.Close()));
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            userName = textBox.Text;
            Button btn = sender as Button;
            if (btn.Tag.ToString() == "Start")
            {
                Network.CreateServer();
                ConnectBtn.IsEnabled = false;
                connect(btn);
            }
            else if (btn.Tag.ToString() == "Cancel")
            {
                TryConnect = false;
                btn.Tag = "Start";
                Image img = new Image();
                img.Source = (ImageSource)Application.Current.Resources["CreateGameBTN"];
                btn.Content = img;
                // btn.Content = "Создать игру";
                ConnectBtn.IsEnabled = true;
                lblState.Content = "";
            }
        }

        private void ConnectBTN_Click(object sender, RoutedEventArgs e)
        {
            userName = textBox.Text;
            Button btn = sender as Button;

            if (btn.Tag.ToString() == "Start")
            {
                Network.CreateClient();
                CreateBtn.IsEnabled = false;
                connect(btn);
            }
            else if (btn.Tag.ToString() == "Cancel")
            {
                Network.Close();
                TryConnect = false;
                CreateBtn.IsEnabled = true;
                btn.Tag = "Start";
                Image img = new Image();
                img.Source = (ImageSource)Application.Current.Resources["ConnectBTN"];
                btn.Content = img;
                //btn.Content = "Присоединиться к игре";
                lblState.Content = "";
            }
        }

        private void connect(Button btn)
        {
            Network.ConnectedEvent += Network_ConnectedEvent;
            TryConnect = true;
            btn.Tag = "Cancel";
            Image img = new Image();
            img.Source = (ImageSource)Application.Current.Resources["CancelButton"];
            btn.Content = img;
            //btn.Content = "Отменить подключение";

            Dispatcher.BeginInvoke(new ThreadStart(() => lblState.Content = "Connecting"));
        }
    }
}
