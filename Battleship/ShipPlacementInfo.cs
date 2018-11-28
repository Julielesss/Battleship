using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Battleship
{
    class ShipPlacementInfo
    {
        public ShipsTypes typeShip { get; }
        Button btnChoiceShip;


        public ShipPlacementInfo(ShipsTypes typeShip, Button btnChoiceShip)
        {
            this.typeShip = typeShip;
            this.btnChoiceShip = btnChoiceShip;
        }

        public bool checkButton(Button btn)
        {
            if (btn == btnChoiceShip)
                return true;
            return false;
        }

        public void checkCount()
        {
            if (typeShip.QuantityShip == 0)
                btnChoiceShip.IsEnabled = false;
        }
    }

}
