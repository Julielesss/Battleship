using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Battleship
{
    enum Position
    {
        Horizontal, 
        Vertical
    };

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


    class ShipPlacement //содержит информациб о выбранном корабле, для того чтобы его выставить на поле
    {
        ShipsTypes selectedShip;
        Position selectedPosition;
        List<ShipPlacementInfo> ships;

        public ShipsTypes SelectedShip => selectedShip;
        public Position SelectedPosition => selectedPosition;


        public ShipPlacement()
        {
            ships = new List<ShipPlacementInfo>();
            selectedShip = new ShipsTypes(4, 1);
            selectedPosition = Position.Horizontal;
        }

        public void InitShips(Button one, Button two, Button three, Button four)
        {
            ships.Add(new ShipPlacementInfo(new ShipsTypes (1, 4) , one));
            ships.Add(new ShipPlacementInfo(new ShipsTypes(2, 3), two));
            ships.Add(new ShipPlacementInfo(new ShipsTypes(3, 2), three));
            ships.Add(new ShipPlacementInfo(new ShipsTypes(4, 1), four));
        }

        public void ChangePosition()
        {
            if (selectedPosition == Position.Horizontal)
                selectedPosition = Position.Vertical;
            else
                selectedPosition = Position.Horizontal;
        }

        public void clickSelectShip(Button btn)
        {
            foreach(var shipInfo in ships)
            {
                if (shipInfo.checkButton(btn))
                {
                    selectedShip = shipInfo.typeShip;
                    // показ нужного изображения 
                    return;
                }
            }
        }

    }
}
