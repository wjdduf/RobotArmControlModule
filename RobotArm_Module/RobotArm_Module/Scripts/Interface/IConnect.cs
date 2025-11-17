using System;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public interface IConnect
    {
        bool Connect(string ip, Action<bool> onComplete = null);

        Task<bool> AutoConnect(string ip, Action<bool> onComplete = null);
        bool DisConnect(Action<bool> onComplete = null);

        void ShutDown();
    }
}
