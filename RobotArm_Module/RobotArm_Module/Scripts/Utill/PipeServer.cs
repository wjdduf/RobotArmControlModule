using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotArm_Module
{
    public class PipeServer
    {
        private NamedPipeClientStream pipeClient;
        private StreamWriter writer;
        private StreamReader reader;

        public static Action<string> OnMessageReceived;

        public void ConnectUnityPipe()
        {
            try
            {
                pipeClient = new NamedPipeClientStream(".", "UnityPipe", PipeDirection.InOut);

                Console.WriteLine("[WinForms] Unity 파이프에 연결 중...");
                pipeClient.Connect(5000); // 5초 대기하며 연결 시도

                if (pipeClient.IsConnected)
                {
                    writer = new StreamWriter(pipeClient);
                    reader = new StreamReader(pipeClient);
                    Console.WriteLine("[WinForms] Unity 파이프 연결 성공. 이제 데이터를 보낼 수 있습니다.");
                }
            }
            catch (TimeoutException)
            {
                Console.WriteLine("[WinForms] Unity 프로세스가 응답하지 않습니다. 파이프가 연결되지 않았습니다.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WinForms] 오류 발생: {ex.Message}");
            }
        }

        public void Send(string data)
        {
            try
            {
                if(writer == null)
                {
                    ConnectUnityPipe();
                    return;
                }
                writer.WriteLine(data);
                writer.Flush();
                Console.WriteLine($"[WinForms] Unity로 전송: {data}");

                // 응답 수신 (선택 사항)
                string response = reader.ReadLine();
                OnMessageReceived?.Invoke(response);
                Console.WriteLine($"[WinForms] Unity로부터 수신: {response}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[WinForms] 통신 오류: {ex.Message}");
                // 통신 오류 발생 시 연결 재설정 시도
                DisconnectPipe();
                Task.Run(() => ConnectUnityPipe());
            }
        }

        public void DisconnectPipe()
        {
            if (writer != null) { writer.Dispose(); }
            if (reader != null) { reader.Dispose(); }
            if (pipeClient != null && pipeClient.IsConnected)
            {
                pipeClient.Close();
                pipeClient.Dispose();
                Console.WriteLine("[WinForms] 파이프 연결 종료.");
            }
        }

    }
}
