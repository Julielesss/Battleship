using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Battleship
{
    class StateBattle : State
    {
        public StateBattle(GameLogic g) : base(g)
        {}

        public override void Start() // возможно сделать не абстрактным
        {
            Init();
            Show();
        }

        public override void clickButtonHandler(Button sender, RoutedEventArgs e)
        {
            game.clickButtonBattle(sender, e);
        }

        public override void clickItemHandler(Item sender, RoutedEventArgs e)
        {
            game.clickItemBattle(sender, e);
        }

        public override void Init()
        {
            game.InitBattle();
        }

        public override void Show()
        {
            game.ShowBattle();
        }
    }
}
