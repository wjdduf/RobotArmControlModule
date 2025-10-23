using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public interface IGripper
    {
        void Grip(Action<bool> onComplete = null);
        void Release(Action<bool> onComplete = null);

    }
}
