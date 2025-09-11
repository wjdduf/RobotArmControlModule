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

    }
}
