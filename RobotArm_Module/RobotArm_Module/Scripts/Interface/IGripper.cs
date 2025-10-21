using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public interface IGripper
    {
        void Grip(Action onComplete = null);
        void Release(Action onComplete = null);

    }
}
