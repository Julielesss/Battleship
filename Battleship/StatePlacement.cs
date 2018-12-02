using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Battleship
{
    class StatePlacement : State
    {
        public StatePlacement(GameLogic g) : base(g)
        { }
        public override void Start()
        {
            Init();
            Show();
        }

        public override void Init()
        {
            game.InitShipPlacement();
        }

        public override void Show()
        {
            game.ShowPlacement();
        }

        public override void clickButtonHandler(Button sender, RoutedEventArgs e)
        {
            game.clickButtonPlacement(sender, e);
        }

        public override void clickItemHandler(Item sender, RoutedEventArgs e)
        {
            game.clickItemPlacement(sender, e);
        }
    }
}
