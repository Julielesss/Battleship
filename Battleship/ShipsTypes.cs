using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
