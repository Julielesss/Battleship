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
        public PointStatus Status { get; set; }

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
                Status = PointStatus.past;
            else
            {
                if (ship.Shot())
                {
                    ship.Killed(field);
                    pairResult = new KeyValuePair<PointStatus, Ship>(Status, ship);
                    return pairResult;
                }
                else
                    Status = PointStatus.wounded;
            }
            SetImg();
            pairResult = new KeyValuePair<PointStatus, Ship>(Status, null);
            return pairResult;
        }

        public void SetImg()
        {
            if (Status == PointStatus.past)
            {
                Image img = new Image();
                img.Source = (ImageSource)Application.Current.Resources["pastSquare"];
                this.Content = img;
            }
            else if (Status == PointStatus.wounded)
            {
                Image img = new Image();
                img.Source = (ImageSource)Application.Current.Resources["yellowSquare"];
                this.Content = img;
            }
            else if (Status == PointStatus.killed)
            {
                Image img = new Image();
                img.Source = (ImageSource)Application.Current.Resources["redSquare"];
                this.Content = img;
            }
        }

        public void ShotEnemy(KeyValuePair<PointStatus, Ship> pair) // это при работе с чужим кораблем
        {
            this.Status = pair.Key;
            if (pair.Value != null)
            {
                
            }
        }

        public void KillItem()
        {
            Status = PointStatus.killed;
            SetImg();
        }


    }
}
