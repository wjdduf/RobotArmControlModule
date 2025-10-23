using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public interface ISafety
    {
        void EmergencyStop();
        int Homming();

        bool GetSafetyMode();
        int UnlockProtectiveStop();

    }
}
