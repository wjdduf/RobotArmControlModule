using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RobotArm_Module
{
    public enum eMoveType   
    {
        Position,
        Joint
    }


    public abstract class RobotArm : IMove, IConnect, ISafety, IData, IGripper
    {
        protected byte[] responseBuffer;

        protected TcpClient dashBoardClient = null;
        protected NetworkStream dashBoardCstream = null;

        protected TcpClient client = null;
        protected NetworkStream stream = null;

        public Vector3 currentPosition = new Vector3();
        public Vector3 currentRotation = new Vector3();

        public abstract bool Connect(string ip, Action onComplete = null);
        public abstract bool DisConnect();
        public abstract int MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position, bool isLinear = false, Action onComplete = null);
        public abstract void ShutDown();
        public abstract bool MonitorConnection();

        public abstract void TestCode(string script);

        public float Speed = 0.3f;
        public float Acceleration = 1.2f;

        protected bool isUsingPreset = false;

        protected Action onPresetComplete = null;

        protected bool isMove = false;


        public Queue<Action<Action>> actionQueue = new Queue<Action<Action>>();

        public RobotArm()
        {
            WorkThread();
            
        }

        private async void WorkThread()
        {
            while (true)
            {
                await Task.Delay(100);

                DataContainer.Instance.RobotArmCurrentData.isConnect = MonitorConnection();

            }
        }

        public void PlayRobotWork(string name)
        {
            RobotWorkList data = JsonManager.ImportFromJsonFile<RobotWorkList>(name, DataContainer.Instance.JsonPath);

            foreach (var step in data.WorkList)
            {
                actionQueue.Enqueue(next =>
                {
                    switch (step.Type)
                    {
                        case "PlayPreset":
                            var preset = JsonManager.ImportFromJsonFile<WorkPreset>(step.PresetName, DataContainer.Instance.JsonPath);
                            PlayPreset(preset,next);
                            break;
                        case "Grip":
                            Grip(next);
                            break;
                        case "Release":
                            Release(next);
                            break;
                    }
                });
            }

            Console.WriteLine("▶️ 자동 시퀀스 시작");
            RunNext();
        }

        private void RunNext()
        {
            if (actionQueue.Count == 0)
            {
                Console.WriteLine("✅ 모든 작업 완료");
                return;
            }

            var nextAction = actionQueue.Dequeue();
            nextAction(RunNext); // 현재 작업 실행, 완료되면 RunNext 호출
        }


        public static double RadiansToDegrees(double radians)
        {
            return radians * (180.0 / Math.PI);
        }

        public static double DegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180.0);
        }

        public abstract int Stop();
        public abstract void MoveToPosition(float speed, eDirection direction);
        public abstract void MoveToRotation(float speed, eRotationAxis axis);
        public abstract int MoveToJoint(float speed, bool isUp, eJointType type = eJointType.None);
        public abstract int JointRotation(float angle, eJointType type = eJointType.None);
        public abstract int SetPivot(Vector3 pivot);
        public abstract void PlayPreset(WorkPreset preset ,Action onComplete = null);
        public abstract void AddWorkQueue(Vector3 pos, Vector3 rot, eMoveType moveType = eMoveType.Position);
        public abstract void AddWorkQueue(PresetData[] preset);

        public abstract void EmergencyStop();

        public abstract int Homming();
        public abstract bool GetSafetyMode();
        public abstract int UnlockProtectiveStop();

        public abstract int PlayCSV(List<CSVData> data);
        public abstract int Grip(Action onComplete = null);
        public abstract int Release(Action onComplete = null);
        public abstract int MoveLoop(Vector3 fromPos, Vector3 fromRot, Vector3 toPos, Vector3 toRot, bool isLoop, float loopTime, Action onComplete = null, eMoveType moveType = eMoveType.Position);
    }
}
