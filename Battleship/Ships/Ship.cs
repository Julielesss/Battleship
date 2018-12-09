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

        /// <summary>
        /// Return true if ship killed
        /// </summary>
        /// <returns></returns>
        public bool Shot()
        {
            if (++padded >= cells.Count) // >= временно, пока тестим и есть вероятность повторного выстрела в клетку
                return true;
            return false;
        }

        public void MakeShip(List<Point> points)
        {
            cells = points;

        }
        public void AddPoint(Point p)
        {
            cells.Add(p);
        }

        public void Killed(Field field)
        {
            foreach (Point point in cells)
               field.Items[(int)point.X, (int)point.Y].KillItem();
        }
    }
}