using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotArm_Module
{
    interface IData
    {
        void AddWorkQueue(Vector3 pos, Vector3 rot, eMoveType moveType = eMoveType.Position);
        void AddWorkQueue(PresetData[] preset);

    }
}
