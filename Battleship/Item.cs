using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public string imgPath { get; set; }

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
            Dispatcher.BeginInvoke(new ThreadStart(() => SetImg()));
            
            pairResult = new KeyValuePair<PointStatus, Ship>(Status, null);
            return pairResult;
        }

       public void SetImg(bool isMy = true)
        {
            Image img = new Image();
            if (Status == PointStatus.past)
                img.Source = (ImageSource)Application.Current.Resources["pastSquare"];
                
            else if (Status == PointStatus.wounded)
            {
                if (isMy)
                    img.Source = (ImageSource)Application.Current.Resources[imgPath + "B"];
                else
                    img.Source = (ImageSource)Application.Current.Resources["Gift"];

            }
            else if (Status == PointStatus.killed)
                img.Source = (ImageSource)Application.Current.Resources[imgPath + "B"];

            this.Content = img;
        }


        public void KillEnemyItem(int count, int size, bool isVertical)
        {
            Status = PointStatus.killed;
            Dispatcher.BeginInvoke(new ThreadStart(() => SetImgEnemyKill(count, size, isVertical)));
        }


        public void KillMyItem()
        {
            Status = PointStatus.killed;
            Dispatcher.BeginInvoke(new ThreadStart(() => SetImg()));
        }

        public void SetImgEnemyKill(int count, int size, bool isVertical)
        {
            Image img = new Image();
            string name = size.ToString() + "Deck";
            if (size == 2 || size == 4)
            {
                if (count < size)
                    name += "F";
                else
                    name += "E";
            }
            else if(size==3)
            {
                if (count == 1)
                    name = "3DeckF";
                else if (count == 2)
                    name = "3DeckS";
                else
                    name = "3DeckE";
            }

            if (isVertical)
                name += "V";
            img.Source = (ImageSource)Application.Current.Resources[name];
            this.Content = img;
        }

    }
}
