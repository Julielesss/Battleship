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


    class ShipPlacement
    {
        ShipPlacementInfo selectedShip;
        Position selectedPosition;
        List<ShipPlacementInfo> ships;
        Field myField;
        Grid grdButtonsPlacement;

        public event Action EndPlacementEvent;

        public ShipPlacement(Field field)
        {
            ships = new List<ShipPlacementInfo>();
            selectedPosition = Position.Horizontal;
            myField = field;
        }

        public void InitShips(Button one, Button two, Button three, Button four, Grid gr)
        {
            ships.Add(new ShipPlacementInfo(new ShipsTypes(1, 4), one));
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

        private void clickSelectShip(Button btn)
        {
            foreach (var shipInfo in ships)
            {
                if (shipInfo.checkButton(btn))
                {
                    selectedShip = shipInfo;
                    ShowImageSelectedShip();  // показ нужного изображения 
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
                if (10 - p.X >= selectedShip.typeShip.QuantityDeck)
                {
                    for (int i = 0; i < selectedShip.typeShip.QuantityDeck; i++)
                    {
                        if (!myField.Items[(int)p.Y , (int)p.X + i ].IsEnabled)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                    return false;
            }

            else if (10 - p.Y >= selectedShip.typeShip.QuantityDeck)
            {
                for (int i = 0; i < selectedShip.typeShip.QuantityDeck; i++)
                {
                    if (!myField.Items[(int)p.Y + i , (int)p.X ].IsEnabled)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
                return false;
        }


        private void SetShip(Item sender)
        {
            Ship currentShip = new Ship();

            for (int i = 0; i < selectedShip.typeShip.QuantityDeck; i++)
            {
                Image img = new Image();
                img.Source = (ImageSource)Application.Current.Resources["greenSquare"]; // картинка, временное дебильное решение
                Item temp;

                if (selectedPosition == Position.Horizontal)
                    temp = myField.Items[(int)sender.Position.Y , (int)sender.Position.X + i ];
                else
                    temp = myField.Items[(int)sender.Position.Y  + i, (int)sender.Position.X ];

                temp.Content = img;
                temp.IsEnabled = false;
                temp.ship = currentShip; // ПРОВЕРИТЬ
                currentShip.AddPoint(temp.Position);
            }

            turnOffItem(currentShip);

            Place(currentShip);
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
            {
                if (EndPlacementEvent != null)
                    EndPlacementEvent.Invoke();
            }
        }

        private void btnTurn_Click()
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

        public void clickButton(Button sender, RoutedEventArgs e)
        {
            if (sender.Tag.ToString() == "Turn")
                btnTurn_Click();
            else
                clickSelectShip(sender);
        }

        public void clickItem(Item sender, RoutedEventArgs e)
        {
            if (ProveState(sender.Position))
                SetShip(sender);
        }

        public void turnOffItem(Ship sh)
        {
            int[,] temp = { { 0, -1 }, { 0, 1 }, { -1, 0 }, { 1, 0 }, { 1, 1 }, { -1, -1 }, { -1, 1 }, { 1, -1 } };
            for (int i = 0; i < selectedShip.typeShip.QuantityDeck; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    int X1 = (int)sh.Cells[i].X  + temp[j, 0];
                    int Y1 = (int)sh.Cells[i].Y  + temp[j, 1];

                    if (Y1 >= 0 && Y1 < 10 && X1>= 0 && X1 < 10)
                        myField.Items[Y1, X1].IsEnabled = false;
                }
            }
        }


    }
}