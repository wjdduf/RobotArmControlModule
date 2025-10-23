using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

                TCPClient.OnRTDEDataReceive -= Rtde_OutputDataReceived;
                TCPClient.OnRTDEDataReceive += Rtde_OutputDataReceived;

                TCPClient.SendPacket(URInterface.RobotMode, ePortType.Dashboard);

                if(TCPClient.Receive(ePortType.Dashboard).Contains(eRobotMode.RUNNING.ToString()))
                {
                    Debug.Log("이미 연결 중입니다.");
                    return true;
                }

                


                TCPClient.SendPacket(URInterface.PowerOn, ePortType.Dashboard);
                TCPClient.Receive(ePortType.Dashboard);

                TCPClient.SendPacketWait(URInterface.RobotMode, eRobotMode.IDLE.ToString());

                TCPClient.SendPacket(URInterface.BrakeRelease, ePortType.Dashboard);
                TCPClient.Receive(ePortType.Dashboard);

                TCPClient.SendPacketWait(URInterface.RobotMode, eRobotMode.RUNNING.ToString());

                TCPClient.SendPacket(URInterface.ConnectGrip, ePortType.Dashboard);


                TCPClient.ConnectCheck = true;
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

                TCPClient.ConnectCheck = false;


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
                if (Math.Abs(qd[i]) >= 0.0001f)
                {
                    isMove = true;
                    break;
                }
            }
            this.isMove = isMove;
        }

        public override int Stop()
        {
            //UR.PrimaryInterface.Script.Send("speedl([0,0,0,0,0,0],0.5)");
            //UR.PrimaryInterface.Script.Send("speedj([0,0,0,0,0,0],0.5)");


            //UR.PrimaryInterface.Script.Send("stopl(0.5)");
            TCPClient.SendPacket("stopl(0.5)");

            //UR.PrimaryInterface.Script.Send("stopj(1)");
            TCPClient.SendPacket("stopj(1)");

            isMove = false;
            isUsingPreset = false;

            return 1;
        }

        public override int MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position, bool isLinear = false, Action onComplete = null)
        {

            if (position == null || rotation == null)
            {
                Debug.Log($"MoveToPreset :: position or rotation is null");
                return 0;
            }

            Debug.Log($"URArm MoveToPreset - {position} ::  {rotation}");


            string mType = "movel";

            if (isLinear)
            {
                mType = "movel";
            }
            else
            {
                mType = "movej";
            }

            if (moveType == eMoveType.Joint)
                mType = mType + "([";
            else
                mType = mType + "(p[";

            StringBuilder st = new StringBuilder();
            st.Append($"{mType}");

            //st.Append("movel(p[");

            if (moveType == eMoveType.Position)
            {
                st.Append(position.X.ToString("F5") + ",");
                st.Append(position.Y.ToString("F5") + ",");
                st.Append(position.Z.ToString("F5") + ",");
                st.Append(rotation.X.ToString("F5") + ",");
                st.Append(rotation.Y.ToString("F5") + ",");
                st.Append(rotation.Z.ToString("F5"));
            }
            else if (moveType == eMoveType.Joint)
            {
                st.Append(DegreesToRadians(position.X).ToString("F5") + ",");
                st.Append(DegreesToRadians(position.Y).ToString("F5") + ",");
                st.Append(DegreesToRadians(position.Z).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.X).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.Y).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.Z).ToString("F5"));
            }

            //st.Append("])");
            st.Append($"],a={Acceleration},v={Speed})");
            //UR.PrimaryInterface.Script.Send(st.ToString());
            TCPClient.SendPacket(st.ToString());

            MoveWaitAsync(onComplete);

            return 1;
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

        public override int MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None)
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

            return 1;
        }

        public override int JointRotation(float angle, eJointType type = eJointType.None)
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

            return 1;
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

        public override int SetPivot(Vector3 pivot)
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

            return 1;
        }

        public override void TestCode(string script)
        {
            
        }

        public override void PlayPreset(WorkPreset Preset, Action onComplete = null)
        {
            Debug.Log($"TestCode :: PlayPreset");

            //isUsingPreset = true;
            //onPresetComplete = onComplete;

            string scriptCommand;

            StringBuilder st = new StringBuilder();
            st.Append("def Test():\n");

            if (Preset.WorkList.Count == 0)
            {
                Debug.Log("Work Data Is Null");
                return;
            }

            List<PresetData> currentTargetQueue = Preset.WorkList;


            for (int i =0;i< currentTargetQueue.Count;i++)
            {
                st.Append(GetScript(currentTargetQueue[i]?.position, currentTargetQueue[i]?.rotation, currentTargetQueue[i].moveType, currentTargetQueue[i].isLinear));
                st.Append("\n");
            }


            st.Append("end");

            scriptCommand = st.ToString();

            Debug.Log($"PlayPreset Create Script { scriptCommand}");

            TCPClient.SendPacket(scriptCommand);

            MoveWaitAsync(onComplete);
        }

        private async void MoveWaitAsync(Action action)
        {
            Thread.Sleep(1000);

            while (isMove)
            {
                await Task.Delay(100);
            }

            Debug.Log("Move Check Complete");
            action?.Invoke();
        }


        private string GetScript(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position, bool isLinear = false)
        {
            string mType = "movel";

            if(isLinear)
            {
                mType = "movel";
            }
            else
            {
                mType = "movej";
            }

            if (moveType == eMoveType.Joint)
                mType = mType + "([";
            else
                mType = mType + "(p[";

            StringBuilder st = new StringBuilder();
            st.Append($"{mType}");

            if (moveType == eMoveType.Position)
            {
                st.Append(position.X.ToString("F5") + ",");
                st.Append(position.Y.ToString("F5") + ",");
                st.Append(position.Z.ToString("F5") + ",");
                st.Append(rotation.X.ToString("F5") + ",");
                st.Append(rotation.Y.ToString("F5") + ",");
                st.Append(rotation.Z.ToString("F5"));
            }
            else if (moveType == eMoveType.Joint)
            {
                st.Append(DegreesToRadians(position.X).ToString("F5") + ",");
                st.Append(DegreesToRadians(position.Y).ToString("F5") + ",");
                st.Append(DegreesToRadians(position.Z).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.X).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.Y).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.Z).ToString("F5"));
            }

            float speedFix = Speed;
            if (isLinear)
                speedFix = Speed / 2f;

            st.Append($"],a={Acceleration},v={speedFix})");

            return st.ToString();
        }

        private string GetScript(Vector3 position, Vector3 rotation, float time, eMoveType moveType = eMoveType.Position)
        {
            string mType = "movel";
            if (moveType == eMoveType.Joint)
                mType = "movej([";
            else
                mType = "movej(p[";

            StringBuilder st = new StringBuilder();
            st.Append($"{mType}");

            if (moveType == eMoveType.Position)
            {
                st.Append(position.X.ToString("F5") + ",");
                st.Append(position.Y.ToString("F5") + ",");
                st.Append(position.Z.ToString("F5") + ",");
                st.Append(rotation.X.ToString("F5") + ",");
                st.Append(rotation.Y.ToString("F5") + ",");
                st.Append(rotation.Z.ToString("F5"));
            }
            else if (moveType == eMoveType.Joint)
            {
                st.Append(DegreesToRadians(position.X).ToString("F5") + ",");
                st.Append(DegreesToRadians(position.Y).ToString("F5") + ",");
                st.Append(DegreesToRadians(position.Z).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.X).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.Y).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.Z).ToString("F5"));
            }

            st.Append($"],t={time})");

            return st.ToString();
        }

        private string GetScript(Vector3 position, Vector3 rotation, double velocity, double acceleration, eMoveType moveType = eMoveType.Position)
        {
            string mType = "movel";
            if (moveType == eMoveType.Joint)
                mType = "movej([";
            else
                mType = "movej(p[";

            StringBuilder st = new StringBuilder();
            st.Append($"{mType}");

            if (moveType == eMoveType.Position)
            {
                st.Append(position.X.ToString("F5") + ",");
                st.Append(position.Y.ToString("F5") + ",");
                st.Append(position.Z.ToString("F5") + ",");
                st.Append(rotation.X.ToString("F5") + ",");
                st.Append(rotation.Y.ToString("F5") + ",");
                st.Append(rotation.Z.ToString("F5"));
            }
            else if (moveType == eMoveType.Joint)
            {
                st.Append(DegreesToRadians(position.X).ToString("F5") + ",");
                st.Append(DegreesToRadians(position.Y).ToString("F5") + ",");
                st.Append(DegreesToRadians(position.Z).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.X).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.Y).ToString("F5") + ",");
                st.Append(DegreesToRadians(rotation.Z).ToString("F5"));
            }

            st.Append($"],a={acceleration},v={velocity})");

            return st.ToString();
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

        public override int Homming()
        {
            MoveToPreset(new Vector3(-90,-110,140), new Vector3(-30,90,180),eMoveType.Joint);

            return 1;
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

        public override int UnlockProtectiveStop()
        {
            TCPClient.SendPacket(URInterface.ClosePopup, ePortType.Dashboard);
            TCPClient.Receive(ePortType.Dashboard);

            TCPClient.SendPacket(URInterface.UnlockProtectiveStop, ePortType.Dashboard);
            TCPClient.Receive(ePortType.Dashboard);

            return 1;
        }

        public override int PlayCSV(List<CSVData> data)
        {

            Vector3 InitPos = new Vector3();
            Vector3 InitRot = new Vector3();

            InitPos.X = (float)(DataContainer.Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.BASE).Angle);
            InitPos.Y = (float)(DataContainer.Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.SHOULDER).Angle);
            InitPos.Z = (float)(DataContainer.Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.ELBOW).Angle);



            InitRot.X = (float)DataContainer.Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST1).Angle;
            InitRot.Y = (float)DataContainer.Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST2).Angle;
            InitRot.Z = (float)DataContainer.Instance.RobotArmCurrentData.currentJoinData.GetJoint(eJointType.WRIST3).Angle;

            StringBuilder st = new StringBuilder();
            st.Append("def my_sequence():\n");


            for (int i = 0; i < data.Count; i++)
            {
                Vector3 rot = new Vector3();
                rot.X = (float)(InitRot.X + (data[i].radianX * 10f));
                rot.Y = (float)(InitRot.Y + (data[i].radianY * 10f));
                rot.Z = (float)(InitRot.Z + (data[i].radianZ * 10f));

                //st.Append(GetScript(InitPos, rot, data[i].velocity,data[i].acceleration, eMoveType.Joint));
                st.Append(GetScript(InitPos, rot, 0.1f, eMoveType.Joint));

                st.Append("\n");
            }

            st.Append("end");

            Debug.Log(st.ToString());

            TCPClient.SendPacket(st.ToString());

            return 1;
        }

        public override int Grip(Action onComplete = null)
        {
            TCPClient.SendPacket(URInterface.Grip, ePortType.Dashboard);
            Thread.Sleep(500);
            TCPClient.SendPacketWait(URInterface.ProgramState, "STOPPED");
            Debug.Log("Grip");
            Thread.Sleep(3000);


            onComplete?.Invoke();

            return 1;
        }

        public override int Release(Action onComplete = null)
        {
            TCPClient.SendPacket(URInterface.Release, ePortType.Dashboard);
            Thread.Sleep(500);
            TCPClient.SendPacketWait(URInterface.ProgramState, "STOPPED");
            Debug.Log("Release");
            Thread.Sleep(2000);


            onComplete?.Invoke();

            return 1;

        }

        public override bool MonitorConnection()
        {
            bool isConnect = false;

            if(TCPClient == null)
                return isConnect;

            if (!TCPClient.ConnectCheck)
                return isConnect;


            TCPClient.SendPacket(URInterface.RobotMode, ePortType.Dashboard,false);

            if(TCPClient.Receive(ePortType.Dashboard,false).Contains(eRobotMode.RUNNING.ToString()))
            {
                isConnect = true;
            }

            return isConnect;
        }

        public override int MoveLoop(Vector3 fromPos, Vector3 fromRot, Vector3 toPos, Vector3 toRot, bool isLoop, float loopTime, Action onComplete = null, eMoveType moveType = eMoveType.Position)
        {
            StringBuilder st = new StringBuilder();

            st.Append("def my_sequence():\n");

            if (isLoop)
            {
                st.Append("while (True):\n");
            }

            if(isLoop)
            {
                loopTime = 1;
            }

            for (int i = 0; i < loopTime; i++)
            {
                st.Append(GetScript(fromPos, fromRot));
                st.Append("\n");
                st.Append(GetScript(toPos, toRot));
                st.Append("\n");

            }

            if (isLoop)
            {
                st.Append("end\n");
            }

            st.Append("end");

            Debug.Log(st.ToString());

            TCPClient.SendPacket(st.ToString());

            MoveWaitAsync(onComplete);

            return 1;
        }
    }
}
