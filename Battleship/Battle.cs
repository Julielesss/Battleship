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
        TextBlock txblInfo;


        public Battle(Field my, Field enemy, UniformGrid myGrid, UniformGrid enemyGrid, TextBlock tblInfo)
        {
            myField = my;
            enemyField = enemy;
            grdMy = myGrid;
            grdEnemy = enemyGrid;
            txblInfo = tblInfo;
            Network.ReceiveMessageEvent += ReceiveEventHandler;
        }


        public void Show()
        {
            grdMy.IsEnabled = false;
            grdMy.Visibility = grdEnemy.Visibility = Visibility.Visible;
                Application.Current.Dispatcher.BeginInvoke
                                     (new ThreadStart(() => changeStep(Network.isServer())));

        }

        public void clickButton(Button sender, RoutedEventArgs e)
        {

        }
        public void clickItem(Item sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke
                (new ThreadStart(() => { changeStep(false); sender.Focusable = sender.IsEnabled = false; }));

           // Thread.Sleep(500);
            Network.Send(new MessageShot() { point = sender.Position } as BaseMessage);

        }


        private void checkWin()
        {
            if (enemyField.checkShipsCount() == 10) 
                win();
        }

        private void win()
        {
            Network.Win();
            MessageBox.Show("Вы выиграли", "Поздравлямба", MessageBoxButton.OK);
            Application.Current.Dispatcher.BeginInvoke
                (new ThreadStart(() => Application.Current.Shutdown()));
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
                    Application.Current.Dispatcher.BeginInvoke
                                            (new ThreadStart(() => changeStep(!checkAble(pairResult.Key))));
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
                }
                else
                {
                    resultShot.pairPointShip.Value.Killed(enemyField);
                    resultShot.pairPointShip.Value.turnOffItem(enemyField);
                    enemyField.AddShip(resultShot.pairPointShip.Value);
                    checkWin();
                }
                
                    Application.Current.Dispatcher.BeginInvoke
                        (new ThreadStart(() => changeStep(checkAble(resultShot.pairPointShip.Key))));
            }
        }

        public bool checkAble(PointStatus status) {
            if (status == PointStatus.past)
                return false;
            else return true;

        }

        private void changeStep(bool flag)
        {
            grdEnemy.IsEnabled = flag;
            if (flag)
                txblInfo.Text = "Ваш ход";
            else
                txblInfo.Text = "Ход соперника";
        }

        public void DisconnectHandler()
        {
            changeStep(false);
        }
    }
}
