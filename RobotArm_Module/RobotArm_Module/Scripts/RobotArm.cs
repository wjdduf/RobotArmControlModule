using System;

namespace RobotArm_Module
{
    public abstract class RobotArm : IMove, IConnect
    {
        public abstract void Connect(string ip, string port);
        public abstract void DisConnect();
        public abstract void MoveToPosition(Vector3 position, eJointType type = eJointType.None);
        public abstract void MoveToRotation(Vector3 rotation, eJointType type = eJointType.None);
    }
}
