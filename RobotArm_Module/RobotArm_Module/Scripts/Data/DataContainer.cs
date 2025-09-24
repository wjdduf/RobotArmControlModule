using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public sealed class DataContainer
    {
        private static readonly DataContainer _instance = new DataContainer();

        private DataContainer()
        {
            Console.WriteLine("싱글톤 인스턴스가 생성되었습니다.");
        }

        public static DataContainer Instance
        {
            get
            {
                return _instance;
            }
        }

        public string currentIP = "192.168.1.40";
        public URConfig URConfig = new URConfig();

        public RobotArmCurrentData RobotArmCurrentData = new RobotArmCurrentData();
        public WorkPreset WorkPreset = new WorkPreset();

        public int IDCount = 0;
    }

    public class WorkPreset
    {
        public List<PresetData> WorkList = new List<PresetData>();

        public void Add(PresetData data)
        {
            if (data != null)
            {
                WorkList.Add(data);
                DataContainer.Instance.IDCount++;
            }
        }

        public bool Delete(string id)
        {
            bool isSuccess = false;
            for (int i = 0; i < WorkList.Count; i++)
            {
                if (WorkList[i].presetID.Equals(id))
                {
                    WorkList.RemoveAt(i);
                    isSuccess = true;
                    break;
                }
            }
            return isSuccess;
        }

    }
}
