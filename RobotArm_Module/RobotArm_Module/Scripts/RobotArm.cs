using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace RobotArm_Module
{
    public abstract class RobotArm : IMove, IConnect, ISafety
    {
        protected byte[] responseBuffer;

        protected TcpClient client = null;
        protected NetworkStream stream = null;

        public abstract bool Connect(string ip);
        public abstract bool DisConnect();
        public abstract void MoveToPosition(Vector3 position, eJointType type = eJointType.None);
        public abstract void MoveToPreset(Vector3 position, Vector3 rotation);
        public abstract void MoveToRotation(Vector3 rotation, eJointType type = eJointType.None);
        public abstract void ShutDown();

        protected string Receive()
        {
            string responseMessage = string.Empty;
            responseBuffer = new byte[1024];

            int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
            responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
            Debug.Log($"서버로부터 받은 응답: '{responseMessage}'");

            return responseMessage;
        }

        protected void SendPacket(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text + "\n");

            stream = client.GetStream(); // 데이터 전송을 위한 네트워크 스트림 얻기
            stream.Write(data, 0, data.Length); // 데이터 쓰기
            Debug.Log($"패킷 전송 완료: '{text}'");
        }

        protected void SendPacketWait(string send, string waitText)
        {
            string responseMessage = string.Empty;
            responseBuffer = new byte[1024];


            while (!responseMessage.Contains(waitText))
            {
                // 1. 패킷 전송
                byte[] commandBytes = Encoding.UTF8.GetBytes(send + "\n");
                stream.Write(commandBytes, 0, commandBytes.Length);
                Debug.Log($"전송: '{send.Trim()}'");

                // 2. 서버로부터 응답 받기
                int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
                responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                Debug.Log($"수신: '{responseMessage.Trim()}'");

                // 응답을 즉시 받지 못할 경우를 대비하여 잠시 대기
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
