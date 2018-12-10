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

namespace Battleship
{
    public partial class MainWindow : Window
    {
        GameLogic game;

        public MainWindow()
        {
            InitializeComponent();
            game = new GameLogic(grdMyField, grdEnemyField, grdShipState);
            FieldsCreate();
            game.SetState(new StatePlacement(game));
            game.Start();
        }

        public void FieldsCreate()
        {
            Item[,] myItems;
            Item[,] enemyItems;
            myItems = new Item[10, 10];
            enemyItems = new Item[10, 10];

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    Point pos = new Point(j, i);
                    myItems[j, i] = new Item(pos);
                    enemyItems[j, i] = new Item(pos);

                    myItems[j, i].Style = enemyItems[j, i].Style = (Style)TryFindResource("ItemStyle");                         
                    grdMyField.Children.Add(myItems[j, i]);
                    grdEnemyField.Children.Add(enemyItems[j, i]);
                }
            }
            grdMyField.Visibility = grdEnemyField.Visibility = Visibility.Hidden;
            game.FieldsCreate(myItems, enemyItems);
        }

        public void clickItem(object sender, RoutedEventArgs e)
        {
            game.clickItem(sender as Item, e);
        }
        private void clickButton(object sender, RoutedEventArgs e) // обработчик запускается только для Enable
        {
            game.clickButton(sender as Button, e);            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            HelloWnd helloWnd = new HelloWnd();
            helloWnd.Closing += HelloWnd_Closed;
            helloWnd.Show();

        }

        private void HelloWnd_Closed(object sender, EventArgs e)
        {
            game.SetMyName((sender as HelloWnd).userName);
            this.IsEnabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Network.Close();
        }
    }
}
