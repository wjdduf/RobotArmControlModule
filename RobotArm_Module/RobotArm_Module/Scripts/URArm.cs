using System;

namespace RobotArm_Module
{
    public class URArm : RobotArm
    {
        public override void Connect(string ip, string port)
        {
            //연결 관련 코드 구현
            Debug.Log($"URArm ip - {ip} :: port - {port}");
        }

        public override void MoveToPosition(Vector3 position, eJointType type = eJointType.None)
        {
            Debug.Log($"URArm position - {position} :: type - {type}");

        }

        public override void MoveToRotation(Vector3 rotation, eJointType type = eJointType.None)
        {
            Debug.Log($"URArm rotation - {rotation} :: type - {type}");

        }
    }
}
