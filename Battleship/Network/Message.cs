using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace Battleship
{ 
    [Serializable]
    enum PointStatus // это наверное должно быть в классе айтема
    {
        past,
        wounded,
        killed
    }

    [Serializable]
    enum GameStatus
    {
        Ready,
        Disconnect, 
        Win
    };

    [Serializable]
    public class BaseMessage
    {   }

    [Serializable]
    class MessageGameStatus : BaseMessage
    {
        public GameStatus Status {get; set;}
    }

    [Serializable]
    class MessageShot:BaseMessage
    {
        public Point point { get; set; }
    }

    [Serializable]
    class MessageResultShot:BaseMessage
    {
        public Point point { get; set; }
        public KeyValuePair<PointStatus, Ship> pairPointShip { get; set; }
        //public PointStatus pointStatus { get; set; }
        //public Ship ship { get; set; }
    }
}
