using System;

namespace RobotArm_Module
{
    public interface IConnect
    {
        bool Connect(string ip, Action onComplete = null);
        bool DisConnect();

        void ShutDown();
    }
}
