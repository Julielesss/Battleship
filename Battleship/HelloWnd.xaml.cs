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
using System.Windows.Shapes;

namespace Battleship
{
    /// <summary>
    /// Логика взаимодействия для HelloWnd.xaml
    /// </summary>
    public partial class HelloWnd : Window
    {
        string title="Привет капитан";
        string tbText = "Введите свое имя:";
        public string userName { get; private set; }
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

        private void ConnectBTN_Click(object sender, RoutedEventArgs e)
        {
            userName = textBox.Text;
            Network.CreateClient();
            this.Close();
            
        }

        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            userName = textBox.Text;
            Network.CreateServer();
            this.Close();
        }
    }
}
