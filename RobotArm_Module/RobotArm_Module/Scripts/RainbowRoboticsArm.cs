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

        public override bool Connect(string ip, Action onComplete = null)
        {
            //연결 관련 코드 구현
            Debug.Log($"Rainbow ip - {ip}");
            return true;
        }

        public override bool DisConnect()
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

        public override int Grip(Action onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override int Homming()
        {
            throw new NotImplementedException();
        }

        public override int JointRotation(float angle, eJointType type = eJointType.None)
        {
            throw new NotImplementedException();
        }

        public override bool MonitorConnection()
        {
            throw new NotImplementedException();
        }

        public override int MoveLoop(Vector3 fromPos, Vector3 fromRot, Vector3 toPos, Vector3 toRot, bool isLoop, float loopTime, Action onComplete = null, eMoveType moveType = eMoveType.Position)
        {
            throw new NotImplementedException();
        }

        public override int MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None)
        {
            throw new NotImplementedException();
        }

        public override void MoveToPosition(float speed, eDirection direction)
        {
            throw new NotImplementedException();
        }

        public override int MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position,bool isLinear = false, Action onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void MoveToRotation(float speed, eRotationAxis axis)
        {
            throw new NotImplementedException();
        }

        public override int PlayCSV(List<CSVData> data)
        {
            throw new NotImplementedException();
        }

        public override void PlayPreset(WorkPreset Preset, Action onComplete)
        {
            throw new NotImplementedException();
        }

        public override int Release(Action onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override int SetPivot(Vector3 pivot)
        {
            throw new NotImplementedException();
        }

        public override void ShutDown()
        {
            throw new NotImplementedException();
        }

        public override int Stop()
        {
            throw new NotImplementedException();
        }

        public override void TestCode(string script)
        {
            throw new NotImplementedException();
        }

        public override int UnlockProtectiveStop()
        {
            throw new NotImplementedException();
        }
    }
}
