using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Battleship
{
    class Battle
    {
        Field myField;
        Field enemyField;
        UniformGrid grdMy;
        UniformGrid grdEnemy;


        public Battle(Field my, Field enemy, UniformGrid myGrid, UniformGrid enemyGrid)
        {
            myField = my;
            enemyField = enemy;
            grdMy = myGrid;
            grdEnemy = enemyGrid;
        }

        public void Show()
        {
            grdMy.IsEnabled = false;
            grdMy.Visibility = grdEnemy.Visibility = Visibility.Visible;
        }

        public void clickButton(Button sender, RoutedEventArgs e)
        {

        }
        public void clickItem(Item sender, RoutedEventArgs e)
        {

        }



    }
}
