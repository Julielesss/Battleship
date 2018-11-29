using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Battleship
{
    interface IState
    {
        void Init();
        void Show();
        void clickButtonHandler(Button sender, RoutedEventArgs e);
        void clickItemHandler(Item sender, RoutedEventArgs e);
    }
}
