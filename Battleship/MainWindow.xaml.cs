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
            game = new GameLogic(grdMyField,null, grdShipState);
            MyFieldCreate();
            game.SetState(new StatePlacement(game));
        }

        public void MyFieldCreate()
        {
            Item[,] items;
            items = new Item[10, 10];

            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    items[i, j] = new Item(new Point(j + 1, i + 1));
                    items[i, j].Width = items[i, j].Height = 30;
                    items[i, j].Click += clickItem;

                   grdMyField.Children.Add(items[i, j]);
                }
            }
            grdMyField.Visibility = Visibility.Hidden;
            game.FieldsCreate(items);
        }

        public void clickItem(object sender, RoutedEventArgs e)
        {
            game.clickItem(sender as Item, e);
        }
        private void clickButton(object sender, RoutedEventArgs e) // обработчик запускается только для Enable
        {
            game.clickButton(sender as Button, e);            
        }
    }
}
