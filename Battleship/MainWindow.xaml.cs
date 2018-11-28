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
            game = new GameLogic(grdMyField,null);
            game.FieldsCreate();
            game.InitShipPlacement(grdShipState);
        }

        private void btnPlacementSizeClick(object sender, RoutedEventArgs e) // обработчик запускается только для Enable
        {
            game.clickSelectShipType(sender as Button);
        }
        private void btnTurn_Click(object sender, RoutedEventArgs e)
        {
            game.btnTurn_Click(sender as Button);
        }

    }
}
