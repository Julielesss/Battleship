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
    class ShipsTypes //сколько палуб выбранного корабля, и сколько таких кораблей осталось
    {
        int quantityDeck;
        int quantityShip;

        public ShipsTypes(int deck, int ship)
        {
            quantityDeck = deck;
            quantityShip = ship;
        }

        public int QuantityDeck => quantityDeck;
        public int QuantityShip => quantityShip;
        public void Placed()
        {
            --quantityShip;
        }

    }


    class GameLogic
    {
        UniformGrid grdMy;
        UniformGrid grdEnemy;
        Grid grdPlacement;

        Field myField;
        Field enemyField;
        ShipPlacement placement;

        IState state;

        public GameLogic(UniformGrid my, UniformGrid enemy, Grid grdPl, IState st)
        {
            myField = new Field();
            enemyField = new Field();
            grdMy = my;
            grdEnemy = enemy;
            grdPlacement = grdPl;
            placement = new ShipPlacement(myField);
            state = st;
        }

        public void FieldsCreate(Item[,] arr)
        {
            myField.Create(arr);
        }

        public void InitShipPlacement()
        {
            List<Button> buttons = grdPlacement.Children.OfType<Button>().ToList();

            placement.InitShips(buttons[0], buttons[1], buttons[2], buttons[3], grdPlacement); // возможно это как-то переделать
        }

        public void clickButton(Button sender)
        {
            placement.clickButton(sender);
        }

        public void clickItem(Item sender)
        {
            // это должен быть метод стейт машины, по разному реагирующий на клик в зависимости от стадии игры
            //реализация для стадии расстановки

            sender.Focusable = false;
            placement.clickItem(sender);
        }

        public void ShowPlacement()
        {
            grdMy.Visibility = Visibility.Visible;

        }
        
    }
}

