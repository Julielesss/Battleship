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

        public void turnOffItem(Field field)
        {
            int[,] temp = { { 0, -1 }, { 0, 1 }, { -1, 0 }, { 1, 0 }, { 1, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 } };
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    int X1 = (int)Cells[i].X + temp[j, 0];
                    int Y1 = (int)Cells[i].Y + temp[j, 1];

                    if (Y1 >= 0 && Y1 < 10 && X1 >= 0 && X1 < 10)
                        field.Items[Y1, X1].IsEnabled = false;
                }
            }
        }
    }
}