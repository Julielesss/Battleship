using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Battleship
{
    enum ShipsTypes
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

        public GameLogic(UniformGrid my, UniformGrid enemy)
        {
            myField = new Field();
            enemyField = new Field();
            grdMy = my;
            grdEnemy = enemy;

        }

        public void FieldsCreate()
        {
            myField.Create(grdMy);
            //enemyField.Create(gbxMy);

        }

        public void ProveState() //Настя
 } 
}
