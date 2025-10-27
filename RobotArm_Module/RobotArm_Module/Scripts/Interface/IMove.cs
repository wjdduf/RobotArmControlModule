using System;

namespace RobotArm_Module
{
    public interface IMove
    {
        void MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None);
        void JointRotation(float angle, eJointType type = eJointType.None, Action<bool> onComplete = null);
        void MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position, bool isLinear = false, Action<bool> onComplete = null);

        void MoveToPosition(float speed, eDirection direction);
        void MoveToRotation(float speed, eRotationAxis axis);

        void PlayPreset(WorkPreset preset, Action<bool> onComplete = null);
        void PlayRobotWork(string name, Action<bool> onComplete = null);

        void SetPivot(Vector3 pivot, Action<bool> onComplete = null);
        void Stop(Action<bool> onComplete = null);
        void MoveLoop(Vector3 fromPos, Vector3 fromRot, Vector3 toPos, Vector3 toRot, bool isLoop, float loopTime, Action<bool> onComplete = null, eMoveType moveType = eMoveType.Position);
    }
}
