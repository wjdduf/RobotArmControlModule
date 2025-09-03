using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    public class Debug
    {
        public static void Log(string text)
        {
            System.Diagnostics.Debug.WriteLine(text);
        }
    }
}
