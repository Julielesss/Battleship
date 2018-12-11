using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

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
        TextBlock txblInfo;
        State state;

        event Action DisconnectEvent;

        public GameLogic(UniformGrid my, UniformGrid enemy, Grid grdPl, TextBlock tblInfo)
        {
            myField = new Field();
            enemyField = new Field();
            grdMy = my;
            grdEnemy = enemy;
            grdPlacement = grdPl;
            txblInfo = tblInfo;
            placement = new ShipPlacement(myField);
            placement.ImReadyEvent += Ready;
            Network.ReceiveStatusEvent += ReceiveStatusHandler;
        }

        public void SetMyName(string name)
        {
            myField.Name = name;
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
            txblInfo.Text = "Расставьте корабли";
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
            battle = new Battle(myField, enemyField, grdMy, grdEnemy, txblInfo);
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
        public void Start()
        {
            state.Start();
        }

        public void ReceiveStatusHandler(BaseMessage message)
        {
            MessageGameStatus status = message as MessageGameStatus;
            if (status.Status == GameStatus.Disconnect)
            {
                DisconnectEvent?.Invoke();
                MessageBox.Show("Ваш оппонент вышел из игры", "Сорямба", MessageBoxButton.OK);
                Application.Current.Dispatcher.BeginInvoke
                     (new ThreadStart(() => Application.Current.Shutdown()));
            }
            else if (status.Status == GameStatus.Ready)
            {
                placement.opponentReady = true;
                Application.Current.Dispatcher.BeginInvoke
                      (new ThreadStart(() => txblInfo.Text = "Ваш оппонент готов к игре!"));
                if (placement.imReady)
                    endPlacement();

            }
            else if (status.Status == GameStatus.Win)
            {

            }
        }

        public void Ready()
        {
            Network.Ready();
            if (placement.opponentReady)
                endPlacement();
            else
                Application.Current.Dispatcher.BeginInvoke
                        (new ThreadStart(() => txblInfo.Text = "Ждем, пока оппонент будет готов"));
        }

        private void endPlacement()
        {
            grdPlacement.Visibility = Visibility.Hidden; // мб это отдельный метод окончания состояния

            changeState(new StateBattle(this));
            Start();
        }

    }

}

