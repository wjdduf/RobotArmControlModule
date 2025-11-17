using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RobotArm_Module.Scripts.Data;

namespace RobotArm_Module
{
	public class RBArm : RobotArm
    {
        protected RBTCPClient TCPClient;

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
            bool isSucces = false;

            try
            {
                Debug.Log($"RBArm Connect Start: {ip}");
                TCPClient = new RBTCPClient(ip); // RBT
                Debug.Log($"RBArm Connect Success: {ip}");

                //작성 필요
                string connect = $"socekt_connect(0,{ip},{DataContainer.Instance.RBConfig.RBTCP_PORT})";
                TCPClient.SendPacket(connect);
                TCPClient.Receive();

                TCPClient.SendPacket(RBInterface.PowerOn);
                TCPClient.Receive();
                
                TCPClient.SendPacket(RBInterface.SetRealMode);
                TCPClient.Receive();

                TCPClient.WorkThread();

                TCPClient.ConnectCheck = true;
                isSucces = true;
            }
            catch (SocketException ex)
            {
                Console.WriteLine("RBArm Connect Socket Error: " + ex.Message);
                isSucces = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RBArm Connect Error: " + ex.Message);
                isSucces = false;
            }
            
            onComplete?.Invoke(isSucces);
            return isSucces;
        }

        public override bool DisConnect(Action<bool> onComplete = null)
        {
            bool isSucces = false;

            try
            {
                TCPClient.SendPacket(RBInterface.PowerOff);
                TCPClient.Receive();

                TCPClient.SendPacket(RBInterface.SetRealMode);
                TCPClient.Receive();

                TCPClient.ConnectCheck = true;
                isSucces = true;
            }
            catch (SocketException ex)
            {
                Console.WriteLine("RBArm Connect Socket Error: " + ex.Message);
                isSucces = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RBArm Connect Error: " + ex.Message);
                isSucces = false;
            }
            
            onComplete?.Invoke(isSucces);
            return isSucces;
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

        public override void JointRotation(float angle, eJointType type = eJointType.None, Action<bool> onComplete = null)
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
            st.Append("move_j(jnt[");
            //st.Append("mov el(p[");

            st.Append(joint.GetJoint(eJointType.BASE).Angle.ToString("F3") + ",");
            st.Append(
                joint.GetJoint(eJointType.SHOULDER).Angle.ToString("F3") + ","
            );
            st.Append(
                joint.GetJoint(eJointType.ELBOW).Angle.ToString("F3") + ","
            );
            st.Append(
                joint.GetJoint(eJointType.WRIST1).Angle.ToString("F3") + ","
            );
            st.Append(
                joint.GetJoint(eJointType.WRIST2).Angle.ToString("F3") + ","
            );
            st.Append(joint.GetJoint(eJointType.WRIST3).Angle.ToString("F3"));
            //st.Append("])");
            st.Append($"], {Speed},{Acceleration})");
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());

            MoveWaitAsync(onComplete);
        }

        public override bool MonitorConnection()
        {
            if (TCPClient == null)
                return false;

            //TCPClient.DataReadThread();

            return TCPClient.ConnectCheck;
        }

        public override void MoveLoop(Vector3 fromPos, Vector3 fromRot, Vector3 toPos, Vector3 toRot, bool isLoop, float loopTime, Action<bool> onComplete = null, eMoveType moveType = eMoveType.Position)
        {
            throw new NotImplementedException();
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
            st.Append("jog_robot_j(1,");
            //st.Append("movel(p[");

            st.Append(position.X.ToString("F3") + ",");
            st.Append(position.Y.ToString("F3") + ",");
            st.Append(position.Z.ToString("F3") + ",");
            st.Append(rotation.X.ToString("F3") + ",");
            st.Append(rotation.Y.ToString("F3") + ",");
            st.Append(rotation.Z.ToString("F3"));
            //st.Append("])");
            st.Append(")");
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());
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
            st.Append("jog_robot_l(2,");
            //st.Append("movel(p[");

            st.Append(position.X.ToString("F3") + ",");
            st.Append(position.Y.ToString("F3") + ",");
            st.Append(position.Z.ToString("F3") + ",");
            st.Append(rotation.X.ToString("F3") + ",");
            st.Append(rotation.Y.ToString("F3") + ",");
            st.Append(rotation.Z.ToString("F3"));
            //st.Append("])");
            st.Append(")");
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());
        }

        public override void MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position, bool isLinear = false, Action<bool> onComplete = null)
        {
            if (position == null || rotation == null)
            {
                Debug.Log($"MoveToPreset :: position or rotation is null");
                onComplete?.Invoke(false);
                return;
            }

            Debug.Log($"RBArm MoveToPreset - {position} ::  {rotation}");

            string mType = "move_l";

            if (isLinear)
            {
                mType = "move_l";
            }
            else
            {
                mType = "move_jl";
            }

            if (moveType == eMoveType.Joint)
                mType = mType + "(jnt[";
            else
                mType = mType + "(pnt[";

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
                st.Append(position.X.ToString("F3") + ",");
                st.Append(position.Y.ToString("F3") + ",");
                st.Append(position.Z.ToString("F3") + ",");
                st.Append(rotation.X.ToString("F3") + ",");
                st.Append(rotation.Y.ToString("F3") + ",");
                st.Append(rotation.Z.ToString("F3"));
            }

            //st.Append("])");
            st.Append($"],{Speed},{Acceleration})");
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());

            MoveWaitAsync(onComplete);

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
            st.Append("jog_robot_l(2,");
            //st.Append("Jog-L([");

            //st.Append("movel(p[");

            st.Append(position.X.ToString("F3") + ",");
            st.Append(position.Y.ToString("F3") + ",");
            st.Append(position.Z.ToString("F3") + ",");
            st.Append(rotation.X.ToString("F3") + ",");
            st.Append(rotation.Y.ToString("F3") + ",");
            st.Append(rotation.Z.ToString("F3"));
            //st.Append("])");
            st.Append($")");
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());
        }

        public override void PlayCSV(List<CSVData> data, Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        public override void PlayPreset(WorkPreset preset, Action<bool> onComplete = null)
        {
            Debug.Log($"TestCode :: PlayPreset");

            //isUsingPreset = true;
            //onPresetComplete = onComplete;

            string scriptCommand;

            //st.Append("def Test():\n");
            List<string> commandList = new List<string>();

            if (preset.WorkList.Count == 0)
            {
                Debug.Log("Work Data Is Null");
                return;
            }

            List<PresetData> currentTargetQueue = preset.WorkList;


            StringBuilder st = new StringBuilder();
            
            for (int i = 0; i < currentTargetQueue.Count; i++)
            {
            
                st.Append(GetScript(currentTargetQueue[i]?.position, currentTargetQueue[i]?.rotation, currentTargetQueue[i].moveType, currentTargetQueue[i].isLinear));
                st.Append("\n");
                st.Append("move_finish_wait()\n");
            }
            
            
            //st.Append("end");
            
            
            scriptCommand = st.ToString();
            
            Debug.Log($"PlayPreset Create Script {scriptCommand}");
            
            TCPClient.SendPacket(scriptCommand);
            TCPClient.Receive();

            //for (int i = 0; i < currentTargetQueue.Count; i++)
            //{
            //    StringBuilder st = new StringBuilder();
            //
            //    st.Append(GetScript(currentTargetQueue[i]?.position, currentTargetQueue[i]?.rotation, currentTargetQueue[i].moveType, currentTargetQueue[i].isLinear));
            //    st.Append("\n");
            //
            //    scriptCommand = st.ToString();
            //    Debug.Log($"PlayPreset Create Script {scriptCommand}");
            //
            //
            //    TCPClient.SendPacket(scriptCommand);
            //    TCPClient.Receive();
            //
            //    TCPClient.SendPacket("move_finish_wait()\n");
            //    TCPClient.Receive();
            //
            //
            //}

            MoveWaitAsync(onComplete);
        }
        
        private string GetScript(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position, bool isLinear = false)
        {
            string mType = "move_l";

            if(isLinear)
            {
                mType = "move_l";
            }
            else
            {
                mType = "move_jl";
            }

            if (moveType == eMoveType.Joint)
                mType = mType + "(jnt[";
            else
                mType = mType + "(pnt[";

            StringBuilder st = new StringBuilder();
            st.Append($"{mType}");

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
                st.Append(position.X.ToString("F3") + ",");
                st.Append(position.Y.ToString("F3") + ",");
                st.Append(position.Z.ToString("F3") + ",");
                st.Append(rotation.X.ToString("F3") + ",");
                st.Append(rotation.Y.ToString("F3") + ",");
                st.Append(rotation.Z.ToString("F3"));
            }

            float speedFix = Speed;
            //if (isLinear)
            //    speedFix = Speed / 2f;

            st.Append($"],{speedFix},{Acceleration})");

            return st.ToString();
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
            TCPClient.SendPacket(RBInterface.Stop);
            TCPClient.Receive();
        }

        public override void TestCode(string script)
        {
            TCPClient.SendPacket("get_tcp_info()");
            TCPClient.Receive();
        }

        public override void UnlockProtectiveStop(Action<bool> onComplete = null)
        {
            throw new NotImplementedException();
        }

        private async void MoveWaitAsync(Action<bool> action)
        {
            bool isPositionMatched = true;

            await Task.Delay(1000);


            while (DataContainer.Instance.RobotArmCurrentData.isMove)
            {
                await Task.Delay(100);
            }

            Debug.Log("Move Check Complete");
            action?.Invoke(isPositionMatched);
        }

        
    }
}
