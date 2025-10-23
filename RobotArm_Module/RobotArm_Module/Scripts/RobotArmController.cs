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

        public bool Connect(string ip)
        {
            return IConnect.Connect(ip);
        }

        public bool DisConnect()
        {
            return IConnect.DisConnect();
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

        public int SetPositionJ()
        {
            return IMove.MoveToPreset(DataContainer.Instance.RobotArmCurrentData.SetPosition, DataContainer.Instance.RobotArmCurrentData.SetRotation);
        }

        public int SetPositionL()
        {
            return IMove.MoveToPreset(DataContainer.Instance.RobotArmCurrentData.SetPosition, DataContainer.Instance.RobotArmCurrentData.SetRotation,eMoveType.Position, true);
        }

        public int SetJoint()
        {
            return IMove.MoveToPreset(DataContainer.Instance.RobotArmCurrentData.SetPosition, DataContainer.Instance.RobotArmCurrentData.SetRotation, eMoveType.Joint);
        }

        public int MoveToJoint(float speed, bool isUp, eJointType type)
        {
            return IMove.MoveToJoint(speed, isUp, type);
        }

        public int JointRotation(float angle, eJointType type)
        {
            return IMove.JointRotation(angle, type);
        }

        public int SetPivot(Vector3 pivot)
        {
            return IMove.SetPivot(pivot);
        }

        public int ResetPivot()
        {
            return IMove.SetPivot(Vector3.Zero());
        }

        public int Stop()
        {
            return IMove.Stop();
        }

        public void ListPlay(string jsonName)
        {
            var preset = JsonManager.ImportFromJsonFile<WorkPreset>(jsonName, DataContainer.Instance.JsonPath);

            IMove.PlayPreset(preset);
        }

        public int PlayWork(string workName)
        {
            IMove.PlayRobotWork(workName);

            Debug.Log("▶️ 자동 작업 실행 완료: " + workName);

            return 1;
        }

        public void AddPlayList(Vector3 pos, Vector3 rot, eMoveType moveType = eMoveType.Position)
        {
            IData.AddWorkQueue(pos, rot, moveType);
        }

        public int SetHoming()
        {
            return ISafety.Homming();
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

        public int UnlockProtectiveStop()
        {
            return ISafety.UnlockProtectiveStop();
        }

        public List<CSVData> LoadCSV(string path)
        {
            CSVManager manager = new CSVManager();

            List<CSVData> data = manager.LoadDataFromCsv("VDIS_GYRO_Radian_120ms", path);

            //RobotArmBuilder.CurrentRobotArm.PlayCSV(data);

            return data;
        }

        public int PlayCSV(List<CSVData> data)
        {
            return RobotArmBuilder.CurrentRobotArm.PlayCSV(data);
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

        public int Grip()
        {
            return IGripper.Grip();
        }

        public int Release()
        {
            return IGripper.Release();
        }

        public int SetDeviceAlignment(Vector3 pivot, Vector3 rotation, Action onComplete = null)
        {
            IMove.SetPivot(pivot);
            Vector3 pos = new Vector3(DataContainer.Instance.RobotArmCurrentData.currentPosition.X,
                                      DataContainer.Instance.RobotArmCurrentData.currentPosition.Y,
                                      DataContainer.Instance.RobotArmCurrentData.currentPosition.Z);

            Vector3 rot = new Vector3((float)RobotArm.DegreesToRadians(DataContainer.Instance.RobotArmCurrentData.currentRotation.X),
                                            (float)RobotArm.DegreesToRadians(DataContainer.Instance.RobotArmCurrentData.currentRotation.Y),
                                            (float)RobotArm.DegreesToRadians(DataContainer.Instance.RobotArmCurrentData.currentRotation.Z));
            IMove.MoveToPreset(pos, rot, eMoveType.Position, false, () => 
            { 
                IMove.SetPivot(Vector3.Zero());
                onComplete?.Invoke();
            });

            return 1;
        }

        public void WorkQueueClear()
        {
            RobotArmBuilder.CurrentRobotArm.actionQueue.Clear();
        }

        public int PlayLoop(Vector3 fromPos, Vector3 fromRot, Vector3 toPos, Vector3 toRot, bool isLoop, float loopTime, Action onComplete = null, eMoveType moveType = eMoveType.Position)
        {
            return IMove.MoveLoop(fromPos,fromRot,toPos,toRot,isLoop,loopTime);

        }

        public int PlayLoop(Vector3 fromPos,  Vector3 toPos, bool isLoop, float loopTime, Action onComplete = null, eMoveType moveType = eMoveType.Position)
        {
            return IMove.MoveLoop(fromPos, DataContainer.Instance.RobotArmCurrentData.currentRotation,
                toPos, DataContainer.Instance.RobotArmCurrentData.currentRotation, 
                isLoop, loopTime);

        }
    }
}
