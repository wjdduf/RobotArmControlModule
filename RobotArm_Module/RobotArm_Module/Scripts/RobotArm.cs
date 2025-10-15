using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public enum eMoveType   
    {
        Position,
        Joint
    }


    public abstract class RobotArm : IMove, IConnect, ISafety, IData
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
        public abstract void MoveToPreset(Vector3 position, Vector3 rotation, eMoveType moveType = eMoveType.Position);
        public abstract void ShutDown();

        public abstract void TestCode(string script);

        public float Speed = 0.1f;
        public float Acceleration = 1.2f;

        protected bool isUsingPreset = false;

        protected Action onPresetComplete = null;

        protected bool isMove = false;

        private bool isFistTime = true;

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

        int currentQueueCount = 0;

        private void PlayMoveQueue()
        {
            if (DataContainer.Instance.WorkPreset.WorkList.Count <= currentQueueCount)
            {
                if (isUsingPreset)
                {
                    Debug.Log($"TestCode :: Complete WorkQueue = 0");
                    isUsingPreset = false;
                    currentQueueCount = 0;
                    isFistTime = true;

                    onPresetComplete?.Invoke();
                }
                return;
            }
            else
            {
                if (currentTargetQueue == null)
                {

                    // Get and remove the first item from the dictionary
                    currentTargetQueue = DataContainer.Instance.WorkPreset.WorkList[currentQueueCount];
                    currentQueueCount++;

                    Debug.Log($"TestCode :: start WorkQueue Dequeue : {currentQueueCount}");


                    MoveToPreset(currentTargetQueue?.position, currentTargetQueue?.rotation, currentTargetQueue.moveType);

                    if(isFistTime)
                    {
                        Debug.Log("IntoSleep");
                        Thread.Sleep(100);
                        isFistTime = false;
                    }

                }
                else
                {
                    //Debug.Log($"TestCode :: check WorkQueue Dequeue");

                    if (!isMove)
                    {
                        Debug.Log($"TestCode :: check WorkQueue complete");

                        currentTargetQueue = null;
                    }
                }
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
        public abstract void AddWorkQueue(Vector3 pos, Vector3 rot, eMoveType moveType = eMoveType.Position);
        public abstract void AddWorkQueue(PresetData[] preset);

        public abstract void EmergencyStop();

        public abstract void Homming();
        public abstract bool GetSafetyMode();
        public abstract void UnlockProtectiveStop();

        public abstract void PlayCSV(List<CSVData> data);
    }
}
