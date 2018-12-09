using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Battleship
{
    class Item: Button
    {
        public Point Position { get; set; }
        public Ship ship { get; set; } = null;
        PointStatus status;

        public Item(Point position)
        {
            Position = position;
        }
        public Item()
        {  }

        public KeyValuePair<PointStatus, Ship> ShotMyItem(Field field)
        {
            KeyValuePair<PointStatus, Ship> pairResult;
            if (ship == null)
                status = PointStatus.past;
            else
            {
                if (ship.Shot())
                {
                    ship.Killed(field);
                    pairResult = new KeyValuePair<PointStatus, Ship>(status, ship);
                    return pairResult;
                }
                else
                    status = PointStatus.wounded;
            }
            SetImg();
            pairResult = new KeyValuePair<PointStatus, Ship>(status, null);
            return pairResult;
        }

        private void SetImg()
        {
            if (status == PointStatus.past)
            {
                Image img = new Image();
                img.Source = (ImageSource)Application.Current.Resources["pastSquare"];
                this.Content = img;
            }
            else if (status == PointStatus.wounded)
            {
                Image img = new Image();
                img.Source = (ImageSource)Application.Current.Resources["yellowSquare"];
                this.Content = img;
            }
            else if (status == PointStatus.killed)
            {
                Image img = new Image();
                img.Source = (ImageSource)Application.Current.Resources["redSquare"];
                this.Content = img;
            }
        }

        public void ShotEnemy(KeyValuePair<PointStatus, Ship> pair) // это при работе с чужим кораблем
        {
            this.status = pair.Key;
            if (pair.Value != null)
            {
                
            }
        }

        public void KillItem()
        {
            status = PointStatus.killed;
            SetImg();
        }


    }
}
