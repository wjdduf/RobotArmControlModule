using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public class RobotArmController
    {
        public RobotArmBuilder RobotArmBuilder;

        private IConnect IConnect;
        private IMove IMove;
        private IData IData;
        private ISafety ISafety;

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
            IData = RobotArmBuilder.CurrentRobotArm;
            ISafety = RobotArmBuilder.CurrentRobotArm;
        }

        public bool Connect(string ip)
        {
            return IConnect.Connect(ip);
        }

        public bool DisConnect()
        {
            return IConnect.DisConnect();
        }

        public void MoveToPosition(float speed, eDirection direction)
        {
            IMove.MoveToPosition(speed, direction);
        }

        public void MoveToRotation(float speed, eRotationAxis axis)
        {
            IMove.MoveToRotation(speed, axis);
        }

        public void MoveToPreset(Vector3 position, Vector3 rotation)
        {
            IMove.MoveToPreset(position, rotation);
        }

        public void Move()
        {
            IMove.MoveToPreset(DataContainer.Instance.RobotArmCurrentData.SetPosition, DataContainer.Instance.RobotArmCurrentData.SetRotation);
        }
        public void MoveToJoint(float speed, bool isUp, eJointType type)
        {
            IMove.MoveToJoint(speed, isUp, type);
        }

        public void JointRotation(float angle, eJointType type)
        {
            IMove.JointRotation(angle, type);
        }

        public void SetPivot(Vector3 pivot)
        {
            IMove.SetPivot(pivot);
        }
        public void Stop()
        {
            IMove.Stop();
        }

        public void ListPlay()
        {
            IMove.PlayPreset();
        }

        public void AddPlayList(Vector3 pos, Vector3 rot, eMoveType moveType = eMoveType.Position)
        {
            IData.AddWorkQueue(pos, rot, moveType);
        }

        public void SetHoming()
        {
            ISafety.Homming();
        }

        public void SetSpeed(float speed)
        {
            RobotArmBuilder.CurrentRobotArm.Speed = speed;
        }

        public void SetAcceleration(float accel)
        {
            RobotArmBuilder.CurrentRobotArm.Acceleration = accel;
        }
    }
}
