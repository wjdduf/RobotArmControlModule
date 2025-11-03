using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module.Scripts.Data
{
    public static class RBInterface
    {
        public const string PowerOn = "mc jall init";

        public const string PowerOff = "arm_powerdown()"; //"AvadaKedavra()";

        public const string SetRealMode = "pgmode real";

        public const string Stop = "task stop";

        public const string GetData = "reqdata";
    }
}
