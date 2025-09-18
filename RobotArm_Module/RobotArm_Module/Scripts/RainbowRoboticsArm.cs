using System;

namespace RobotArm_Module
{
	public class RainbowRoboticsArm : RobotArm
	{
		public RainbowRoboticsArm()
		{
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

        public override void JointRotation(float angle, eJointType type = eJointType.None)
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

        public override void MoveToPreset(Vector3 position, Vector3 rotation)
        {
            throw new NotImplementedException();
        }

        public override void MoveToRotation(float speed, eRotationAxis axis)
        {
            throw new NotImplementedException();
        }

        public override void PlayPreset(Action onComplete)
        {
            throw new NotImplementedException();
        }

        public override void SetPivot(Vector3 pivot)
        {
            throw new NotImplementedException();
        }

        public override void ShutDown()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override void TestCode(string script)
        {
            throw new NotImplementedException();
        }
    }
}
