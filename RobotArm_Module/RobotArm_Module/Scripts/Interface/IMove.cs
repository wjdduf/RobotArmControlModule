using System;

namespace RobotArm_Module
{
    public interface IMove
    {
        int MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None);
        int JointRotation(float angle, eJointType type = eJointType.None);
        int MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position, bool isLinear = false, Action onComplete = null);

        void MoveToPosition(float speed, eDirection direction);
        void MoveToRotation(float speed, eRotationAxis axis);

        void PlayPreset(WorkPreset preset, Action onComplete = null);
        void PlayRobotWork(string name);

        int SetPivot(Vector3 pivot);
        int Stop();
        int MoveLoop(Vector3 fromPos, Vector3 fromRot, Vector3 toPos, Vector3 toRot, bool isLoop, float loopTime, Action onComplete = null, eMoveType moveType = eMoveType.Position);
    }
}
