using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Battleship
{
    class Ship
    {
        List<Point> cells;
        int padded;

        public List<Point> Cells

        {
            get { return cells; }
            set { cells = value; }

        }

        public Ship()
        {
            cells = new List<Point>();
            padded = 0;
        }

        public void Shot()
        {
            --padded;
        }

        public void MakeShip(List<Point> points)
        {
            cells = points;
        }
        public void AddPoint(Point p)
        {
            cells.Add(p);
        }
    }
}