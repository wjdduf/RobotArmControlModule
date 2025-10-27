using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public class RobotArmController
    {
        public RobotArmBuilder RobotArmBuilder;

        private IConnect IConnect;
        private IMove IMove;
        private IData IData;
        private ISafety ISafety;
        private IGripper IGripper;

        public void Initialize(eRobotArmType type)
        {
            RobotArmBuilder = new RobotArmBuilder();
            RobotArmBuilder.Initialize(type);

            Register();
        }

        private void Register()
        {
            IConnect = RobotArmBuilder.CurrentRobotArm;
            IMove = RobotArmBuilder.CurrentRobotArm;
            IData = RobotArmBuilder.CurrentRobotArm;
            ISafety = RobotArmBuilder.CurrentRobotArm;
            IGripper = RobotArmBuilder.CurrentRobotArm;
        }

        public bool Connect(string ip, Action<bool> onComplete = null)
        {
            return IConnect.Connect(ip, onComplete);
        }

        public bool DisConnect(Action<bool> onComplete = null)
        {
            return IConnect.DisConnect(onComplete);
        }

        public void MoveToPosition(float speed, eDirection direction)
        {
            IMove.MoveToPosition(speed, direction);
        }

        public void MoveToRotation(float speed, eRotationAxis axis)
        {
            IMove.MoveToRotation(speed, axis);
        }

        public void MoveToPreset(Vector3 position, Vector3 rotation)
        {
            IMove.MoveToPreset(position, rotation);
        }

        public void SetPositionJ(Action<bool> onComplete = null)
        {
            IMove.MoveToPreset(DataContainer.Instance.RobotArmCurrentData.SetPosition, DataContainer.Instance.RobotArmCurrentData.SetRotation,eMoveType.Position,false, onComplete);
        }

        public void SetPositionL(Action<bool> onComplete = null)
        {
            IMove.MoveToPreset(DataContainer.Instance.RobotArmCurrentData.SetPosition, DataContainer.Instance.RobotArmCurrentData.SetRotation,eMoveType.Position, true, onComplete);
        }

        public void SetJoint(Action<bool> onComplete = null)
        {
            IMove.MoveToPreset(DataContainer.Instance.RobotArmCurrentData.SetPosition, DataContainer.Instance.RobotArmCurrentData.SetRotation, eMoveType.Joint, false, onComplete);
        }

        public void MoveToJoint(float speed, bool isUp, eJointType type)
        {
            IMove.MoveToJoint(speed, isUp, type);
        }

        public void JointRotation(float angle, eJointType type, Action<bool> onComplete = null)
        {
            IMove.JointRotation(angle, type, onComplete);
        }

        public void SetPivot(Vector3 pivot, Action<bool> onComplete = null)
        {
            IMove.SetPivot(pivot, onComplete);
        }

        public void ResetPivot(Action<bool> onComplete = null)
        {
            IMove.SetPivot(Vector3.Zero(), onComplete);
        }

        public void Stop(Action<bool> onComplete = null)
        {
            IMove.Stop(onComplete);
        }

        public void ListPlay(string jsonName)
        {
            var preset = JsonManager.ImportFromJsonFile<WorkPreset>(jsonName, DataContainer.Instance.JsonPath);

            IMove.PlayPreset(preset);
        }

        public void PlayWork(string workName, Action<bool> onComplete = null)
        {
            IMove.PlayRobotWork(workName, onComplete);

            Debug.Log("▶️ 자동 작업 실행 완료: " + workName);

        }

        public void AddPlayList(Vector3 pos, Vector3 rot, eMoveType moveType = eMoveType.Position)
        {
            IData.AddWorkQueue(pos, rot, moveType);
        }

        public void SetHoming(Action<bool> onComplete = null)
        {
            ISafety.Homming(onComplete);
        }

        public void SetSpeed(float speed)
        {
            RobotArmBuilder.CurrentRobotArm.Speed = speed;
        }

        public void SetAcceleration(float accel)
        {
            RobotArmBuilder.CurrentRobotArm.Acceleration = accel;
        }
            
        public bool GetSafetyMode()
        {
            return ISafety.GetSafetyMode();
        }

        public void UnlockProtectiveStop(Action<bool> onComplete = null)
        {
            ISafety.UnlockProtectiveStop(onComplete);
        }

        public List<CSVData> LoadCSV(string path)
        {
            CSVManager manager = new CSVManager();

            List<CSVData> data = manager.LoadDataFromCsv("VDIS_GYRO_Radian_120ms", path);

            //RobotArmBuilder.CurrentRobotArm.PlayCSV(data);

            return data;
        }

        public void PlayCSV(List<CSVData> data, Action<bool> onComplete = null)
        {
            RobotArmBuilder.CurrentRobotArm.PlayCSV(data, onComplete);
        }

        public void ExportJson(string name)
        {
            JsonManager.ExportToJsonFile<WorkPreset>(DataContainer.Instance.WorkPreset, name, DataContainer.Instance.JsonPath);
        }

        public void ImportJson(string name)
        {
            DataContainer.Instance.WorkPreset = JsonManager.ImportFromJsonFile<WorkPreset>(name, DataContainer.Instance.JsonPath);
        }

        public T ImportJson<T>(string name)
        {
            return JsonManager.ImportFromJsonFile<T>(name, DataContainer.Instance.JsonPath);
        }

        public void Grip(Action<bool> onComplete = null)
        {
            IGripper.Grip(onComplete);
        }

        public void Release(Action<bool> onComplete = null)
        {
            IGripper.Release(onComplete);
        }

        public void SetDeviceAlignment(Vector3 pivot, Vector3 rotation, Action<bool> onComplete = null)
        {
            IMove.SetPivot(pivot);
            Vector3 pos = new Vector3(DataContainer.Instance.RobotArmCurrentData.currentPosition.X,
                                      DataContainer.Instance.RobotArmCurrentData.currentPosition.Y,
                                      DataContainer.Instance.RobotArmCurrentData.currentPosition.Z);

            Vector3 rot = new Vector3((float)(DataContainer.Instance.RobotArmCurrentData.currentRotation.X + rotation.X),
                                            (float)(DataContainer.Instance.RobotArmCurrentData.currentRotation.Y + rotation.Y),
                                            (float)(DataContainer.Instance.RobotArmCurrentData.currentRotation.Z + rotation.Z));
            IMove.MoveToPreset(pos, rot, eMoveType.Position, false, (isSuccess) => 
            { 
                IMove.SetPivot(Vector3.Zero());
                onComplete?.Invoke(true);
            });
        }

        public void WorkQueueClear()
        {
            RobotArmBuilder.CurrentRobotArm.actionQueue.Clear();
        }

        public void PlayLoop(Vector3 fromPos, Vector3 fromRot, Vector3 toPos, Vector3 toRot, bool isLoop, float loopTime, Action<bool> onComplete = null, eMoveType moveType = eMoveType.Position)
        {
            IMove.MoveLoop(fromPos,fromRot,toPos,toRot,isLoop,loopTime, onComplete, moveType);

        }

        public void PlayLoop(Vector3 fromPos,  Vector3 toPos, bool isLoop, float loopTime, Action<bool> onComplete = null, eMoveType moveType = eMoveType.Position)
        {
            IMove.MoveLoop(fromPos, DataContainer.Instance.RobotArmCurrentData.currentRotation,
                toPos, DataContainer.Instance.RobotArmCurrentData.currentRotation, 
                isLoop, loopTime, onComplete, moveType);

        }
    }
}
