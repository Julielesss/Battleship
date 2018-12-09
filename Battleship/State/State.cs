using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Battleship
{
    abstract class State
    {

        protected GameLogic game;
        public State(GameLogic g)
        {
            game = g;
        }

        abstract public void Start();
        abstract public void Init();
        abstract public void Show();
        abstract public void clickButtonHandler(Button sender, RoutedEventArgs e);
        abstract public void clickItemHandler(Item sender, RoutedEventArgs e);
    }
}
