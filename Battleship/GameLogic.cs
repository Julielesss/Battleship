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
    public enum ShipsTypes
    {
        OneDeck = 4,
        TwoDeck = 3,
        ThreeDeck = 2,
        FourDeck = 1
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


        // public void ProveState() //Настя
    }
}
