using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public interface IGripper
    {
        int Grip(Action onComplete = null);
        int Release(Action onComplete = null);

    }
}
