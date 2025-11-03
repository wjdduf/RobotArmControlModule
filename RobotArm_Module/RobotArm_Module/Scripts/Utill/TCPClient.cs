using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public enum ePortType
    {
        Control, // UR Primary, RB Socket
        Dashboard
    }
    public abstract class TCPClient
    {
        protected Action onComplete = null;

        public abstract bool Connect(string ip);
        public abstract bool DisConnect();
        public abstract void SendPacket(string message, ePortType portType = ePortType.Control, bool useLog = true);
        public abstract string Receive(ePortType portType = ePortType.Control, bool useLog = true);

        public abstract void SendPacketWait(string send, string waitText);
    }
}
