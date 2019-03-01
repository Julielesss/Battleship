using System;
using System.Collections.Generic;
using System.Windows;

namespace Battleship
{ 
    [Serializable]
    enum PointStatus
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
    }

}
