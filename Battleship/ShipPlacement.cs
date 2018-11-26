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
        ShipsTypes typeShip;
        int countShips;
        Button btnChoiceShip;

        public ShipPlacementInfo(ShipsTypes typeShip, Button btnChoiceShip)
        {
            this.typeShip = typeShip;
            this.countShips = (int)typeShip;
            this.btnChoiceShip = btnChoiceShip;
        }
    }


    class ShipPlacement
    {
        ShipsTypes selectedShip;
        Position selectedPosition;
        List<ShipPlacementInfo> ships;

        public ShipPlacement()
        {
            ships = new List<ShipPlacementInfo>();
            selectedShip = ShipsTypes.FourDeck;
            selectedPosition = Position.Horizontal;
        }

        public void InitShips(Button one, Button two, Button three, Button four)
        {
            ships.Add(new ShipPlacementInfo(ShipsTypes.OneDeck, one));
            ships.Add(new ShipPlacementInfo(ShipsTypes.TwoDeck, two));
            ships.Add(new ShipPlacementInfo(ShipsTypes.ThreeDeck, three));
            ships.Add(new ShipPlacementInfo(ShipsTypes.FourDeck, four));
        }

        public void ChangePosition()
        {
            if (selectedPosition == Position.Horizontal)
                selectedPosition = Position.Vertical;
            else
                selectedPosition = Position.Horizontal;
        }



    }
}
