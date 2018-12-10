using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using System.Threading;

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
            Network.ReceiveMessageEvent += ReceiveEventHandler;
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
            //    ReceiveEventHandler(new MessageShot() { point = new Point(0, 0) } as BaseMessage);
            //    ReceiveEventHandler(new MessageShot() { point = new Point(0, 1) } as BaseMessage);
            Network.Send(new MessageShot() { point = sender.Position } as BaseMessage);
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
                Network.Send(answer as BaseMessage);
            }
            else if (message is MessageResultShot)//результат выстрела по противнику
            {
                MessageResultShot resultShot = message as MessageResultShot;

                enemyField.Items[(int)resultShot.point.X, (int)resultShot.point.Y].Status = resultShot.pairPointShip.Key;

                if (resultShot.pairPointShip.Key != PointStatus.killed)
                {

                    var Item = enemyField.Items[(int)resultShot.point.X, (int)resultShot.point.Y];
                    Application.Current.Dispatcher.BeginInvoke
                         (new ThreadStart(() => Item.SetImg()));
                    //Dispatcher.BeginInvoke
                    //    (new ThreadStart(() => Item.SetImg()));

                    enemyField.Items[(int)resultShot.point.X, (int)resultShot.point.Y].SetImg();
                }
                else
                {
                    resultShot.pairPointShip.Value.Killed(enemyField);
                    resultShot.pairPointShip.Value.turnOffItem(enemyField);
                    enemyField.AddShip(resultShot.pairPointShip.Value);
                    checkWin();
                }
            }
        }
    }
}
