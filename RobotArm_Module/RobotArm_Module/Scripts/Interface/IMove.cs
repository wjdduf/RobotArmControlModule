using System;

namespace RobotArm_Module
{
    public interface IMove
    {
        void MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None);
        void JointRotation(float angle, eJointType type = eJointType.None);
        void MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position, Action onComplete = null);

        void MoveToPosition(float speed, eDirection direction);
        void MoveToRotation(float speed, eRotationAxis axis);

        void PlayPreset(WorkPreset preset, Action onComplete = null);
        void PlayRobotWork(string name);

        void SetPivot(Vector3 pivot);
        void Stop();
    }
}
