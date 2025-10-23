using System;
using System.Collections.Generic;

namespace RobotArm_Module
{
	public class RainbowRoboticsArm : RobotArm
	{
		public RainbowRoboticsArm()
		{
		}

        public override void AddWorkQueue(Vector3 pos, Vector3 rot, eMoveType moveType = eMoveType.Position)
        {
            throw new NotImplementedException();
        }

        public override void AddWorkQueue(PresetData[] preset)
        {
            throw new NotImplementedException();
        }

        public override bool Connect(string ip, Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override bool DisConnect(Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void EmergencyStop()
        {
            throw new NotImplementedException();
        }

        public override bool GetSafetyMode()
        {
            throw new NotImplementedException();
        }

        public override void Grip(Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void Homming(Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void JointRotation(float angle, eJointType type = eJointType.None)
        {
            throw new NotImplementedException();
        }

        public override bool MonitorConnection()
        {
            throw new NotImplementedException();
        }

        public override void MoveLoop(Vector3 fromPos, Vector3 fromRot, Vector3 toPos, Vector3 toRot, bool isLoop, float loopTime, Action<bool> onComplete = null, eMoveType moveType = eMoveType.Position)
        {
            throw new NotImplementedException();
        }

        public override void MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None)
        {
            throw new NotImplementedException();
        }

        public override void MoveToPosition(float speed, eDirection direction)
        {
            throw new NotImplementedException();
        }

        public override void MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position, bool isLinear = false, Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void MoveToRotation(float speed, eRotationAxis axis)
        {
            throw new NotImplementedException();
        }

        public override void PlayCSV(List<CSVData> data, Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void PlayPreset(WorkPreset preset, Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void Release(Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void SetPivot(Vector3 pivot, Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void ShutDown()
        {
            throw new NotImplementedException();
        }

        public override void Stop(Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void TestCode(string script)
        {
            throw new NotImplementedException();
        }

        public override void UnlockProtectiveStop(Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }
    }
}
