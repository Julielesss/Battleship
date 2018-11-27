﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Battleship
{
    class Field
    {
        Item[,] items;
        List<Ship> ships;

        public Field()
        {
            items = new Item[10, 10];
            ships = new List<Ship>();
        }
        public void Create(UniformGrid grid)
        {
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    items[i, j] = new Item(new Point(i + 1, j + 1));
                    items[i, j].Width = items[i, j].Height = 30;
                    grid.Children.Add(items[i, j]);
                }
            }
        }

    }
}