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
    class ShipPlacementInfo
    {
        public ShipsTypes typeShip { get; } // нужна ли такая вложенность? Может просто хранить поля из структуры здесь? будет ли повторное использование структуры 
        Button btnChoiceShip;
        public string imgPath { get; private set; }


        public ShipPlacementInfo(ShipsTypes typeShip, Button btnChoiceShip)
        {
            this.typeShip = typeShip;
            this.btnChoiceShip = btnChoiceShip;
            setImg();
        }

        public bool checkButton(Button btn)
        {
            if (btn == btnChoiceShip)
                return true;
            return false;
        }

        public bool checkCount()
        {
            if (typeShip.QuantityShip == 0)
            {
                btnChoiceShip.Visibility = Visibility.Collapsed;
                return true;
            }
            return false;
        }
        public bool Placed()
        {
            typeShip.Placed(); // если заменить класс на структуру, значение останется прежним
            return checkCount();
        }

        private void setImg()
        {
            imgPath = typeShip.QuantityDeck + "Deck";
        }
    }

}
