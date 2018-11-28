using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Battleship
{
    public struct ShipsTypes //сколько палуб выбранного корабля, и сколько таких кораблей осталось
    {
        int quantityDeck;
        int quantityShip;

        public ShipsTypes(int deck, int ship) : this()
        {
            quantityDeck = deck;
            quantityShip = ship;
        }

        public int QuantityDeck => quantityDeck;
        public int QuantityShip => quantityShip;

    };


    class GameLogic
    {
        UniformGrid grdMy;
        UniformGrid grdEnemy;

        Field myField;
        Field enemyField;
        ShipPlacement placement;

        public GameLogic(UniformGrid my, UniformGrid enemy)
        {
            myField = new Field();
            enemyField = new Field();
            grdMy = my;
            grdEnemy = enemy;
            placement = new ShipPlacement();

        }

        public void FieldsCreate()
        {
            myField.Create(grdMy);
            //enemyField.Create(gbxMy);

        }

        public void InitShipPlacement(Grid grd)
        {
            List<Button> buttons = grd.Children.OfType<Button>().ToList();
            
            placement.InitShips(buttons[0], buttons[1], buttons[2], buttons[3]); // возможно это как-то переделать
        }

        public void clickSelectShipType(Button sender)
        {
            placement.clickSelectShip(sender);
        }


         public bool ProveState(Point p, ShipPlacement sh) { //проверка, можем ли мы поставить корабль в выбранное место
            if (sh.SelectedPosition == Position.Horizontal)
            {
                if (10 - p.X + 1 >= sh.SelectedShip.QuantityDeck)
                    return true;
                else return false;
            }
            else
                if (10 - p.Y + 1 >= sh.SelectedShip.QuantityDeck)
                    return true;
                else return false;
         }
    }
}
