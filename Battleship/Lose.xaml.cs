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
    public partial class Lose : Window
    {


        public Lose()
        {
            InitializeComponent();

        }



        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ConnectBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Application.Current.Dispatcher.BeginInvoke
                (new ThreadStart(() => Application.Current.Shutdown()));
        }

      
    }
}
