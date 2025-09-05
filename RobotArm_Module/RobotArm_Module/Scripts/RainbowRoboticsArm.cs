using System;

namespace RobotArm_Module
{
	public class RainbowRoboticsArm : RobotArm
	{
		public RainbowRoboticsArm()
		{
		}

        public override bool Connect(string ip)
        {
            //연결 관련 코드 구현
            Debug.Log($"Rainbow ip - {ip}");
            return true;
        }

        public override bool DisConnect()
        {
            throw new NotImplementedException();
        }

        public override void MoveToPosition(Vector3 position, eJointType type = eJointType.None)
        {
            Debug.Log($"Rainbow position - {position} :: type - {type}");

        }

        public override void MoveToPreset(Vector3 position, Vector3 rotation)
        {
            throw new NotImplementedException();
        }

        public override void MoveToRotation(Vector3 rotation, eJointType type = eJointType.None)
        {
            Debug.Log($"Rainbow rotation - {rotation} :: type - {type}");

        }

        public override void ShutDown()
        {
            throw new NotImplementedException();
        }
    }
}
