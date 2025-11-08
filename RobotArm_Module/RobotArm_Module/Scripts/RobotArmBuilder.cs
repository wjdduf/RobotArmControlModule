using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    

    public class RobotArmBuilder
    {
        public RobotArm CurrentRobotArm;

        public void Initialize(eRobotArmType type)
        {
            switch(type)
            {
                case eRobotArmType.UR:
                    CurrentRobotArm = new URArm();
                    TCPClient.HeartBeatThread(DataContainer.Instance.URConfig.IP, DataContainer.Instance.URConfig.DASHBOARD_PORT);
                    break;
                case eRobotArmType.RB:
                    CurrentRobotArm = new RBArm();
                    TCPClient.HeartBeatThread(DataContainer.Instance.RBConfig.IP, DataContainer.Instance.RBConfig.DATA_PORT);

                    break;
            }
        }
    }
}
