using System;
using System.Net.Sockets;
using System.Text;
using UnderAutomation.UniversalRobots;

namespace RobotArm_Module.Scripts
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

    public enum eMoveType
    {
        NONE,
        C,
        L,
        J,
        P,
    }

    public class URArm : RobotArm
    {
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
                client = new TcpClient(ip, port);
                Debug.Log("서버에 연결되었습니다.");

                SendPacket(URInterface.PowerOn);
                Receive();

                SendPacketWait(URInterface.RobotMode, eRobotMode.IDLE.ToString());

                SendPacket(URInterface.BrakeRelease);
                Receive();

                SendPacketWait(URInterface.RobotMode, eRobotMode.RUNNING.ToString());

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

            try
            {
                // 2. 서버에 연결하기
                Debug.Log($"서버에 연결 중... ({ip}:{port})");
                client = new TcpClient(ip, port);
                Debug.Log("서버에 연결되었습니다.");

                SendPacket(URInterface.PowerOff);
                Receive();

                SendPacketWait(URInterface.RobotMode, eRobotMode.POWER_OFF.ToString());

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

        public override void JointRotation(float angle, eJointType type = eJointType.None)
        {
            throw new NotImplementedException();
        }

        public override void MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None)
        {
            throw new NotImplementedException();
        }

        public void MoveToPosition(Vector3 position, eJointType type = eJointType.None)
        {
            Debug.Log($"URArm position - {position} :: type - {type}");


            TcpClient client = null;
            try
            {
                // 1. TcpClient 객체를 생성하고 로봇에 연결합니다.
                client = new TcpClient(DataContainer.Instance.currentIP, DataContainer.Instance.URConfig.SECONDARY_PORT);

                // 2. 명령어를 바이트 배열로 변환하고 줄바꿈 문자를 추가합니다.
                string fullCommand = "movel(p[-0.600, 0.250, 0.00, 3.140, 0, 0])" + "\n";
                byte[] commandBytes = Encoding.UTF8.GetBytes(fullCommand);

                // 3. 네트워크 스트림을 얻어 명령어를 전송합니다.
                NetworkStream stream = client.GetStream();
                stream.Write(commandBytes, 0, commandBytes.Length);

                Console.WriteLine($"Sent command: {fullCommand.Trim()}");
            }
            catch (SocketException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }
            finally
            {
                // 4. 연결을 안전하게 종료합니다.
                if (client != null)
                {
                    client.Close();
                }
            }
        }

        public override void MoveToPosition(float speed, eDirection direction)
        {
            throw new NotImplementedException();
        }

        public override void MoveToPreset(Vector3 position, Vector3 rotation)
        {
            Debug.Log($"URArm MoveToPreset - {position} ::  {rotation}");

            StringBuilder st = new StringBuilder();
            st.Append("movel(p[");
            //st.Append("movel(p[");

            st.Append(position.X.ToString("F3") + ",");
            st.Append(position.Y.ToString("F3") + ",");
            st.Append(position.Z.ToString("F3") + ",");
            st.Append(rotation.X.ToString("F3") + ",");
            st.Append(rotation.Y.ToString("F3") + ",");
            st.Append(rotation.Z.ToString("F3"));
            //st.Append("])");
            st.Append("],0.05,0.1)");

            try
            {
                // 1. TcpClient 객체를 생성하고 로봇에 연결합니다.
                client = new TcpClient(DataContainer.Instance.currentIP, DataContainer.Instance.URConfig.REALTIMEINTERFACE_PORT);

                SendPacket(st.ToString());
                Receive();

                Console.WriteLine($"\nSent command: {st.ToString().Trim()}");
            }
            catch (SocketException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }
            finally
            {
                // 4. 연결을 안전하게 종료합니다.
                if (client != null)
                {
                    client.Close();
                }
            }
        }


        public override void MoveToRotation(float speed, eRotationAxis axis)
        {
            throw new NotImplementedException();
        }

        public override void PlayPreset(Action onComplete)
        {
            throw new NotImplementedException();
        }

        public override void SetPivot(Vector3 pivot)
        {
            throw new NotImplementedException();
        }

        public override void ShutDown()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override void TestCode(string script)
        {
            Debug.Log($"URArm TestCode -");

            StringBuilder st = new StringBuilder();
            st.Append("get_actual_joint_positions()");
            //st.Append("get_actual_tcp_pose()");
            //st.Append(" force()");



            try
            {
                // 1. TcpClient 객체를 생성하고 로봇에 연결합니다.
                client = new TcpClient(DataContainer.Instance.currentIP, DataContainer.Instance.URConfig.RTDE);

                SendPacket(st.ToString());
                //SendPacketWait(st.ToString(),"test");

                Receive();

                //Console.WriteLine($"Sent command: {st.ToString().Trim()}");
            }
            catch (SocketException e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }
            finally
            {
                // 4. 연결을 안전하게 종료합니다.
                if (client != null)
                {
                    client.Close();
                }
            }
        }

    }
}
