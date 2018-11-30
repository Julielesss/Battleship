using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Battleship
{
    class StatePlacement : IState
    {
        GameLogic game;

        public StatePlacement(GameLogic g)
        {
            game = g;
            Init();
            Show();
        }

        public void Init()
        {
            game.InitShipPlacement();
        }

        public void clickButtonHandler(Button sender, RoutedEventArgs e)
        {
            game.clickButtonPlacement(sender, e);
        }

        public void clickItemHandler(Item sender, RoutedEventArgs e)
        {
            game.clickItemPlacement(sender, e);
        }

        public void Show()
        {
            game.ShowPlacement();
        }
    }
}
