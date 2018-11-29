using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Battleship
{
    enum Position
    {
        Horizontal, 
        Vertical
    };

    class ShipPlacement //содержит информацию о выбранном корабле, чтобы его выставить на поле
    {
        ShipPlacementInfo selectedShip;
        Position selectedPosition;
        List<ShipPlacementInfo> ships;
        Field myField;
        Grid grdButtonsPlacement;


        public ShipPlacement(Field field)
        {
            ships = new List<ShipPlacementInfo>();
            selectedPosition = Position.Horizontal;
            myField = field;
        }

        public void InitShips(Button one, Button two, Button three, Button four, Grid gr)
        {
            ships.Add(new ShipPlacementInfo(new ShipsTypes (1, 4) , one));
            ships.Add(new ShipPlacementInfo(new ShipsTypes(2, 3), two));
            ships.Add(new ShipPlacementInfo(new ShipsTypes(3, 2), three));
            ships.Add(new ShipPlacementInfo(new ShipsTypes(4, 1), four));
            grdButtonsPlacement = gr;

            //selectedShip = ships.Last();
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
                    selectedShip = shipInfo;
                    ShowImageSelectedShip(); // показ нужного изображения 
                    return;
                }
            }
        }

        public bool ProveState(Point p)   //проверка, можем ли мы поставить корабль в выбранное место
        { 
            if (selectedShip == null)
                return false;

            if (selectedPosition == Position.Horizontal)
            {
                if (10 - p.X + 1 >= selectedShip.typeShip.QuantityDeck)
                {
                    for (int i = 0; i < selectedShip.typeShip.QuantityDeck; i++)
                    {
                        if (!myField.Items[(int)p.Y-1, (int)p.X + i-1].IsEnabled)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                    return false;
            }

            else if (10 - p.Y + 1 >= selectedShip.typeShip.QuantityDeck)
            {
                for (int i = 0; i < selectedShip.typeShip.QuantityDeck; i++)
                {
                    if (!myField.Items[(int)p.Y + i-1, (int)p.X-1].IsEnabled)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
                return false;
        }

        public void clickItem(Item sender)
        {
            if (ProveState(sender.Position))
            {
                Ship currentShip = new Ship();

                for (int i = 0; i < selectedShip.typeShip.QuantityDeck; i++)
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "green.jpg")); // картинка, временное дебильное решение
                    Item temp;

                    if (selectedPosition == Position.Horizontal)
                        temp = myField.Items[(int)sender.Position.Y - 1, (int)sender.Position.X + i - 1];
                    else
                       temp = myField.Items[(int)sender.Position.Y - 1 + i, (int)sender.Position.X - 1];

                    temp.Content = img;
                    temp.IsEnabled = false;
                    currentShip.AddPoint(temp.Position);
                }

                Place(currentShip);
            }

        }

        public void Place(Ship sh)
        {
            myField.AddShip(sh);

            if (selectedShip.Placed())
            {
                ships.Remove(selectedShip);
                selectedShip = null; // может выбирать другой корабль?
                ShowImageSelectedShip();
            }

            if (ships.Count() == 0) 
                grdButtonsPlacement.Visibility = Visibility.Hidden;  // это наверное должно быть в отдельном методе 
        }

        public void btnTurn_Click()
        {
            if (selectedPosition == Position.Horizontal)
                selectedPosition = Position.Vertical;
            else
                selectedPosition = Position.Horizontal;

            ShowImageSelectedShip();
        }

        public void ShowImageSelectedShip()
        {
            Image img = grdButtonsPlacement.Children.OfType<Image>().First();
            img.Source = null;
            if (selectedShip == null)
                return;

            if (selectedPosition == Position.Vertical)
                img.Source = (ImageSource)Application.Current.Resources[selectedShip.imgPath + "V"];
            else
                img.Source = (ImageSource)Application.Current.Resources[selectedShip.imgPath];
        }

    }
}
