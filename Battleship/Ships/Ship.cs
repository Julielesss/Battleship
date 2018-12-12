using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Battleship
{
    [Serializable]
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
            if (++padded >= cells.Count) 
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

        public void Killed(Field field, bool my = true)
        {
            int count = 1;
            int size = cells.Count;
            bool isVertical = false;

            if (!my && cells.Count>1 && cells[0].X == cells[1].X)
                    isVertical = true;
            

            foreach (Point point in cells)
            {
                if (my)
                    field.Items[(int)point.X, (int)point.Y].KillMyItem();
                else
                    field.Items[(int)point.X, (int)point.Y].KillEnemyItem(count, size, isVertical);

                ++count;
            }
        }


        public void turnOffItem(Field field, bool my = true)
        {
            int[,] temp = { { 0, -1 }, { 0, 1 }, { -1, 0 }, { 1, 0 }, { 1, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 } };
            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int X1 = (int)Cells[i].X + temp[j, 0];
                    int Y1 = (int)Cells[i].Y + temp[j, 1];

                    if (Y1 >= 0 && Y1 < 10 && X1 >= 0 && X1 < 10)
                        Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() =>
                        {
                            field.Items[X1, Y1].IsEnabled = false;
                            //field.Items[X1, Y1].Status = PointStatus.past;
                         }));
                }
            }
        }
    }
}