using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;


namespace ClientServer
{ 
    [Serializable]
    enum PointStatus // это наверное должно быть в классе айтема
    {
        //мимо
        //ранен
        //убит
    }


    [Serializable]
    class BaseMessage
    {
        

    }

    [Serializable]
    class MessageReady
    {}

    [Serializable]
    class MessageShot:BaseMessage
    {
        public Point point { get; set; }
        // Ship;

    }

    [Serializable]
    class MessageResultShot:BaseMessage
    {
        public Point point { get; set; }
        public PointStatus pointStatus { get; set; }
    }
}
