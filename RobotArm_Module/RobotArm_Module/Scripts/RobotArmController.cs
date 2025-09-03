using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public class RobotArmController
    {
        private RobotArmBuilder RobotArmBuilder;

        private IConnect IConnect;
        private IMove IMove;

        public void Initialize(eRobotArmType type)
        {
            RobotArmBuilder = new RobotArmBuilder();
            RobotArmBuilder.Initialize(type);

            Register();
        }

        private void Register()
        {
            IConnect = RobotArmBuilder.CurrentRobotArm;
            IMove = RobotArmBuilder.CurrentRobotArm;
        }

        public void Connect(string ip, string port)
        {
            IConnect.Connect(ip, port);
        }

        public void MoveToPosition(Vector3 position, eJointType type = eJointType.None)
        {
            IMove.MoveToPosition(position, type);
        }

        public void MoveToRotation(Vector3 rotation, eJointType type = eJointType.None)
        {
            IMove.MoveToRotation(rotation, type);

        }
    }
}
