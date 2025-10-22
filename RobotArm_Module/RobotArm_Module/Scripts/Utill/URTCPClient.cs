using CookComputing.XmlRpc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public class URTCPClient : TCPClient
    {
        protected TcpClient DashBoardClient = null;
        protected NetworkStream DashBoardStream = null;

        protected TcpClient PrimaryClient = null;
        protected NetworkStream PrimaryStream = null;

        IModbusXmlRpc proxy = XmlRpcProxyGen.Create<IModbusXmlRpc>();

        private RtdeClientConnector RtdeClientConnector;

        public EventHandler OnRTDESockClosed;
        public EventHandler OnRTDEDataReceive;

        public RTDE_Outputs UrOutputs = new RTDE_Outputs();

        string url = "http://192.168.1.40:31000/RPC2";

        public URTCPClient() { }

        public URTCPClient(string ip)
        {
            Connect(ip);
        }

        public override bool Connect(string ip)
        {
            try
            {
                DashBoardClient = new TcpClient();
                DashBoardClient.Connect(ip, DataContainer.Instance.URConfig.DASHBOARD_PORT);
                DashBoardStream = DashBoardClient.GetStream();

                PrimaryClient = new TcpClient();
                PrimaryClient.Connect(ip, DataContainer.Instance.URConfig.PRIMARY_PORT);
                PrimaryStream = PrimaryClient.GetStream();

                if(RtdeClientConnector == null)
                {
                    RtdeClientConnector = new RtdeClientConnector();
                    //RtdeClientConnector.Disconnect();
                    RtdeClientConnector.Connect(ip, 2);
                }
                

                proxy.Url = url;

                RTDEInitialize();
                Debug.Log("서버에 연결되었습니다.");

                onComplete?.Invoke();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Connection failed: {ex.Message}");
                return false;
            }
        }

        private void RTDEInitialize()
        {

            RtdeClientConnector.OnSockClosed -= new EventHandler(OnSockClosed);
            RtdeClientConnector.OnSockClosed += new EventHandler(OnSockClosed);

            RtdeClientConnector.OnDataReceive -= new EventHandler(OnDataReceive);
            RtdeClientConnector.OnDataReceive += new EventHandler(OnDataReceive);

            RtdeClientConnector.Setup_Ur_Outputs(UrOutputs, 10);


            RtdeClientConnector.Ur_ControlStart();

            //RtdeClientConnector.Setup_Ur_Inputs(UrOutputs);
        }

        private void OnDataReceive(object sender, EventArgs e)
        {
            OnRTDEDataReceive?.Invoke(sender, e);
        }

        private void OnSockClosed(object sender, EventArgs e)
        {
            OnRTDESockClosed?.Invoke(sender, e);
        }

        public override bool DisConnect()
        {
            try
            {
                if (DashBoardStream != null)
                    DashBoardStream.Close();
                if (DashBoardClient != null)
                    DashBoardClient.Close();

                if (PrimaryStream != null)
                    PrimaryStream.Close();
                if (PrimaryClient != null)
                    PrimaryClient.Close();

                RtdeClientConnector?.Disconnect();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnection failed: {ex.Message}");
                return false;
            }
        }

        public override void SendPacket(string message, ePortType portType = ePortType.Primary, bool useLog = true)
        {

            if (portType == ePortType.Dashboard)
            {
                System.Threading.Thread.Sleep(100);

                byte[] data = Encoding.UTF8.GetBytes(message + "\n");

                DashBoardStream = DashBoardClient.GetStream(); // 데이터 전송을 위한 네트워크 스트림 얻기
                DashBoardStream.Write(data, 0, data.Length); // 데이터 쓰기
                
                if(useLog)
                    Console.WriteLine($"패킷 전송 완료: '{message}'");
            }
            else
            {
                byte[] data = Encoding.UTF8.GetBytes(message + "\n");

                PrimaryStream = PrimaryClient.GetStream(); // 데이터 전송을 위한 네트워크 스트림 얻기
                PrimaryStream.Write(data, 0, data.Length); // 데이터 쓰기
                
                if(useLog)
                    Debug.Log($"패킷 전송 완료: '{message}'");
            }
        }

        public override string Receive(ePortType portType = ePortType.Primary, bool useLog = true)
        {
            string responseMessage = string.Empty;
            byte[] responseBuffer = new byte[1024];

            int bytesRead;
            if (portType == ePortType.Dashboard)
            {
                bytesRead = DashBoardStream.Read(responseBuffer, 0, responseBuffer.Length);
            }
            else
            {
                bytesRead = PrimaryStream.Read(responseBuffer, 0, responseBuffer.Length);
            }
            responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);

            if(useLog)
                Console.WriteLine($"서버로부터 받은 응답: '{responseMessage}'\n");
        
            return responseMessage;
        }


        public override void SendPacketWait(string send, string waitText)
        {
            string responseMessage = string.Empty;
            byte[] responseBuffer = new byte[1024];

            while (!responseMessage.Contains(waitText) && !responseMessage.Contains(eRobotMode.RUNNING.ToString()))
            {
                // 1. 패킷 전송
                byte[] commandBytes = Encoding.UTF8.GetBytes(send + "\n");

                DashBoardStream = DashBoardClient.GetStream();
                DashBoardStream.Write(commandBytes, 0, commandBytes.Length);
                Debug.Log($"전송: '{send.Trim()}'");

                // 2. 서버로부터 응답 받기
                int bytesRead = DashBoardStream.Read(responseBuffer, 0, responseBuffer.Length);
                responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                Debug.Log($"수신: '{responseMessage.Trim()}'");

                // 응답을 즉시 받지 못할 경우를 대비하여 잠시 대기
                System.Threading.Thread.Sleep(100);
            }
        }

        public void SendPacketSafety(string send, string waitText)
        {
            string responseMessage = string.Empty;
            byte[] responseBuffer = new byte[1024];

            while (!responseMessage.Contains(waitText) && !responseMessage.Contains(eRobotMode.RUNNING.ToString()))
            {
                // 1. 패킷 전송
                byte[] commandBytes = Encoding.UTF8.GetBytes(send + "\n");

                DashBoardStream = DashBoardClient.GetStream();
                DashBoardStream.Write(commandBytes, 0, commandBytes.Length);
                Debug.Log($"전송: '{send.Trim()}'");

                // 2. 서버로부터 응답 받기
                int bytesRead = DashBoardStream.Read(responseBuffer, 0, responseBuffer.Length);
                responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                Debug.Log($"수신: '{responseMessage.Trim()}'");

                // 응답을 즉시 받지 못할 경우를 대비하여 잠시 대기
                System.Threading.Thread.Sleep(100);
            }
        }

        public void Test()
        {
            int address = 1409;  // 예시 주소
            int data = 1;       // 예시 데이터
            try
            {
                int result = proxy.WriteModbus5(address, data);
                Console.WriteLine($"Modbus Write Result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"XML-RPC 호출 오류: {ex.Message}");
            }
        }

    }

    public interface IModbusXmlRpc : IXmlRpcProxy
    {
        [XmlRpcMethod("tool_modbus_write5")]
        int WriteModbus5(int address, int data);
    }
}
