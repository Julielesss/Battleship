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
        public string Name { get; set; }
        Item[,] items;
        List<Ship> ships;

        public Item [,] Items => items ;

        public Field()
        {
            ships = new List<Ship>();
        }
        public void Create(Item[,] arr)
        {
            items = arr;
        }
        public void AddShip(Ship sh)
        {
            ships.Add(sh);
        }

        public int checkShipsCount()
        {
            return ships.Count;
        }

    }
}
