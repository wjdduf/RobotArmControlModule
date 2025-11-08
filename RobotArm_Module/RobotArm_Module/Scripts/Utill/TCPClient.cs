using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public enum ePortType
    {
        Control, // UR Primary, RB Socket
        Dashboard
    }
    public abstract class TCPClient
    {
        protected Action onComplete = null;

        public abstract bool Connect(string ip);
        public abstract bool DisConnect();
        public abstract void SendPacket(string message, ePortType portType = ePortType.Control, bool useLog = true);
        public abstract string Receive(ePortType portType = ePortType.Control, bool useLog = true);

        public abstract void SendPacketWait(string send, string waitText);

        public static async void HeartBeatThread(string ip, int port)
        {
            while (true)
            {
                await Task.Delay(1000);

                DataContainer.Instance.RobotArmCurrentData.ControlBoxPowerOn = await CheckTcpConnectionAsync(ip, port);
                Console.WriteLine($"{DateTime.Now:HH:mm:ss} - 연결 상태: {(DataContainer.Instance.RobotArmCurrentData.ControlBoxPowerOn ? "성공" : "실패")}");

            }
        }

        public static async Task<bool> CheckTcpConnectionAsync(string ip, int port)
        {
            using (var client = new TcpClient())
            {
                try
                {
                    Console.WriteLine($"CheckTcpConnectionAsync -  ip : {ip} , port : {port}");

                    var connectTask = client.ConnectAsync(ip, port);
                    var timeoutTask = Task.Delay(500); // 연결 타임아웃 0.5초

                    var completedTask = await Task.WhenAny(connectTask, timeoutTask);
                    return completedTask == connectTask && client.Connected;
                    //return completedTask == connectTask;

                }
                catch (Exception e)
                {
                    Console.WriteLine("CheckTcpConnectionAsync : " + e.ToString());
                    return false;
                }
            }
        }
    }
}
