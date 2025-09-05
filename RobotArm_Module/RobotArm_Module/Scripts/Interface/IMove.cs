using System;

namespace RobotArm_Module
{
    public interface IMove
    {
        void MoveToPosition(Vector3 position, eJointType type = eJointType.None);
        void MoveToRotation(Vector3 rotation, eJointType type = eJointType.None);

        void MoveToPreset(Vector3 position, Vector3 rotation);
    }
}
