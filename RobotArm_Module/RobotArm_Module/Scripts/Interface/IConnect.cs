using System;

namespace RobotArm_Module
{
    public interface IConnect
    {
        bool Connect(string ip);
        bool DisConnect();

        void ShutDown();
    }
}
