using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Battleship
{
    class Field
    {
        Item[,] items;
        List<Ship> ships;

        public Item [,] Items => items ;

        public Field()
        {
           // items = new Item[10, 10];
            ships = new List<Ship>();
        }
        public void Create(Item[,] arr)
        {
            items = arr;
        }
    }
}
