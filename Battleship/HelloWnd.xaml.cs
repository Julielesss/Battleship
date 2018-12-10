using System;
using System.Collections.Generic;
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
        string title = "Привет капитан";
        string tbText = "Введите свое имя:";
        public string userName { get; private set; }
        public bool isConnected { get; set; } = false;

        public HelloWnd()
        {
            InitializeComponent();
            Title = title;
            TextBlock.Text = tbText;
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
            isConnected = true;
            Thread.Sleep(1000);

            Dispatcher.BeginInvoke(new ThreadStart(() => this.Close()));
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            userName = textBox.Text;
            Button btn = sender as Button;
            if (btn.Tag.ToString() == "Start")
            {
                Network.CreateServer();
                connect(btn);
            }
            else if (btn.Tag.ToString() == "Cancel")
            {
                Network.Close();
                btn.Tag = "Start";
                btn.Content = "Создать игру";
                lblState.Content = "";
            }
                   
            //this.Close // надо закомментить строчки выше и раскомментить эту
        }

        private void ConnectBTN_Click(object sender, RoutedEventArgs e)
        {
            userName = textBox.Text;
            Button btn = sender as Button;

            if (btn.Tag.ToString() == "Start")
            {
                Network.CreateClient();
                connect(btn);
            }
            else if (btn.Tag.ToString() == "Cancel")
            {
                Network.Close();
                btn.Tag = "Start";
                btn.Content = "Присоединиться к игре";
                lblState.Content = "";
            }

            //this.Close // надо закомментить две строчки выше и раскомментить эту

        }

        private void connect(Button btn)
        {
            Network.ConnectedEvent += Network_ConnectedEvent;
            btn.Tag = "Cancel";
            btn.Content = "Отменить подключение";

            Dispatcher.BeginInvoke(new ThreadStart(() => lblState.Content = "Connecting"));
        }
    }
}
