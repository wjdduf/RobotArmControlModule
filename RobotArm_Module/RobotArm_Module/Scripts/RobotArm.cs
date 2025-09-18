using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public abstract class RobotArm : IMove, IConnect, ISafety, IData
    {
        protected byte[] responseBuffer;

        protected TcpClient client = null;
        protected NetworkStream stream = null;

        public Vector3 currentPosition = new Vector3();
        public Vector3 currentRotation = new Vector3();

        public abstract bool Connect(string ip, Action onComplete = null);
        public abstract bool DisConnect();
        public abstract void MoveToPreset(Vector3 position, Vector3 rotation);
        public abstract void ShutDown();

        public abstract void TestCode(string script);

        protected bool isUsingPreset = false;

        protected Action onPresetComplete = null;

        protected bool isMove = false;

        public RobotArm()
        {
            WorkThread();
        }

        private async void WorkThread()
        {
            while (true)
            {
                await Task.Delay(100);

                if (isUsingPreset)
                {
                    PlayMoveQueue();
                }
            }
        }

        PresetData currentTargetQueue = null;

        private void PlayMoveQueue()
        {
            if (DataContainer.Instance.WorkPreset.WorkQueue.Count == 0)
            {
                if (isUsingPreset)
                {
                    Debug.Log($"TestCode :: Complete WorkQueue = 0");
                    isUsingPreset = false;
                    onPresetComplete?.Invoke();
                }
                return;
            }
            else
            {
                if (currentTargetQueue == null)
                {
                    Debug.Log($"TestCode :: start WorkQueue Dequeue");

                    currentTargetQueue = DataContainer.Instance.WorkPreset.WorkQueue.Dequeue();

                    MoveToPreset(currentTargetQueue.position, currentTargetQueue.rotation[0]);

                    Thread.Sleep(500);

                }
                else
                {
                    Debug.Log($"TestCode :: check WorkQueue Dequeue");

                    if (!isMove)
                    {
                        Debug.Log($"TestCode :: check WorkQueue complete");

                        currentTargetQueue = null;
                    }
                }
            }
        }

        protected string Receive()
        {
            string responseMessage = string.Empty;
            responseBuffer = new byte[1024];

            int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
            responseMessage = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead).Trim();
            Debug.Log($"서버로부터 받은 응답: '{responseMessage}'\n");

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

                stream = client.GetStream();
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

        public double RadiansToDegrees(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        public double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        public abstract void Stop();
        public abstract void MoveToPosition(float speed, eDirection direction);
        public abstract void MoveToRotation(float speed, eRotationAxis axis);
        public abstract void MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None);
        public abstract void JointRotation(float angle, eJointType type = eJointType.None);
        public abstract void SetPivot(Vector3 pivot);
        public abstract void PlayPreset(Action onComplete = null);
    }
}
