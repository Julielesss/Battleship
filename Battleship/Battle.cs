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

        }

        public void checkAnswer() //проверяем сообщения
        {

        }

        public void pastItem(Point p)
        {
            Image img = new Image();
            img.Source = (ImageSource)Application.Current.Resources["pastSquare"];
            myField.Items[(int)p.X, (int)p.Y].Content = img;

        }

        public void wundedShip(Point p)//при ранении
        {

            Image img = new Image();
            img.Source = (ImageSource)Application.Current.Resources["yellowSquare"];
            myField.Items[(int)p.X, (int)p.Y].Content = img;
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
            if (message is MessageShot)
            {
                MessageShot shot = (MessageShot)message;
                
            }
        }
    }
}
