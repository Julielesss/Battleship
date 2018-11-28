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
    enum Position
    {
        Horizontal, 
        Vertical
    };

    class ShipPlacement //содержит информациб о выбранном корабле, для того чтобы его выставить на поле
    {
        ShipsTypes selectedShip;
        Position selectedPosition;
        List<ShipPlacementInfo> ships;
        Field myField;

        public ShipsTypes SelectedShip => selectedShip;
        public Position SelectedPosition => selectedPosition;


        public ShipPlacement(Field field)
        {
            ships = new List<ShipPlacementInfo>();
            selectedShip = new ShipsTypes(4, 1);
            selectedPosition = Position.Horizontal;
            myField = field;
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

        public bool ProveState(Point p)
        { //проверка, можем ли мы поставить корабль в выбранное место
            if (SelectedPosition == Position.Horizontal)
            {
                if (10 - p.X + 1 >= SelectedShip.QuantityDeck)
                {
                    for (int i = 0; i < SelectedShip.QuantityDeck; i++)
                    {
                        if (!myField.Items[(int)p.Y, (int)p.X + i].IsEnabled) // здесь вылетаем
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else return false;
            }
            else
                if (10 - p.Y + 1 >= SelectedShip.QuantityDeck)
            {
                for (int i = 0; i < SelectedShip.QuantityDeck; i++)
                {
                    if (!myField.Items[(int)p.Y + i, (int)p.X].IsEnabled)
                    {
                        return false;
                    }
                }
                return true;
            }
            else return false;
        }

        public void btnTurn_Click(Button sender)
        {
            if (selectedPosition == Position.Horizontal)
                selectedPosition = Position.Vertical;
            else
                selectedPosition = Position.Horizontal;
        }

        public void clickItem(Item sender)
        {
            if (ProveState(sender.Position))
            {
               // sender.IsEnabled = false;
                sender.Background = Brushes.Aqua;
                sender.Focusable = false; 
            }
        }
    }
}
