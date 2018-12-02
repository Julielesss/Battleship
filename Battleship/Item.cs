using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Battleship
{
    class Item: Button
    {
        public Item(Point position)
        {
            Position = position;
        }
        public Item()
        {
           
        }

        public Point Position { get; set; }
    }
}
