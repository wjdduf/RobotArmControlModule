using com.rainbow.external;
using RobotArm_Module.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public class RBTCPClient : TCPClient
    {
        protected RBSocket RBCommandSocket = null;
        protected RBSocket RBDataSocket = null;


        public bool ConnectCheck = false;
        private bool CommandSocketConnectCheck = false;
        private bool DataSocketConnectCheck = false;

        public const int MOVE_CMD_IGNORE_COUNT = 3;
        public static int moveCmdCnt = 0;

        public RBTCPClient() { }

        public RBTCPClient(string ip)
        {
            Connect(ip);
        }

        public async void WorkThread()
        {
            while (true)
            {
                await Task.Delay(100);

                DisplayThread();
                DataReadThread();
            }
        }

        public override bool Connect(string ip)
        {
            try
            {
                RBCommandSocket = new RBSocket(ip, DataContainer.Instance.RBConfig.RBTCP_PORT);
                RBCommandSocket.ActionConnected += () =>
                {
                    CommandSocketConnectCheck = true;
                    SocketConnectCheck();

                };
                RBCommandSocket.ActionConnectionLost += () =>
                {
                    CommandSocketConnectCheck = false;
                    SocketConnectCheck();
                };
                RBDataSocket = new RBSocket(ip, DataContainer.Instance.RBConfig.DATA_PORT);
                RBDataSocket.ActionConnected += () =>
                {
                    DataSocketConnectCheck = true;
                    SocketConnectCheck();
                };
                RBDataSocket.ActionConnectionLost += () =>
                {
                    DataSocketConnectCheck = false;
                    SocketConnectCheck();
                };

                RBCommandSocket.Connect();
                RBDataSocket.Connect();

                //DataReadThread();


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RBTCPClient Connect Error: " + ex.Message);
                return false;
            }
        }

        private void SocketConnectCheck()
        {
            ConnectCheck = CommandSocketConnectCheck && DataSocketConnectCheck;
        }

        public override bool DisConnect()
        {
            try
            {
                if (RBCommandSocket != null)
                    RBCommandSocket.Close();
                if (RBDataSocket != null)
                    RBDataSocket.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RBTCPClient DisConnect Error: " + ex.Message);
                return false;
            }
        }

        public override string Receive(ePortType portType = ePortType.Control, bool useLog = true)
        {
            string responseMessage = string.Empty;
            byte[] responseBuffer = new byte[2048];

            int bytesRead;


            if (portType == ePortType.Control)
            {
                if (RBCommandSocket.IsConnected)
                {
                    try
                    {
                        bytesRead = RBCommandSocket.Read(responseBuffer, responseBuffer.Length);
                        responseMessage = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);

                        if (useLog)
                            Console.WriteLine($"서버로부터 받은 응답: '{responseMessage}'\n");
                    }
                    catch (Exception e)
                    {
                        ConnectCheck = false;

                        Console.WriteLine($"Receive Error :: '{e.Message}'");
                    }
                }
            }
            else
            {
                if (RBDataSocket.IsConnected)
                {
                    try
                    {
                        bytesRead = RBDataSocket.Read(responseBuffer, responseBuffer.Length);
                        responseMessage = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);

                        if (useLog)
                            Console.WriteLine($"서버로부터 받은 응답: '{responseMessage}'\n");
                    }
                    catch (Exception e)
                    {
                        ConnectCheck = false;

                        Console.WriteLine($"Receive Error :: '{e.Message}'");
                    }
                }

            }



            return responseMessage;
        }

        public override void SendPacket(string message, ePortType portType = ePortType.Control, bool useLog = true)
        {
            byte[] data = Encoding.ASCII.GetBytes(message + "\n");

            if (portType == ePortType.Control)
            {
                if (RBCommandSocket.IsConnected)
                {
                    try
                    {
                        RBCommandSocket.Write(data);

                        if (useLog)
                            Debug.Log($"패킷 전송 완료: '{message}'");
                    }
                    catch (Exception e)
                    {
                        ConnectCheck = false;

                        Console.WriteLine($"SendPacket Error :: '{e.Message}'");

                    }
                }
                else
                {
                    ConnectCheck = false;

                }
            }
            else
            {
                if(RBDataSocket.IsConnected)
                {
                    try
                    {
                        RBDataSocket.Write(data);

                        if (useLog)
                            Debug.Log($"패킷 전송 완료: '{message}'");
                    }
                    catch (Exception e)
                    {
                        ConnectCheck = false;

                        Console.WriteLine($"SendPacket Error :: '{e.Message}'");

                    }
                }
                else
                {
                    ConnectCheck = false;

                }
            }


        }

        public override void SendPacketWait(string send, string waitText)
        {
            string responseMessage = string.Empty;
            byte[] responseBuffer = new byte[1024];

            while (!responseMessage.Contains(waitText) && !responseMessage.Contains(eRobotMode.RUNNING.ToString()))
            {
                //// 1. 패킷 전송
                //byte[] commandBytes = Encoding.UTF8.GetBytes(send + "\n");
                //
                //WebUIStream = WebUIClient.GetStream();
                //WebUIStream.Write(commandBytes, 0, commandBytes.Length);
                //Debug.Log($"전송: '{send.Trim()}'");
                //
                //// 2. 서버로부터 응답 받기
                //int bytesRead = WebUIStream.Read(responseBuffer, 0, responseBuffer.Length);
                //responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                //Debug.Log($"수신: '{responseMessage.Trim()}'");
                //
                //// 응답을 즉시 받지 못할 경우를 대비하여 잠시 대기
                //System.Threading.Thread.Sleep(100);
            }
        }

        public void DataReadThread()
        {
            byte[] recvBuf = null;
            byte[] readBuf = new byte[2048];

            if (RBDataSocket.IsConnected == true)
            {

                int read_size = RBDataSocket.Read(readBuf, 2048);
                if (read_size < 0)
                {
                    ;
                }
                else
                {
                    byte[] newBuf = BufferUtil.copyBytes(readBuf, 0, read_size);
                    if (recvBuf == null)
                    {
                        recvBuf = newBuf;
                    }
                    else
                    {
                        recvBuf = BufferUtil.AppendTwoByteArrays(recvBuf, newBuf);
                    }
                }


                while (true)
                {
                    if (recvBuf == null)
                    {
                        break;
                    }

                    int usedLength = Parsing(recvBuf);


                    if (usedLength != 0)
                    {
                        recvBuf = BufferUtil.copyBytes(recvBuf, usedLength, recvBuf.Length - usedLength);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void DisplayThread()
        {
            SendPacket(RBInterface.GetData, ePortType.Dashboard,false);
            //Receive(ePortType.Dashboard);

        }


        public int Parsing(byte[] parseBuf)
        {
            try
            {
                if (parseBuf[Const.IPCP_SFD] != Const.IPC_SFD)
                {
                    return 1;
                }

                if (parseBuf.Length < Const.IPC_HEADER_SIZE)
                {
                    return 0;
                }

                byte[] lengthBuf = BufferUtil.copyBytes(parseBuf, Const.IPCP_LEN, 2);
                int length = BufferUtil.getInt16(lengthBuf, 0);
                byte type = parseBuf[Const.IPCP_TYPE];

                if (parseBuf.Length < Const.IPC_HEADER_SIZE + length)
                {
                    return 0;
                }

                byte[] data = BufferUtil.copyBytes(parseBuf, Const.IPCP_DATA, length);
                IPCP ipcp = (IPCP)type;

                switch (ipcp)
                {
                    case IPCP.IPCT_PSS:
                        if (moveCmdCnt > 0)
                        {
                            moveCmdCnt--;
                        }
                        else
                        {
                            SetPSSData(data);
                        }
                        break;
                    case IPCP.IPCT_CFG:
                        break;
                    case IPCP.IPCT_POP:
                        break;
                    case IPCP.IPCT_ALARM:
                        break;
                }
                return length + Const.IPC_HEADER_SIZE;
            }
            catch (Exception ex)
            {
                ;
            }
            return 0;
        }

        public void SetPSSData(byte[] data)
        {
            Globals.IsReceivedData = true;

            SDP i;
            int idx = 0;
            for (i = SDP.SD_J1_REF; i <= SDP.SD_J6_REF; i++)
            {
                if ((int)i * 4 + 4 < data.Length)
                {
                    //Globals.JOINT_ANGLE_REFS[idx] = BufferUtil.getFloat(data, (int)i * 4);
                    DataContainer.Instance.RobotArmCurrentData.currentJoinData.Joints[(eJointType)idx + 1].Angle = BufferUtil.getFloat(data, (int)i * 4);

                }
                else
                {
                    break;
                }
                idx++;
            }

            idx = 0;
            for (i = SDP.SD_J1_ANG; i <= SDP.SD_J6_ANG; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {
                    Globals.JOINT_ANGLES[idx] = BufferUtil.getFloat(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }

            idx = 0;
            for (i = SDP.SD_J1_CUR; i <= SDP.SD_J6_CUR; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {
                    Globals.JOINT_CURRENTS[idx] = BufferUtil.getFloat(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }

            //Globals.TCP_VALUES_REFS[0] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_X_REF) * 4);
            //Globals.TCP_VALUES_REFS[1] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_Y_REF) * 4);
            //Globals.TCP_VALUES_REFS[2] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_Z_REF) * 4);
            //Globals.TCP_VALUES_REFS[3] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RX_REF) * 4);
            //Globals.TCP_VALUES_REFS[4] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RY_REF) * 4);
            //Globals.TCP_VALUES_REFS[5] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RZ_REF) * 4);

            DataContainer.Instance.RobotArmCurrentData.currentPosition.X = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_X_REF) * 4);
            DataContainer.Instance.RobotArmCurrentData.currentPosition.Y = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_Y_REF) * 4);
            DataContainer.Instance.RobotArmCurrentData.currentPosition.Z = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_Z_REF) * 4);
            DataContainer.Instance.RobotArmCurrentData.currentRotation.X = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RX_REF) * 4);
            DataContainer.Instance.RobotArmCurrentData.currentRotation.Y = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RY_REF) * 4);
            DataContainer.Instance.RobotArmCurrentData.currentRotation.Z = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RZ_REF) * 4);

            Globals.TCP_VALUES[0] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_X) * 4);
            Globals.TCP_VALUES[1] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_Y) * 4);
            Globals.TCP_VALUES[2] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_Z) * 4);
            Globals.TCP_VALUES[3] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RX) * 4);
            Globals.TCP_VALUES[4] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RY) * 4);
            Globals.TCP_VALUES[5] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RZ) * 4);

            idx = 0;
            for (i = SDP.SD_ANALOG_IN_0; i <= SDP.SD_ANALOG_IN_3; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {
                    Globals.ANALOG_IN[idx] = BufferUtil.getFloat(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }

            idx = 0;
            for (i = SDP.SD_ANALOG_OUT_0; i <= SDP.SD_ANALOG_OUT_3; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {
                    Globals.ANALOG_OUT[idx] = BufferUtil.getFloat(data, (int)i * 4);

                }
                else
                {
                    break;
                }
                idx++;
            }

            idx = 0;
            for (i = SDP.SD_DIGITAL_IN_0; i <= SDP.SD_DIGITAL_IN_15; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {
                    Globals.DIGITAL_IN[idx] = BufferUtil.getInt32(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }

            idx = 0;
            for (i = SDP.SD_DIGITAL_OUT_0; i <= SDP.SD_DIGITAL_OUT_15; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {
                    Globals.DIGITAL_OUT[idx] = BufferUtil.getInt32(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }

            idx = 0;
            for (i = SDP.SD_TEMPERATURE_MC1; i <= SDP.SD_TEMPERATURE_MC6; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {
                    Globals.TEMPERATURE_MCS[idx] = BufferUtil.getFloat(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }

            Globals.TASK_PC = BufferUtil.getInt32(data, (int)(SDP.SD_TASK_PC) * 4);
            Globals.TASK_REPEAT = BufferUtil.getInt32(data, (int)(SDP.SD_TASK_REPEAT) * 4);
            Globals.TASK_RUN_ID = BufferUtil.getInt32(data, (int)(SDP.SD_TASK_RUN_ID) * 4);
            if (Globals.TASK_RUN_ID < 1)
            {
                Globals.TASK_RUN_ID = 1;
            }
            Globals.TASK_RUN_NUM = BufferUtil.getInt32(data, (int)(SDP.SD_TASK_RUN_NUM) * 4);
            Globals.TASK_RUN_TIME = BufferUtil.getFloat(data, (int)(SDP.SD_TASK_RUN_TIME) * 4);
            Globals.TASK_STATE = BufferUtil.getInt32(data, (int)(SDP.SD_TASK_STATE) * 4);
            Globals.DEFAULT_SPEED = BufferUtil.getFloat(data, (int)(SDP.SD_DEFAULT_SPEED) * 4);



            Globals.robotState = BufferUtil.getInt32(data, (int)(SDP.SD_ROBOT_STATE) * 4);
            //Debug.Log($"Globals.robotState : {Globals.robotState}");

            if (Globals.robotState == 3)
            {
                DataContainer.Instance.RobotArmCurrentData.isMove = true;
            }
            else
            {
                DataContainer.Instance.RobotArmCurrentData.isMove = false;
            }

            Globals.POWER_STATE = BufferUtil.getInt32(data, (int)(SDP.SD_POWER_STATE) * 4);
            Globals.TCP_TARGETS[0] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_X_TARGET) * 4);
            Globals.TCP_TARGETS[1] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_Y_TARGET) * 4);
            Globals.TCP_TARGETS[2] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_Z_TARGET) * 4);
            Globals.TCP_TARGETS[3] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RX_TARGET) * 4);
            Globals.TCP_TARGETS[4] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RY_TARGET) * 4);
            Globals.TCP_TARGETS[5] = BufferUtil.getFloat(data, (int)(SDP.SD_TCP_RZ_TARGET) * 4);


            idx = 0;
            for (i = SDP.SD_JOINT_INFO_M1; i <= SDP.SD_JOINT_INFO_M6; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {

                    int offset = (int)i * 4;

                    byte temp = data[offset];

                    byte temp1 = data[offset + 1];
                    byte temp2 = data[offset + 2];
                    byte temp3 = data[offset + 3];

                    string str = Convert.ToString(temp, 2).PadLeft(8, '0');
                    string str1 = Convert.ToString(temp1, 2).PadLeft(8, '0');
                    string str2 = Convert.ToString(temp2, 2).PadLeft(8, '0');
                    string str3 = Convert.ToString(temp3, 2).PadLeft(8, '0');

                    int k = 0;
                    for (int j = 0; j < 8; j++)
                    {
                        string stateStr = str.Substring(7 - k, 1);
                        int stateInt;
                        Int32.TryParse(stateStr, out stateInt);
                        Globals.JOINT_INFO_ARRAY[i - SDP.SD_JOINT_INFO_M1, j] = stateInt;
                        // Debug.Log(Globals.JOINT_INFO_ARRAY[i - SDP.SD_JOINT_INFO_M1, j]);
                        k++;
                    }
                    k = 0;
                    for (int j = 8; j < 16; j++)
                    {
                        string stateStr = str1.Substring(7 - k, 1);
                        int stateInt;
                        Int32.TryParse(stateStr, out stateInt);
                        Globals.JOINT_INFO_ARRAY[i - SDP.SD_JOINT_INFO_M1, j] = stateInt;
                        k++;
                    }
                    k = 0;
                    for (int j = 16; j < 24; j++)
                    {
                        string stateStr = str2.Substring(7 - k, 1);
                        int stateInt;
                        Int32.TryParse(stateStr, out stateInt);
                        Globals.JOINT_INFO_ARRAY[i - SDP.SD_JOINT_INFO_M1, j] = stateInt;
                        k++;
                    }
                    k = 0;
                    for (int j = 24; j < 32; j++)
                    {
                        string stateStr = str3.Substring(7 - k, 1);
                        int stateInt;
                        Int32.TryParse(stateStr, out stateInt);
                        Globals.JOINT_INFO_ARRAY[i - SDP.SD_JOINT_INFO_M1, j] = stateInt;
                        k++;
                    }

                }
                else
                {
                    break;
                }
                idx++;

            }

            Globals.COLLISION_STATE = BufferUtil.getInt32(data, (int)(SDP.SD_COLLISION) * 4);
            Globals.IS_FREE_DRIVE_MODE = BufferUtil.getInt32(data, (int)(SDP.SD_IS_FREE_DRIVE_MODE) * 4);
            int pgMode = BufferUtil.getInt32(data, (int)(SDP.SD_PG_MODE) * 4);
            Globals.programMode = (Globals.ProgramMode)((int)pgMode);
            Globals.INIT_STATE_INFO = BufferUtil.getInt32(data, (int)(SDP.SD_INIT_STATE_INFO) * 4);
            Globals.INIT_ERR = BufferUtil.getInt32(data, (int)(SDP.SD_INIT_ERR) * 4);


            idx = 0;
            for (i = SDP.SD_TFB_ANALOG_IN_0; i <= SDP.SD_TFB_ANALOG_IN_1; i++)
            {
                if ((int)i * 4 + 4 < data.Length)
                {
                    Globals.TFB_ANALOG_IN[idx] = BufferUtil.getFloat(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }
            idx = 0;
            for (i = SDP.SD_TFB_DIGITAL_IN_0; i <= SDP.SD_TFB_DIGITAL_IN_1; i++)
            {
                if ((int)i * 4 + 4 < data.Length)
                {
                    Globals.TFB_DIGITAL_IN[idx] = BufferUtil.getInt32(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }

            idx = 0;
            for (i = SDP.SD_TFB_DIGITAL_OUT_0; i <= SDP.SD_TFB_DIGITAL_OUT_1; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {
                    Globals.TFB_DIGITAL_OUT[idx] = BufferUtil.getInt32(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }

            Globals.TPB_VOLTAGE_OUT = BufferUtil.getFloat(data, (int)(SDP.SD_TFB_VOLTAGE_OUT) * 4);

            Globals.OP_STAT_COLLISION_OCCUR = BufferUtil.getInt32(data, (int)(SDP.SD_OP_STAT_COLLISION_OCCUR) * 4);
            Globals.OP_STAT_SOS_FLAG = BufferUtil.getInt32(data, (int)(SDP.SD_OP_STAT_SOS_FLAG) * 4);
            Globals.OP_STAT_SELF_COLLISION = BufferUtil.getInt32(data, (int)(SDP.SD_OP_STAT_SELF_COLLISION) * 4);
            Globals.OP_STAT_ESTOP_OCCUR = BufferUtil.getInt32(data, (int)(SDP.SD_OP_STAT_ESTOP_OCCUR) * 4);
            Globals.OP_STAT_EMS_FLAG = BufferUtil.getInt32(data, (int)(SDP.SD_OP_STAT_EMS_FLAG) * 4);

            idx = 0;
            for (i = SDP.SD_DIGITAL_IN_CONFIG_0; i <= SDP.SD_DIGITAL_IN_CONFIG_1; i++)
            {
                if ((int)i * 4 + 4 <= data.Length)
                {
                    Globals.DIGITAL_IN_CONFIG[idx] = BufferUtil.getInt32(data, (int)i * 4);
                }
                else
                {
                    break;
                }
                idx++;
            }
        }
    }
}
