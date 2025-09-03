using System;

namespace RobotArm_Module
{
	public class RainbowRoboticsArm : RobotArm
	{
		public RainbowRoboticsArm()
		{
		}

        public override void Connect(string ip, string port)
        {
            //연결 관련 코드 구현
            Debug.Log($"Rainbow ip - {ip} :: port - {port}");
        }

        public override void MoveToPosition(Vector3 position, eJointType type = eJointType.None)
        {
            Debug.Log($"Rainbow position - {position} :: type - {type}");

        }

        public override void MoveToRotation(Vector3 rotation, eJointType type = eJointType.None)
        {
            Debug.Log($"Rainbow rotation - {rotation} :: type - {type}");

        }
    }
}
