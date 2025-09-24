using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotArm_Module
{
    public class CreateProcess
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);



        Process process;

        private Panel unityPanel;

        public async void CreateUnity(Panel panel, string processName)
        {
            try
            {
                Console.WriteLine("Test0");

                string unityFolderPath = Path.Combine(Application.StartupPath, "Unity");
                string unityExePath = Path.Combine(unityFolderPath, processName);

                unityPanel = panel;

                process = new Process();

                Console.WriteLine("Test0 :: " + unityExePath);


                process.StartInfo.FileName = unityExePath;
                process.StartInfo.Arguments = $"-parentHWND {panel.Handle.ToInt32()} -popupwindow";

                process.Start();

                await WaitForMainWindowHandle(process);

                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    // SetParent 함수를 호출하여 Unity 창을 Panel의 자식 창으로 설정
                    SetParent(process.MainWindowHandle, panel.Handle);

                    // 창 위치 및 크기 조정
                    MoveWindow(process.MainWindowHandle, 0, 0, panel.Width, panel.Height, true);
                }

            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }
        }


        private async Task WaitForMainWindowHandle(Process process, int maxWaitMilliseconds = 5000)
        {
            int elapsed = 0;
            while (process.MainWindowHandle == IntPtr.Zero && !process.HasExited && elapsed < maxWaitMilliseconds)
            {
                await Task.Delay(100);
                elapsed += 100;
                process.Refresh();
            }
        }


        public void ProcessClose()
        {
            if (process != null && !process.HasExited)
            {
                process.Kill();
                process.WaitForExit(2000);
            }
        }

        // 창 크기 변경 시 Unity 창도 함께 조정 (선택 사항)
        private void unityPanel_SizeChanged(object sender, EventArgs e)
        {
            if (process != null && !process.HasExited)
            {
                MoveWindow(process.MainWindowHandle, 0, 0, unityPanel.Width, unityPanel.Height, true);
            }
        }
    }
}
