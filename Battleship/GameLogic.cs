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
    class GameLogic
    {
        UniformGrid grdMy;
        UniformGrid grdEnemy;
        Grid grdPlacement;

        Field myField;
        Field enemyField;
        ShipPlacement placement;
        Battle battle;

        State state;

        public GameLogic(UniformGrid my, UniformGrid enemy, Grid grdPl)
        {
            myField = new Field();
            enemyField = new Field();
            grdMy = my;
            grdEnemy = enemy;
            grdPlacement = grdPl;
            placement = new ShipPlacement(myField);
            placement.EndPlacementEvent += endPlacement;

        }

        public void SetState(State st)
        {
            state = st;
        }

        public void FieldsCreate(Item[,] arrMy, Item[,] arrEnemy)
        {
            myField.Create(arrMy);
            enemyField.Create(arrEnemy);
        }

        public void InitShipPlacement()
        {
            List<Button> buttons = grdPlacement.Children.OfType<Button>().ToList();

            placement.InitShips(buttons[0], buttons[1], buttons[2], buttons[3], grdPlacement); // возможно это как-то переделать
        }

        public void ShowPlacement()
        {
            grdMy.Visibility = Visibility.Visible;
            grdPlacement.Visibility = Visibility.Visible;
        }

        public void clickButtonPlacement(Button sender, RoutedEventArgs e)
        {
            placement.clickButton(sender, e);
        }
        
        public void clickItemPlacement(Item sender, RoutedEventArgs e)
        {
            sender.Focusable = false;
            placement.clickItem(sender, e);
        }

        public void InitBattle()
        {
            battle = new Battle(myField, enemyField, grdMy, grdEnemy); // возможно сделать по-другому
        }

        public void ShowBattle()
        {
            battle.Show();
        }
        public void clickButtonBattle(Button sender, RoutedEventArgs e)
        {
            battle.clickButton(sender, e);
        }
        public void clickItemBattle(Item sender, RoutedEventArgs e)
        {
            battle.clickItem(sender, e);
        }


        private void changeState(State st)
        {
            state = st;
        }
        public void Init()
        {
            state.Init();
        }
        public void Show()
        {
            state.Show();
        }
        public void clickButton(Button sender, RoutedEventArgs e)
        {
            state.clickButtonHandler(sender, e);
        }
        public void clickItem(Item sender, RoutedEventArgs e)
        {
            state.clickItemHandler(sender, e);
        }
        public void endPlacement() // возможно общий обработчик для всех состояний
        {
            grdPlacement.Visibility = Visibility.Hidden; // мб это отдельный метод окончания состояния

            changeState(new StateBattle(this));
            Start();
        }
        public void Start()
        {
            state.Start();
        }
    }
}

