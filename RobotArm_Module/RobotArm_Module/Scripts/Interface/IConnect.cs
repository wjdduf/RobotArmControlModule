using System;

namespace RobotArm_Module
{
    public interface IConnect
    {
        bool Connect(string ip, Action<bool> onComplete = null);
        bool DisConnect(Action<bool> onComplete = null);

        void ShutDown();
    }
}
