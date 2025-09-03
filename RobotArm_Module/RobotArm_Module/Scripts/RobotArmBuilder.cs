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
                    break;
                case eRobotArmType.Rainbow:
                    CurrentRobotArm = new RainbowRoboticsArm();
                    break;
            }
        }
    }

    
}
