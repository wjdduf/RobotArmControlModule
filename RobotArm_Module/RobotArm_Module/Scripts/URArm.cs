using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnderAutomation.UniversalRobots;
using UnderAutomation.UniversalRobots.Common;
using UnderAutomation.UniversalRobots.Rtde;

namespace RobotArm_Module
{
    public enum eRobotMode
    {
        NO_CONTROLLER,
        DISCONNECTED,
        CONFIRM_SAFETY,
        BOOTING,
        POWER_OFF,
        POWER_ON,
        IDLE,
        BACKDRIVE,
        RUNNING,
    }


    class URArm : RobotArm
    {
        private UR UR;
        private ConnectParameters connectParameters;
        public bool UseInterPreterMode
        {
            get { return connectParameters.InterpreterMode.Enable; }
            set { connectParameters.InterpreterMode.Enable = value; }
        }

        public URArm()
        {
            UR = new UR();
        }

        ~URArm()
        {
            UR.Rtde.OutputDataReceived -= Rtde_OutputDataReceived;
            UR.PrimaryInterface.JointDataReceived -= SetCurrentJoinData;
        }

        public override bool Connect(string ip, Action onComplete = null)
        {
            bool isSucces = true;

            Debug.Log($"서버에 연결 중... ({ip})");
            ConnectParamSet(ip);
            UR.Connect(connectParameters);
            //UR.Rtde.Connect();
            Debug.Log("서버에 연결되었습니다.");
            UR.Rtde.OutputDataReceived += Rtde_OutputDataReceived;
            UR.PrimaryInterface.JointDataReceived += SetCurrentJoinData;

            UR.Dashboard.PowerOn();

            UR.Dashboard.ReleaseBrake();

            WaitStatus(eRobotMode.RUNNING.ToString(), onComplete);

            return isSucces;
        }

        public override bool DisConnect()
        {
            bool isSucces = false;

            isSucces = UR.Dashboard.PowerOff().Succeed;

            UR.InterpreterMode.Disconnect();

            UR.Rtde.Disconnect();
            UR.Disconnect();

            Debug.Log($"RTDE :: {UR.Rtde.Connected}");
            Debug.Log($"InterpreterMode :: {UR.InterpreterMode.Connected}");
            Debug.Log($"UR :: {UR.SocketCommunication.Enabled}");

            return true;
        }

        public override void ShutDown()
        {
            throw new NotImplementedException();
        }

        private void ConnectParamSet(string ip)
        {
            connectParameters = new ConnectParameters(ip);

            // Enable RTDE
            connectParameters.Rtde.Enable = true;

            //connectParameters.InterpreterMode.Enable = true;

            // Exchange data at 500Hz
            //connectParameters.Rtde.Frequency = 500;

            // Select data you want to write in robot controller
            connectParameters.Rtde.InputSetup.Add(RtdeInputData.StandardAnalogOutput0);
            connectParameters.Rtde.InputSetup.Add(RtdeInputData.InputIntRegisters, 0);

            // Select data you want the robot to send
            connectParameters.Rtde.OutputSetup.Add(RtdeOutputData.ActualTcpPose);
            connectParameters.Rtde.OutputSetup.Add(RtdeOutputData.ActualTcpSpeed);
            connectParameters.Rtde.OutputSetup.Add(RtdeOutputData.JointControlOutput);
            connectParameters.Rtde.OutputSetup.Add(RtdeOutputData.ToolOutputVoltage);
            connectParameters.Rtde.OutputSetup.Add(RtdeOutputData.OutputDoubleRegisters, 10);
        }

        private void Rtde_OutputDataReceived(object sender, RtdeDataPackageEventArgs e)
        {
            // Get frequency of received message (OutputSetup contains Timestamp by default)
            var realMessageFrequency = e.MeasuredFrequency;

            // Get the value of the data you have selected in the setup
            SetCurrentRobotTransform(e.OutputDataValues.ActualTcpPose);
            MoveCheck(e.OutputDataValues.ActualTcpSpeed.Values);
            var ActualCurrent = e.OutputDataValues.TargetTcpPose;
            double outputDoubleRegisters10 = e.OutputDataValues.OutputDoubleRegisters.X10;

            //Debug.Log($"ActualCurrent ::: {ActualCurrent}");
        }

        private void SetCurrentRobotTransform(Pose pos)
        {
            DataContainer.Instance.RobotArmCurrentData.currentPosition.X = (float)pos.X;
            DataContainer.Instance.RobotArmCurrentData.currentPosition.Y = (float)pos.Y;
            DataContainer.Instance.RobotArmCurrentData.currentPosition.Z = (float)pos.Z;

            DataContainer.Instance.RobotArmCurrentData.currentRotation.X = (float)pos.Rx;
            DataContainer.Instance.RobotArmCurrentData.currentRotation.Y = (float)pos.Ry;
            DataContainer.Instance.RobotArmCurrentData.currentRotation.Z = (float)pos.Rz;
        }

        private void MoveCheck(double[] qd)
        {
            bool isMove = false;
            for (int i = 0; i < qd.Length; i++)
            {
                if (Math.Abs(qd[i]) >= 0.001f)
                {
                    isMove = true;
                    break;
                }
            }
            this.isMove = isMove;
        }

        private async void WaitStatus(string waitText, Action onComplete = null)
        {
            while (
                !UR.Dashboard.GetRobotMode().Value.ToString().ToLower().Contains(waitText.ToLower())
            )
            {
                Debug.Log(
                    $"Wait Status :: Current - {UR.Dashboard.GetRobotMode().Value.ToString()} : wait - {waitText}"
                );
                await Task.Delay(100);
            }
            onComplete?.Invoke();
        }

        public override void Stop()
        {
            //UR.PrimaryInterface.Script.Send("speedl([0,0,0,0,0,0],0.5)");
            //UR.PrimaryInterface.Script.Send("speedj([0,0,0,0,0,0],0.5)");


            UR.PrimaryInterface.Script.Send("stopl(0.5)");
            UR.PrimaryInterface.Script.Send("stopj(1)");
            isMove = false;
            isUsingPreset = false;

        }

        public override void MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position)
        {
            Debug.Log($"URArm MoveToPreset - {position} ::  {rotation}");
            
            string mType = "movel";
            if (moveType == eMoveType.Joint)
                mType = "movej([";
            else
                mType = "movel(p[";

            if (position == null || rotation == null)
            {
                Debug.Log($"MoveToPreset :: position or rotation is null");
                return;
            }

            StringBuilder st = new StringBuilder();
            st.Append($"{mType}");
            //st.Append("movel(p[");

            if (moveType == eMoveType.Position)
            {
                st.Append(position.X.ToString("F3") + ",");
                st.Append(position.Y.ToString("F3") + ",");
                st.Append(position.Z.ToString("F3") + ",");
                st.Append(rotation.X.ToString("F3") + ",");
                st.Append(rotation.Y.ToString("F3") + ",");
                st.Append(rotation.Z.ToString("F3"));
            }
            else if (moveType == eMoveType.Joint)
            {
                st.Append(DegreesToRadians(position.X).ToString("F3") + ",");
                st.Append(DegreesToRadians(position.Y).ToString("F3") + ",");
                st.Append(DegreesToRadians(position.Z).ToString("F3") + ",");
                st.Append(DegreesToRadians(rotation.X).ToString("F3") + ",");
                st.Append(DegreesToRadians(rotation.Y).ToString("F3") + ",");
                st.Append(DegreesToRadians(rotation.Z).ToString("F3"));
            }
            
            //st.Append("])");
            st.Append($"],{Acceleration},{Speed})");
            UR.PrimaryInterface.Script.Send(st.ToString());
        }

        public override void MoveToPosition(float speed, eDirection direction)
        {
            Debug.Log($"MoveToPosition :: speed - {speed} :: direction - {direction.ToString()}");

            Vector3 position = new Vector3();
            Vector3 rotation = new Vector3();

            switch (direction)
            {
                case eDirection.X_Negative:
                    position.X = -speed;
                    break;
                case eDirection.X_Positive:
                    position.X = speed;
                    break;
                case eDirection.Y_Negative:
                    position.Y = -speed;
                    break;
                case eDirection.Y_Positive:
                    position.Y = speed;
                    break;
                case eDirection.Z_Negative:
                    position.Z = -speed;
                    break;
                case eDirection.Z_Positive:
                    position.Z = speed;
                    break;
            }

            StringBuilder st = new StringBuilder();
            st.Append("speedl([");
            //st.Append("movel(p[");

            st.Append(position.X.ToString("F3") + ",");
            st.Append(position.Y.ToString("F3") + ",");
            st.Append(position.Z.ToString("F3") + ",");
            st.Append(rotation.X.ToString("F3") + ",");
            st.Append(rotation.Y.ToString("F3") + ",");
            st.Append(rotation.Z.ToString("F3"));
            //st.Append("])");
            st.Append("], 0.2,0.5)");
            UR.PrimaryInterface.Script.Send(st.ToString());
        }

        public override void MoveToRotation(float speed, eRotationAxis axis)
        {
            Vector3 position = new Vector3();
            Vector3 rotation = new Vector3();

            Debug.Log($"Move To Rotation :: speed - {speed} :: axis - {axis.ToString()}");

            switch (axis)
            {
                case eRotationAxis.X_Axis_Negative:
                    rotation.X = -speed;
                    break;
                case eRotationAxis.X_Axis_Positive:
                    rotation.X = speed;
                    break;
                case eRotationAxis.Y_Axis_Negative:
                    rotation.Y = -speed;
                    break;
                case eRotationAxis.Y_Axis_Positive:
                    rotation.Y = speed;
                    break;
                case eRotationAxis.Z_Axis_Negative:
                    rotation.Z = -speed;
                    break;
                case eRotationAxis.Z_Axis_Positive:
                    rotation.Z = speed;
                    break;
            }

            StringBuilder st = new StringBuilder();
            st.Append("speedl([");
            //st.Append("movel(p[");

            st.Append(position.X.ToString("F3") + ",");
            st.Append(position.Y.ToString("F3") + ",");
            st.Append(position.Z.ToString("F3") + ",");
            st.Append(rotation.X.ToString("F3") + ",");
            st.Append(rotation.Y.ToString("F3") + ",");
            st.Append(rotation.Z.ToString("F3"));
            //st.Append("])");
            st.Append("], 0.2,0.5)");
            UR.PrimaryInterface.Script.Send(st.ToString());
        }

        public override void MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None)
        {
            Vector3 position = new Vector3();
            Vector3 rotation = new Vector3();

            if (!isUp)
                speed *= -1f;

            switch (type)
            {
                case eJointType.BASE:
                    position.X = speed;
                    break;
                case eJointType.SHOULDER:
                    position.Y = speed;
                    break;
                case eJointType.ELBOW:
                    position.Z = speed;
                    break;
                case eJointType.WRIST1:
                    rotation.X = speed;
                    break;
                case eJointType.WRIST2:
                    rotation.Y = speed;
                    break;
                case eJointType.WRIST3:
                    rotation.Z = speed;
                    break;
            }

            StringBuilder st = new StringBuilder();
            st.Append("speedj([");
            //st.Append("movel(p[");

            st.Append(position.X.ToString("F3") + ",");
            st.Append(position.Y.ToString("F3") + ",");
            st.Append(position.Z.ToString("F3") + ",");
            st.Append(rotation.X.ToString("F3") + ",");
            st.Append(rotation.Y.ToString("F3") + ",");
            st.Append(rotation.Z.ToString("F3"));
            //st.Append("])");
            st.Append("], 0.2,0.5)");
            UR.PrimaryInterface.Script.Send(st.ToString());
        }

        public override void JointRotation(float angle, eJointType type = eJointType.None)
        {
            JointData joint = new JointData();

            joint.GetJoint(eJointType.BASE).Angle = DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.BASE)
                .Angle;
            joint.GetJoint(eJointType.SHOULDER).Angle = DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.SHOULDER)
                .Angle;
            joint.GetJoint(eJointType.ELBOW).Angle = DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.ELBOW)
                .Angle;
            joint.GetJoint(eJointType.WRIST1).Angle = DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST1)
                .Angle;
            joint.GetJoint(eJointType.WRIST2).Angle = DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST2)
                .Angle;
            joint.GetJoint(eJointType.WRIST3).Angle = DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST3)
                .Angle;

            joint.GetJoint(type).Angle = angle;

            StringBuilder st = new StringBuilder();
            st.Append("movej([");
            //st.Append("mov el(p[");

            st.Append(DegreesToRadians(joint.GetJoint(eJointType.BASE).Angle).ToString("F3") + ",");
            st.Append(
                DegreesToRadians(joint.GetJoint(eJointType.SHOULDER).Angle).ToString("F3") + ","
            );
            st.Append(
                DegreesToRadians(joint.GetJoint(eJointType.ELBOW).Angle).ToString("F3") + ","
            );
            st.Append(
                DegreesToRadians(joint.GetJoint(eJointType.WRIST1).Angle).ToString("F3") + ","
            );
            st.Append(
                DegreesToRadians(joint.GetJoint(eJointType.WRIST2).Angle).ToString("F3") + ","
            );
            st.Append(DegreesToRadians(joint.GetJoint(eJointType.WRIST3).Angle).ToString("F3"));
            //st.Append("])");
            st.Append($"], {Acceleration},{Speed})");
            UR.PrimaryInterface.Script.Send(st.ToString());
        }

        private void SetCurrentJoinData(
            object sender,
            UnderAutomation.UniversalRobots.PrimaryInterface.JointDataPackageEventArgs e
        )
        {
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.BASE)
                .Angle = RadiansToDegrees(e.Base.Position);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.SHOULDER)
                .Angle = RadiansToDegrees(e.Shoulder.Position);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.ELBOW)
                .Angle = RadiansToDegrees(e.Elbow.Position);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST1)
                .Angle = RadiansToDegrees(e.Wrist1.Position);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST2)
                .Angle = RadiansToDegrees(e.Wrist2.Position);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST3)
                .Angle = RadiansToDegrees(e.Wrist3.Position);
        }

        public override void SetPivot(Vector3 pivot)
        {
            StringBuilder st = new StringBuilder();
            st.Append("set_tcp(p[");
            st.Append(pivot.X.ToString("F3") + ",");
            st.Append(pivot.Y.ToString("F3") + ",");
            st.Append(pivot.Z.ToString("F3") + ",");
            st.Append("0, 0, 0])");

            Debug.Log($"SetPivot :: {st.ToString()}");

            UR.PrimaryInterface.Script.Send(st.ToString());
        }

        public override void TestCode(string script)
        {
            //UR.InterpreterMode.ClearInterpreter();
            //UseInterPreterMode = true;
            Debug.Log($"TestCode :: {UR.InterpreterMode.Connected}");
            //UR.PrimaryInterface.Script.Send(script);
            //UR.InterpreterMode.ExecuteCommand("movel(p[-0.150,0.600,0.650,0,0,6])");
            //UR.InterpreterMode.ExecuteCommand("movel(p[-0.150,0.300,0.650,0,0,6])");

            PresetData temp = new PresetData(new Vector3(0.150f, 0.300f, 0.650f), new Vector3(4.766f, 0.010f, 0.010f));
            temp.presetID = "0";
            DataContainer.Instance.WorkPreset.Add(temp);


            temp = new PresetData(new Vector3(0.133f, 0.524f, 0.650f), new Vector3(4.766f, 0.010f, 0.010f));
            temp.presetID = "1";
            DataContainer.Instance.WorkPreset.Add(temp);



            temp = new PresetData(new Vector3(-0.300f, 0.100f, 0.650f), new Vector3(2.088f, 2.453f, -2.580f));
            temp.presetID = "2";
            DataContainer.Instance.WorkPreset.Add(temp);



            temp = new PresetData(new Vector3(-0.600f, 0.100f, 0.650f), new Vector3(2.088f, 2.453f, -2.580f));
            temp.presetID = "3";
            DataContainer.Instance.WorkPreset.Add(temp);


            temp = new PresetData(new Vector3(0.150f, 0.300f, 0.650f), new Vector3(4.766f, 0.010f, 0.010f));
            temp.presetID = "4";
            DataContainer.Instance.WorkPreset.Add(temp);



            temp = new PresetData(new Vector3(0.300f, -0.100f, 0.650f), new Vector3(2.088f, -2.453f, 2.580f));
            temp.presetID = "5";
            DataContainer.Instance.WorkPreset.Add(temp);


            temp = new PresetData(new Vector3(0.600f, -0.100f, 0.650f), new Vector3(2.088f, -2.453f, 2.580f));
            temp.presetID = "6";
            DataContainer.Instance.WorkPreset.Add(temp);

            PlayPreset();
            //UR.InterpreterMode.EndInterpreter();

            return;
        }

        public override void PlayPreset(Action onComplete = null)
        {
            Debug.Log($"TestCode :: PlayPreset");

            isUsingPreset = true;
            onPresetComplete = onComplete;
        }

        public override void AddWorkQueue(Vector3 pos, Vector3 rot, eMoveType moveType = eMoveType.Position)
        {
            PresetData data = new PresetData(pos, rot);

            StringBuilder st = new StringBuilder();
            st.Append($"{pos.X.ToString("F3")},");
            st.Append($"{pos.Y.ToString("F3")},");
            st.Append($"{pos.Z.ToString("F3")},");
            st.Append($"{rot.X.ToString("F3")},");
            st.Append($"{rot.Y.ToString("F3")},");
            st.Append($"{rot.Z.ToString("F3")}");

            data.presetName = DataContainer.Instance.IDCount + ":" + st.ToString();
            data.presetID = DataContainer.Instance.IDCount.ToString();
            data.moveType = moveType;

            DataContainer.Instance.WorkPreset.Add(data);

        }

        public override void AddWorkQueue(PresetData[] preset)
        {
            throw new NotImplementedException();
        }

        public override void EmergencyStop()
        {
            throw new NotImplementedException();
        }

        public override void Homming()
        {
            MoveToPreset(new Vector3(90,-170,135), new Vector3(-150,90,0),eMoveType.Joint);
        }
    }
}
