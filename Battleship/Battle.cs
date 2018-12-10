using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Battleship
{
    class Battle //Батл-бубатл
    {
        Field myField;
        Field enemyField;
        UniformGrid grdMy;
        UniformGrid grdEnemy;


        public Battle(Field my, Field enemy, UniformGrid myGrid, UniformGrid enemyGrid)
        {
            myField = my;
            enemyField = enemy;
            grdMy = myGrid;
            grdEnemy = enemyGrid;
        }


        public void Show()
        {
            grdMy.IsEnabled = false;
            grdMy.Visibility = grdEnemy.Visibility = Visibility.Visible;
        }

        public void clickButton(Button sender, RoutedEventArgs e)
        {

        }
        public void clickItem(Item sender, RoutedEventArgs e) //Жду Синглтон
        {
            ReceiveEventHandler(new MessageShot() { point = new Point(0, 0) } as BaseMessage);
            ReceiveEventHandler(new MessageShot() { point = new Point(0, 1) } as BaseMessage);
        }



        public void killedShip(Ship ship)//при убийстве корабля
        {
            //for(ship.GetType)
            //Image img = new Image();
            //img.Source = (ImageSource)Application.Current.Resources["redSquare"];
            //myField.Items[(int)p.X, (int)p.Y].Content = img;

        }

        public void checkWin()
        {
            if (enemyField.checkShipsCount() == 10) {
                win();
            }
        }

        public void win()
        {
            //отправка сообщения о победе
            //показ победного сообщения пользователю
        }

        public void ReceiveEventHandler(BaseMessage message)
        {
            if (message is MessageShot)//выстрел в нас
            {
                MessageShot shot = (MessageShot)message;
                KeyValuePair<PointStatus, Ship> pairResult = myField.Items[(int)shot.point.X, (int)shot.point.Y].ShotMyItem(myField);

                MessageResultShot answer = new MessageResultShot()
                { point = shot.point, pairPointShip = pairResult};
                //Network.Send(answer as BaseMessage);
            }
            else if (message is MessageResultShot)//результат выстрела по противнику
            {
                MessageResultShot resultShot = message as MessageResultShot;
                enemyField.Items[(int)resultShot.point.X, (int)resultShot.point.Y].ShotEnemy(resultShot.pairPointShip);
            }
        }
    }
}
