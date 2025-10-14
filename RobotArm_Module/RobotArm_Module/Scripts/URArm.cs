using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
        //private UR UR;
        private URTCPClient TCPClient;
        
        public override bool Connect(string ip, Action onComplete = null)
        {
            int port = DataContainer.Instance.URConfig.DASHBOARD_PORT;
            DataContainer.Instance.currentIP = ip;
            //연결 관련 코드 구현
            Debug.Log($"Connect URArm ip - {ip} :: port - {port}");

            bool isSucces = false;

            try
            {
                

                // 2. 서버에 연결하기
                Debug.Log($"서버에 연결 중... ({ip}:{port})");
                TCPClient = new URTCPClient(ip);
                Debug.Log("서버에 연결되었습니다.");

                TCPClient.SendPacket(URInterface.RobotMode, ePortType.Dashboard);

                if(TCPClient.Receive(ePortType.Dashboard).Contains(eRobotMode.RUNNING.ToString()))
                {
                    Debug.Log("이미 연결 중입니다.");
                    return true;
                }

                TCPClient.OnRTDEDataReceive -= Rtde_OutputDataReceived;
                TCPClient.OnRTDEDataReceive += Rtde_OutputDataReceived;


                TCPClient.SendPacket(URInterface.PowerOn, ePortType.Dashboard);
                TCPClient.Receive(ePortType.Dashboard);

                TCPClient.SendPacketWait(URInterface.RobotMode, eRobotMode.IDLE.ToString());

                TCPClient.SendPacket(URInterface.BrakeRelease, ePortType.Dashboard);
                TCPClient.Receive(ePortType.Dashboard);

                TCPClient.SendPacketWait(URInterface.RobotMode, eRobotMode.RUNNING.ToString());

                isSucces = true;
            }
            catch (SocketException e)
            {
                Debug.Log($"소켓 예외 발생: {e.Message}");
            }
            catch (Exception e)
            {
                Debug.Log($"일반 예외 발생: {e.Message}");
            }
            finally
            {
                // 5. 연결 끊기
                // NetworkStream과 TcpClient 객체를 닫아 리소스를 해제합니다.
                if (stream != null)
                {
                    stream.Close();
                    Debug.Log("네트워크 스트림을 닫았습니다.");
                }
                if (client != null)
                {
                    client.Close();
                    Debug.Log("클라이언트 연결을 끊었습니다.");
                }


            }
            return isSucces;
        }

        public override bool DisConnect()
        {
            int port = DataContainer.Instance.URConfig.DASHBOARD_PORT;
            string ip = DataContainer.Instance.currentIP;

            //연결 관련 코드 구현
            Debug.Log($"DisConnect URArm ip - {ip} :: port - {port}");

            bool isSucces = false;
            if (TCPClient == null)
            {
                TCPClient = new URTCPClient(ip);
            }

            try
            {
                TCPClient.SendPacket(URInterface.PowerOff, ePortType.Dashboard);
                TCPClient.Receive(ePortType.Dashboard);

                TCPClient.SendPacketWait(URInterface.RobotMode, eRobotMode.POWER_OFF.ToString());

                isSucces = true;
            }
            catch (SocketException e)
            {
                Debug.Log($"소켓 예외 발생: {e.Message}");
            }
            catch (Exception e)
            {
                Debug.Log($"일반 예외 발생: {e.Message}");
            }
            finally
            {
                // 5. 연결 끊기
                // NetworkStream과 TcpClient 객체를 닫아 리소스를 해제합니다.
                //if(TCPClient!= null)
                //    TCPClient.DisConnect();
            }
            return isSucces;
        }

        public override void ShutDown()
        {
            //UR.Dashboard.Shutdown();
            TCPClient.SendPacket(URInterface.ShutDown, ePortType.Dashboard);
        }


        private void Rtde_OutputDataReceived(object sender, EventArgs e)
        {

            // Get the value of the data you have selected in the setup
            SetCurrentRobotTransform(TCPClient.UrOutputs.actual_TCP_pose);
            
            //수정필요
            MoveCheck(TCPClient.UrOutputs.actual_TCP_speed);

            SetCurrentJoinData(TCPClient.UrOutputs.actual_q);


            //Debug.Log($"ActualCurrent ::: {TCPClient.UrOutputs.actual_TCP_speed[0]}");
            //Debug.Log($"ActualCurrent2222 ::: {TCPClient.UrOutputs.actual_current[0]}");

        }

        private void SetCurrentRobotTransform(double[] pos)
        {
            DataContainer.Instance.RobotArmCurrentData.currentPosition.X = (float)pos[0];
            DataContainer.Instance.RobotArmCurrentData.currentPosition.Y = (float)pos[1];
            DataContainer.Instance.RobotArmCurrentData.currentPosition.Z = (float)pos[2];

            DataContainer.Instance.RobotArmCurrentData.currentRotation.X = (float)pos[3];
            DataContainer.Instance.RobotArmCurrentData.currentRotation.Y = (float)pos[4];
            DataContainer.Instance.RobotArmCurrentData.currentRotation.Z = (float)pos[5];
        }

        private void MoveCheck(double[] qd)
        {
            bool isMove = false;
            for (int i = 0; i < qd.Length; i++)
            {
                //Move
                if (Math.Abs(qd[i]) >= 0.001f)
                {
                    isMove = true;
                    break;
                }
            }
            this.isMove = isMove;
        }

        public override void Stop()
        {
            //UR.PrimaryInterface.Script.Send("speedl([0,0,0,0,0,0],0.5)");
            //UR.PrimaryInterface.Script.Send("speedj([0,0,0,0,0,0],0.5)");


            //UR.PrimaryInterface.Script.Send("stopl(0.5)");
            TCPClient.SendPacket("stopl(0.5)");

            //UR.PrimaryInterface.Script.Send("stopj(1)");
            TCPClient.SendPacket("stopj(1)");

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
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());

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
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());

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
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());

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
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());

        }

        private void SetCurrentJoinData(double[] angle)
        {
            
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.BASE)
                .Angle = RadiansToDegrees(angle[0]);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.SHOULDER)
                .Angle = RadiansToDegrees(angle[1]);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.ELBOW)
                .Angle = RadiansToDegrees(angle[2]);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST1)
                .Angle = RadiansToDegrees(angle[3]);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST2)
                .Angle = RadiansToDegrees(angle[4]);
            DataContainer
                .Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST3)
                .Angle = RadiansToDegrees(angle[5]);
                
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

            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());
        }

        public override void TestCode(string script)
        {
            //UR.InterpreterMode.ClearInterpreter();
            //UseInterPreterMode = true;
            //Debug.Log($"TestCode :: {UR.InterpreterMode.Connected}");
            //UR.PrimaryInterface.Script.Send(script);
            //UR.PrimaryInterface.Script.Send("movel(p[-0.150,0.600,0.650,0,0,6],1.2,0.2,0,0.1)");
            //UR.PrimaryInterface.Script.Send("movel(p[-0.150,0.300,0.650,0,0,6],1.2,0.2,0,0.1)");

            //UR.InterpreterMode.ExecuteCommand("movel(p[-0.150,0.600,0.650,0,0,6],1.2,0.2,0,0.1)");
            //UR.InterpreterMode.ExecuteCommand("movel(p[-0.150,0.300,0.650,0,0,6],1.2,0.2,0,0.1)");
               
            if(TCPClient == null)
            {
                TCPClient = new URTCPClient(DataContainer.Instance.URConfig.IP);
            }
            GetSafetyMode();


            return;




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

            //MoveP();
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

        public void MoveP()
        {
            StringBuilder st = new StringBuilder();
            st.Append("servol(");
            for (int i = 0; i < DataContainer.Instance.WorkPreset.WorkList.Count; i++)
            {
                var item = DataContainer.Instance.WorkPreset.WorkList[i];
                st.Append("p[");

                st.Append($"{item.position.X.ToString("F3")},");
                st.Append($"{item.position.Y.ToString("F3")},");
                st.Append($"{item.position.Z.ToString("F3")},");
                st.Append($"{item.rotation.X.ToString("F3")},");
                st.Append($"{item.rotation.Y.ToString("F3")},");
                st.Append($"{item.rotation.Z.ToString("F3")}");
                st.Append("],");
            }
            st.Append($"{Acceleration},{Speed})");
            Debug.Log($"MoveP TestCode :: {st.ToString()}");
            DataContainer.Instance.WorkPreset.WorkList.Clear();

            //UR.PrimaryInterface.Script.Send(st.ToString());
            
            TCPClient.SendPacket(st.ToString());
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

        public override bool GetSafetyMode()
        {
            bool isSafety = false;

            TCPClient.SendPacket(URInterface.GetSafetyStatus,ePortType.Dashboard);


            var data = TCPClient.Receive(ePortType.Dashboard);
            if(data.Contains("NORMAL"))
            {
                isSafety = true;
            }
            

            return isSafety;
        }

        public override void UnlockProtectiveStop()
        {
            TCPClient.SendPacket(URInterface.ClosePopup, ePortType.Dashboard);
            TCPClient.Receive(ePortType.Dashboard);

            TCPClient.SendPacket(URInterface.UnlockProtectiveStop, ePortType.Dashboard);
            TCPClient.Receive(ePortType.Dashboard);

        }
    }
}
